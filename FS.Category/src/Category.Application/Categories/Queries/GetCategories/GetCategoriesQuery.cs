using Category.Application.Criteria;

using FluentResults;

using MediatR;

using SH.Infrastructure.Criteria;

namespace Category.Application.Categories.Queries.GetCategories;

public record GetCategoriesQuery(CategoryQueryStringParameters Parameters) : IRequest<Result<ResponseModel<IEnumerable<GetCategoryDto>, CategoryQueryStringParameters>>>;

public record GetCategoryDto(Guid Id, string Title, string Slug, string Parent, string CreateDate, bool HasChild);