using MediatR;

namespace PM.Application.ProductImages.Commands.DeleteProductImage;

public record DeleteProductImageCommand(Guid Id, Guid ProductId) : IRequest<Result>;