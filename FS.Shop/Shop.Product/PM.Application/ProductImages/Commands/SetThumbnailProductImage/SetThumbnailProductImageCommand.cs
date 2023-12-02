using MediatR;

using PM.Application.ProductImages.Queries.GetProductImage;

namespace PM.Application.ProductImages.Commands.SetThumbnailProductImage;

public record SetThumbnailProductImageCommand(Guid Id, Guid ProductId, string Title, string Alt) : IRequest<Result<Guid>>;

public static class SetThumbnailProductImageCommandExtension
{
    public static SetThumbnailProductImageCommand ToCommand(this GetProductImageDto query, GetProductImageDto thumbnailProductImage)
    {
        return new(query.Id, query.ProductId, thumbnailProductImage.Title, thumbnailProductImage.Alt);
    }
}