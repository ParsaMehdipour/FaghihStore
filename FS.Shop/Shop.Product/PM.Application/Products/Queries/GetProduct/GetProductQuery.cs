using MediatR;

using PM.Application.Products.Commands.EditProduct;

namespace PM.Application.Products.Queries.GetProduct;
public record GetProductQuery(Guid Id) : IRequest<Result<EditProductCommand>>;