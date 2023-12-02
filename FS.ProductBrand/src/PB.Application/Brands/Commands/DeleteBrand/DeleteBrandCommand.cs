using MediatR;

namespace PB.Application.Brands.Commands.DeleteBrand;

public record DeleteBrandCommand(Guid Id, bool IsRestored) : IRequest<Result>;
