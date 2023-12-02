using MediatR;
using SH.Infrastructure.Criteria;

namespace PM.Application.ProductDescriptions.Queries.GetProductDescriptions;

public record GetProductDescriptionsQuery(Guid ProductId) : IRequest<Result<ResponseModel<IEnumerable<GetProductDescriptionDto>>>>;

public record GetProductDescriptionDto(Guid Id, string Title, string Description);