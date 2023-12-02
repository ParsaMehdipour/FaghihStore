using MediatR;

namespace PM.Application.ProductDescriptions.Commands.EditProductDescription;

public record EditProductDescriptionCommand(Guid Id, Guid ProductId, string Title, string Description) : IRequest<Result>;

