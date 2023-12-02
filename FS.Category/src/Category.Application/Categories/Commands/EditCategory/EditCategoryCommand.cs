using FluentResults;

using MediatR;

namespace Category.Application.Categories.Commands.EditCategory;

public record EditCategoryCommand(Guid Id, Guid ParentId, string Title, string Slug, int OrderNumber, bool Status) : IRequest<Result>;

