using MediatR;

using PM.Application.ProductImages.Queries.GetProductImage;

namespace PM.Application.ProductImages.Queries.GetThumbnailProductImage;

public record GetThumbnailProductImageQuery(Guid ProductId) : IRequest<Result<GetProductImageDto>>;