using MediatR;
using PM.Application.ProductDescriptions.Commands.EditProductDescription;

namespace PM.Application.ProductDescriptions.Queries.GetProductDescription;

public record GetProductDescriptionQuery(Guid Id) : IRequest<Result<EditProductDescriptionCommand>>;