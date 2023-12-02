using MediatR;

namespace PB.Application.Brands.Commands.CreateBrand;

public record CreateBrandCommand(string Title, string Slug, int OrderNumber, bool Status) : IRequest<Result<Guid>>;
