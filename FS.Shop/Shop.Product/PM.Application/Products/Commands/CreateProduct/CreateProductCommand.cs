using MediatR;

using Microsoft.AspNetCore.Http;

using PM.Domain.ProductAgg;
using PM.Domain.ProductImageAgg;

namespace PM.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(string TitlePersian,
    string TitleEnglish,
    string Slug,
    string MetaDescription,
    DateTime PublishedDate,
    string WarrantyDescription,
    Guid CategoryId,
    Guid BrandId,
    CreateProductImageDto Image) : IRequest<Result<Guid>>;

public record CreateProductImageDto(string Title, string Alt, IFormFile File, Guid ProductId);

public static class ProductImageExtension
{
    public static List<ProductImage> ToModel(this CreateProductImageDto createProductImageDto, ProductFactory productFactory, string fullFileName)
    {
        var sequences = fullFileName.Split('/');

        var filePath = string.Join('/', sequences.Where(sequence => !sequence.Contains('.')));

        List<ProductImage> productImages = new(capacity: 1)
        {
            productFactory.CreateImage(createProductImageDto.Title, createProductImageDto.Alt, url:filePath, fileName: sequences.First(sequence => sequence.Contains('.')), true, createProductImageDto.ProductId)
        };

        return productImages;
    }
}