using MediatR;

using Microsoft.AspNetCore.Http;

using PM.Domain.ProductAgg;
using PM.Domain.ProductImageAgg;

namespace PM.Application.ProductImages.Commands.CreateProductImage;

public record CreateProductImageCommand(Guid ProductId, List<IFormFile> Files) : IRequest<Result<Guid>>;

public static class ProductImageExtension
{
    public static List<ProductImage> ToModel(this CreateProductImageCommand createProductImageCommand, ProductFactory productFactory, List<(IFormFile File, string NewFileName)> fullFileNames)
    {
        List<ProductImage> productImages = new(capacity: createProductImageCommand.Files.Count);

        fullFileNames.ForEach(file =>
        {
            var sequences = file.NewFileName.Split('/');

            var filePath = string.Join('/', sequences.Where(sequence => !sequence.Contains('.')));

            productImages.Add(productFactory.CreateImage(string.Empty, string.Empty, url: filePath, fileName: sequences.First(sequence => sequence.Contains('.')), false, createProductImageCommand.ProductId));
        });

        return productImages;
    }
}