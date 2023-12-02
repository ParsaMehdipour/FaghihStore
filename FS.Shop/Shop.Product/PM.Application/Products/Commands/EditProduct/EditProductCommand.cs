using MediatR;

using Microsoft.AspNetCore.Http;

using PM.Domain.ProductImageAgg;

namespace PM.Application.Products.Commands.EditProduct;

public record EditProductCommand(
    Guid Id,
    string TitlePersian,
    string TitleEnglish,
    string Slug,
    string MetaDescription,
    DateTime PublishedDate,
    string WarrantyDescription,
    Guid CategoryId,
    Guid BrandId,
    EditProductImageDto Image) : IRequest<Result<Guid>>;

public record EditProductImageDto(string Title, string Alt, IFormFile File, string CurrentImage);

public static class ProductImageExtension
{
    public static EditProductImageDto ToCommand(this ProductImage productImage) =>
        new(productImage.Title, productImage.Alt, null, productImage.GetFileNamePath());

    public static ProductImage ToModel(this EditProductImageDto editProductImageDto, ProductImage productImage, string fullFileName)
    {
        var sequences = fullFileName.Split('/');

        var filePath = string.Join('/', sequences.Where(sequence => !sequence.Contains('.')));

        productImage.SetTitle(editProductImageDto.Title);
        productImage.SetAlt(editProductImageDto.Alt);
        productImage.SetUrl(filePath);
        productImage.SetFileName(sequences.First(sequence => sequence.Contains('.')));

        return productImage;
    }
}