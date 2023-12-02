using MediatR;

namespace PM.Application.ProductImages.Queries.GetProductImage;

public record GetProductImageQuery(Guid Id, Guid ProductId) : IRequest<Result<GetProductImageDto>>;

public record GetProductImageDto(Guid Id, Guid ProductId, string Title, string Alt, string Url);