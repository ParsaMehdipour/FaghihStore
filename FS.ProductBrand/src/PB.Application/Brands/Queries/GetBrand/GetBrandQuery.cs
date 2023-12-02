using MediatR;
using PB.Application.Brands.Commands.EditBrand;

namespace PB.Application.Brands.Queries.GetBrand;

public record GetBrandQuery(Guid Id) : IRequest<Result<EditBrandCommand>>;

