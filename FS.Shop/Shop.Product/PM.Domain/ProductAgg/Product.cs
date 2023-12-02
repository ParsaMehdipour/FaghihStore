using PM.Domain.ProductDescriptionAgg;
using PM.Domain.ProductImageAgg;
using PM.Domain.ProductTraitAggregate;
using PM.Domain.ProductVarietyAggregate;

using SH.Domain;
using SH.Domain.Interfaces;

namespace PM.Domain.ProductAgg;

public class Product : AuditableEntity, IAggregateRoot
{
    private Product()
    {
    }

    public string TitlePersian { get; private set; }
    public string TitleEnglish { get; private set; }
    public string Slug { get; private set; }

    //////// Date ////////
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public DateTime PublishedDate { get; private set; }

    public int ViewCount { get; private set; }

    //////// Warranty ////////
    public string WarrantyDescription { get; private set; }
    public bool WarrantyIsExpired { get; private set; }

    public string MetaDescription { get; private set; }

    public Guid CategoryId { get; private set; }
    public Guid BrandId { get; private set; }
    public List<ProductImage> Images { get; private set; }
    public ICollection<ProductDescription> Descriptions { get; private set; }
    public ICollection<ProductVariety> ProductVarieties { get; private set; }
    public ICollection<ProductTraitItem> ProductTraitItems { get; private set; }

    public Product(string titlePersian, string titleEnglish, Guid categoryId, Guid brandId, string slug, string metaDescription,
        string warrantyDescription, DateTime publishedDate,
        List<ProductImage> images)
    {
        SetTitlePersian(titlePersian);
        SetTitleEnglish(titleEnglish);
        SetCategory(categoryId);
        SetSlug(slug);
        SetMetaDescription(metaDescription);
        IncreaseViewCount();
        SetPublishedDate(publishedDate);
        SetBrandId(brandId);
        SetWarrantyDescription(warrantyDescription);
        AddImages(images);
        this.CreatedDate = DateTime.Now;
    }

    public void SetTitlePersian(string titlePersian)
    {
        if (TitlePersian == titlePersian)
            return;

        TitlePersian = titlePersian;
    }

    public void SetTitleEnglish(string titleEnglish)
    {
        if (TitleEnglish == titleEnglish)
            return;

        TitleEnglish = titleEnglish;
    }

    public void SetSlug(string slug)
    {
        if (Slug == slug)
            return;

        Slug = slug;
    }

    public void IncreaseViewCount()
    {
        ViewCount++;
    }

    public void SetMetaDescription(string metaDescription)
    {
        if (MetaDescription == metaDescription)
            return;

        MetaDescription = metaDescription;
    }

    //Since we don't get list of descriptions this method need's refactoring
    public void AddDescriptions(ProductDescription description)
    {
        Descriptions ??= new List<ProductDescription>();

        Descriptions.Add(description);
    }

    public void AddProductVariety(ProductVariety productVariety)
    {
        ProductVarieties ??= new List<ProductVariety>();

        ProductVarieties.Add(productVariety);
    }

    public void AddImages(List<ProductImage> images)
    {
        Images ??= new List<ProductImage>();
        images ??= new List<ProductImage>();

        if (images.Count > 1)
            ImagesThumbnailBeOne(images);

        foreach (var image in images)
        {
            Images.Add(image);
        }
    }

    public void ImagesThumbnailBeOne(List<ProductImage> images)
    {
        var image = images.FirstOrDefault(_ => _.IsThumbnail);
        var thumbnails = images.Where(_ => _.FileName != image.FileName && _.IsThumbnail).ToList();
        thumbnails.ForEach(_ => _.SetThumbnail(false));
    }

    public string GetThumbnailImage()
    {
        if (Images is { Count: 0 })
            return string.Empty;

        return Images.FirstOrDefault(_ => _.IsThumbnail).GetFileNamePath();
    }

    public void SetPublishedDate(DateTime publishedDate)
    {
        if (PublishedDate == publishedDate)
            return;

        PublishedDate = publishedDate;
    }

    public void SetWarrantyDescription(string warrantyDescription)
    {
        if (WarrantyDescription == warrantyDescription)
            return;

        WarrantyDescription = warrantyDescription;
    }

    public void SetCategory(Guid categoryId)
    {
        if (CategoryId == categoryId)
            return;

        CategoryId = categoryId;
    }

    public void SetBrandId(Guid brandId)
    {
        if (BrandId == brandId)
            return;

        BrandId = brandId;
    }
}