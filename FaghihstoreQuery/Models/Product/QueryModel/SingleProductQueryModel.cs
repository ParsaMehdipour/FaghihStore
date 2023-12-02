using PM.Domain.ProductDescriptionAgg;
using PM.Domain.ProductImageAgg;
using VG.Domain.Enums;

namespace FaghihstoreQuery.Models.Product.QueryModel;

public record SingleProductQueryModel(string PersianTitle, string EnglishTitle, string Brand, string Warranty,
    List<ProductImageQueryModel> Images,
    List<ProductTraitWithTraitGroupModel> ProductTraitsWithTraitGroups,
    List<ProductVarietyQueryModel> ProductVarieties,
    List<ProductDescriptionQueryModel> Descriptions,
    List<ProductCategoryModel> ProductCategories,
    List<ProductQueryModel> SimilarProductQueryModels)
{
    public static List<ProductImageQueryModel> ToProductImageQueryModel(List<ProductImage> productImages) =>
        productImages.Select(_ => new ProductImageQueryModel(
            _.Title,
            _.GetFileNamePath(),
            _.IsThumbnail)).ToList();

    public static List<ProductDescriptionQueryModel> ToProductDescriptionQueryModel(ICollection<ProductDescription> productDescriptions) =>
        productDescriptions.Select(_ => new ProductDescriptionQueryModel(
            _.Title,
            _.Description)).ToList();

    public static IEnumerable<ProductCategoryModel> GetParents(Category.Domain.Models.Category category)
    {
        List<ProductCategoryModel> productCategoryModels = new() { new ProductCategoryModel(category.Id, category.Title) };

        while (category.Parent != null)
        {
            productCategoryModels.Add(new ProductCategoryModel(category.Parent.Id, category.Parent.Title));

            category = category.Parent;
        }

        return productCategoryModels;

    }
}

public record ProductTraitQueryModel(string Title, string Value, bool HasInGeneralSpecification);

public record ProductVarietyQueryModel(Guid Id, string Value, BoxType BoxType, long Quantity, bool InStock, string UnitPrice);

public record ProductDescriptionQueryModel(string Title, string Description);

public record ProductImageQueryModel(string Title, string Url, bool IsThumbnail);

public record ProductTraitWithTraitGroupModel(string TraitGroupTitle, List<ProductTraitQueryModel> ProductTraitQueryModels);

public record ProductCategoryModel(Guid Id, string Title);
