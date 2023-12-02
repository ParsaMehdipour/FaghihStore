using DNTPersianUtils.Core;

using MediatR;

using Microsoft.EntityFrameworkCore;

using PM.Application.Criteria;
using PM.Domain.ProductAgg;
using PM.Domain.ProductTraitAggregate;
using PM.Domain.Services;

using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Extensions;

namespace PM.Application.ProductTraits.Queries.GetProductTraitItems;

public record GetProductTraitItemsQueryHandler : IRequestHandler<GetProductTraitItemsQuery, Result<ResponseModel<GetProductTraitItemsViewModel, ProductTraitQueryStringParameters>>>
{
    protected IProductTraitItemRepository _productTraitItemRepository { get; }
    protected IProductRepository _productRepository { get; }
    protected IProductTraitGroupAcl _productTraitGroupAcl { get; }

    public GetProductTraitItemsQueryHandler(IProductTraitItemRepository productTraitItemRepository,
        IProductRepository productRepository,
        IProductTraitGroupAcl productTraitGroupAcl)
    {
        _productTraitItemRepository = productTraitItemRepository;
        _productRepository = productRepository;
        _productTraitGroupAcl = productTraitGroupAcl;
    }

    public async Task<Result<ResponseModel<GetProductTraitItemsViewModel, ProductTraitQueryStringParameters>>> Handle(GetProductTraitItemsQuery request, CancellationToken cancellationToken)
    {
        var productTraitItems = _productTraitItemRepository.Get()
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(_ => _.IsDeleted == request.Parameters.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
            productTraitItems = productTraitItems.Where(_ => _.Value.Contains(request.Parameters.Search));

        int count = await productTraitItems.CountAsync(cancellationToken);
        var pager = new Pager(count, request.Parameters.PageNumber);

        productTraitItems = productTraitItems.Paginate(pager);

        var traits = await _productTraitGroupAcl.GetTraitsById(productTraitItems.Select(_ => _.TraitId).ToArray(), cancellationToken);

        List<GetProductTraitItemsDto> result = new(count);

        foreach (var productTrait in productTraitItems)
        {
            result.Add(new(productTrait.Id,
                productTrait.Value,
                traits.FirstOrDefault(b => b.traitId == productTrait.TraitId).trait,
                 traits.FirstOrDefault(b => b.traitId == productTrait.TraitId).traitGroup,
                productTrait.CreatedDate.ToShortPersianDateString(true),
                productTrait.IsDeleted));
        }

        var product = await _productRepository.GetByIdAsync(cancellationToken, request.ProductId);

        ResponseModel<GetProductTraitItemsViewModel, ProductTraitQueryStringParameters> responseModel =
            new()
            {
                Model = new()
                {
                    ProductId = product.Id.ToString(),
                    ProductTitle = product.TitlePersian,
                    GetProductTraitItems = result.AsReadOnly()
                },
                Pager = pager,
                Parameters = request.Parameters
            };

        return Result.Ok(responseModel);
    }
}