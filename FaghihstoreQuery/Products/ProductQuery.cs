using Category.Domain.Repositories;
using FaghihstoreQuery.Interfaces;
using FaghihstoreQuery.Models.Product.QueryModel;
using FaghihstoreQuery.Models.Product.SerachModel;
using FluentResults;
using Inventory.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using PB.Domain.Repositories;
using PM.Domain.ProductAgg;
using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Extensions;
using TG.Domain.Repositories;
using VG.Domain.Enums;
using VG.Domain.Repositories;

namespace FaghihstoreQuery.Products;

public class ProductQuery : IProductQuery
{
    protected IProductRepository _productRepository { get; }
    protected ICategoryRepository _categoryRepository { get; }
    protected IInventoryRepository _inventoryRepository { get; }
    protected IBrandRepository _brandRepository { get; }
    protected IVarietyRepository _varietyRepository { get; }
    protected ITraitRepository _traitRepository { get; }
    protected ITraitGroupRepository _traitGroupRepository { get; }

    public ProductQuery(IProductRepository productRepository,
        IBrandRepository brandRepository,
        IInventoryRepository inventoryRepository,
        IVarietyRepository varietyRepository,
        ICategoryRepository categoryRepository,
        ITraitRepository traitRepository,
        ITraitGroupRepository traitGroupRepository)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _inventoryRepository = inventoryRepository;
        _varietyRepository = varietyRepository;
        _categoryRepository = categoryRepository;
        _traitRepository = traitRepository;
        _traitGroupRepository = traitGroupRepository;
    }

    public async Task<Result<ResponseModel<IEnumerable<ProductQueryModel>, ProductSearchModel>>> GetAsync(ProductSearchModel searchModel, CancellationToken cancellationToken)
    {
        var products = _productRepository.Get().Where(_ => _.ProductVarieties.Count > 0);
        var inventories = _inventoryRepository.Get();

        int count = await products.CountAsync(cancellationToken);
        var pager = new Pager(count, searchModel.PageNumber);

        products = products.Paginate(pager).AsNoTracking().Include(_ => _.Images).Include(_ => _.ProductVarieties);

        #region Filter

        if (searchModel.IsFilter())
        {
            if (searchModel.BrandQueryFilters.Any(_ => _.IsCurrent))
            {
                var brandIds = searchModel.BrandQueryFilters.Where(a => a.IsCurrent).Select(b => b.Id);

                products = products.Where(_ => brandIds.Contains(_.BrandId));
            }
        }

        #endregion

        var result = new List<ProductQueryModel>();

        foreach (var product in products)
        {
            result.Add(new(
                product.Id,
                product.TitlePersian,
                product.GetThumbnailImage(),
                //todo : refactor this shitty code
                inventories.ToList().Where(i => product.ProductVarieties.Any(pv => pv.InventoryId == i.Id)).MinBy(m => m.UnitPrice)!.UnitPrice.ToString("n0")));
        }

        ResponseModel<IEnumerable<ProductQueryModel>, ProductSearchModel> responseModel = new()
        {
            Model = result.AsReadOnly(),
            Parameters = searchModel,
            Pager = pager
        };

        return Result.Ok(responseModel);
    }
    public IEnumerable<ProductQueryModel> GetByCategoryId(Guid categoryId, Guid productId)
    {
        var products = _productRepository.Get();
        var inventories = _inventoryRepository.Get();

        products = products.AsNoTracking().Include(_ => _.Images).Include(_ => _.ProductVarieties).Where(_ => _.CategoryId == categoryId && _.Id != productId).Take(6);

        var result = new List<ProductQueryModel>();

        foreach (var product in products)
        {
            result.Add(new(
                product.Id,
                product.TitlePersian,
                product.GetThumbnailImage(),
                //todo : refactor this shitty code
                inventories.ToList().Where(i => product.ProductVarieties.Any(pv => pv.InventoryId == i.Id)).MinBy(m => m.UnitPrice)!.UnitPrice.ToString("n0")));
        }

        return result;
    }

    public async Task<Result<SingleProductQueryModel>> GetByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Get(_ => _.Id == productId)
            .Include(_ => _.ProductVarieties)
            .Include(_ => _.Descriptions)
            .Include(_ => _.ProductTraitItems)
            .Include(_ => _.Images)
            .FirstOrDefaultAsync(cancellationToken);

        ArgumentNullException.ThrowIfNull(product, nameof(product));

        var varieties = await _varietyRepository.Get(_ => product.ProductVarieties.Select(pv => pv.VarietyId).Contains(_.Id)).ToListAsync(cancellationToken);

        if (varieties.Any() is false)
            throw new ArgumentException();

        var inventories = await _inventoryRepository.Get(_ => product.ProductVarieties.Select(pv => pv.InventoryId).Contains(_.Id)).Select(_ => new { _.ProductVarietyId, Quantity = _.CalculateCurrentCount(), _.InStock, _.UnitPrice }).ToListAsync(cancellationToken);

        if (inventories.Any() is false)
            throw new ArgumentException();

        var productVarieties = product.ProductVarieties.Select(_ => new ProductVarietyQueryModel(
        Id: _.Id,
        Value: varieties.FirstOrDefault(v => v.Id == _.VarietyId).BoxType == BoxType.Circle ? varieties.FirstOrDefault(v => v.Id == _.VarietyId).ColorCode : varieties.FirstOrDefault(v => v.Id == _.VarietyId)!.Size,
        BoxType: varieties.FirstOrDefault(v => v.Id == _.VarietyId).BoxType,
        Quantity: inventories.FirstOrDefault(i => i.ProductVarietyId == _.Id).Quantity,
        InStock: inventories.FirstOrDefault(i => i.ProductVarietyId == _.Id).InStock,
        UnitPrice: inventories.FirstOrDefault(i => i.ProductVarietyId == _.Id).UnitPrice.ToString("n0"))).ToList();
        //parent -> list<children>

        var category = _categoryRepository.GetWithRecursiveParent(_ => _.Id == product.CategoryId);

        var brand = await _brandRepository.GetByIdAsync(cancellationToken, product.BrandId);

        var traits = _traitRepository.Get(_ => product.ProductTraitItems.Select(pt => pt.TraitId).Contains(_.Id)).Include(_ => _.TraitGroup).ToList();

        var traitGroups = _traitGroupRepository.Get(_ => traits.Select(t => t.TraitGroupId).Contains(_.Id)).Include(_ => _.Traits).ToList();

        var productTraits = traitGroups.Select(_ => new ProductTraitWithTraitGroupModel(
            TraitGroupTitle: _.Title,
            ProductTraitQueryModels: product.ProductTraitItems.Where(pt => _.Traits.Select(t => t.Id).Contains(pt.TraitId)).Select(pt => new ProductTraitQueryModel(
                _.Traits.FirstOrDefault(t => t.Id == pt.TraitId).Title,
                pt.Value,
                pt.HasInGeneralSpecification)).ToList()
        )).ToList();

        var similarProducts = GetByCategoryId(category.Id, product.Id).ToList();

        var singleProductQuery = new SingleProductQueryModel(
            product.TitlePersian,
            product.TitleEnglish,
            brand.Title,
            product.WarrantyDescription,
            SingleProductQueryModel.ToProductImageQueryModel(product.Images),
            productTraits,
            productVarieties,
            SingleProductQueryModel.ToProductDescriptionQueryModel(product.Descriptions),
            SingleProductQueryModel.GetParents(category).Reverse().ToList(),
            similarProducts);

        return Result.Ok(singleProductQuery);
    }
}
