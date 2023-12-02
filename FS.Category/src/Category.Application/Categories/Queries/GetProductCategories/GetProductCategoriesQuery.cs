using FluentResults;

using MediatR;

namespace Category.Application.Categories.Queries.GetProductCategories;

public record GetProductCategoriesQuery(Dictionary<Guid, Guid> CategoryDictionary) : IRequest<Dictionary<Guid, string>>;

/// <summary>
/// return category Model that need a category title by Id:GUID
/// </summary>
public record GetOwnTitleRecordCategoriesQuery(Dictionary<Guid, Guid> CategoryDictionary) : IRequest<Result<Dictionary<Guid, string>>>;
