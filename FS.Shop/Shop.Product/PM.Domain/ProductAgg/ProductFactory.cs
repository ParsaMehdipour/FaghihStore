using PM.Domain.ProductDescriptionAgg;
using PM.Domain.ProductImageAgg;
using PM.Domain.ProductVarietyAggregate;

namespace PM.Domain.ProductAgg;

public class ProductFactory
{
    public Product Create(string titlePersian, string titleEnglish, Guid categoryId, Guid brandId, string slug, string metaDescription,
        string warrantyDescription, DateTime publishedDate, List<ProductImage> images)
    {
        Product product = new(titlePersian, titleEnglish, categoryId, brandId, slug, metaDescription,
            warrantyDescription, publishedDate, images);

        return product;
    }

    public ProductImage CreateImage(string title, string alt, string url, string fileName, bool isThumbnail, Guid productId)
    {
        ProductImage productImage = new(title, alt, url, fileName, isThumbnail, productId);

        return productImage;
    }

    public ProductDescription CreateDescription(string title, string description, Guid productId)
    {
        ProductDescription productDescription = new(title, description, productId);

        return productDescription;
    }

    public ProductVariety CreateVariety(Guid productId, Guid varietyId, Guid inventoryId)
    {
        ProductVariety productVariety = new(productId, varietyId, inventoryId);

        return productVariety;
    }
}