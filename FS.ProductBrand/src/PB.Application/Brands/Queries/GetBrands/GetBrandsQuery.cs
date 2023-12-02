using MediatR;

using PB.Application.Criteria;

using SH.Infrastructure.Criteria;

namespace PB.Application.Brands.Queries.GetBrands;

public record GetBrandsQuery(BrandQueryStringParameters Parameters) : IRequest<Result<ResponseModel<IEnumerable<GetBrandDto>, BrandQueryStringParameters>>>;

public record GetBrandDto(Guid Id, string Title, string Slug, bool Status, bool IsDeleted, string CreateDate);