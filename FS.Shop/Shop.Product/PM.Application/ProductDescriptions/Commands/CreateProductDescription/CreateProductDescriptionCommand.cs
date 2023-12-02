using MediatR;
using PM.Domain.ProductAgg;
using PM.Domain.ProductDescriptionAgg;

namespace PM.Application.ProductDescriptions.Commands.CreateProductDescription;

public record CreateProductDescriptionCommand(string Title, string Description, Guid ProductId) : IRequest<Result<Guid>>;

public static class ProductDescriptionExtension
{
    public static List<ProductDescription> ToModel(this List<CreateProductDescriptionCommand> descriptions, ProductFactory productFactory)
    {
        descriptions ??= new();

        return descriptions.Select(_ => productFactory.CreateDescription(_.Title, _.Description, _.ProductId)).ToList();
    }
}