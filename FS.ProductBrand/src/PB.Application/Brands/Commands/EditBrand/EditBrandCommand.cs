using MediatR;

namespace PB.Application.Brands.Commands.EditBrand;

public record EditBrandCommand(Guid Id, string Title, string Slug, int OrderNumber, bool Status) : IRequest<Result>;