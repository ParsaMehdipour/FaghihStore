using FluentResults;

using MediatR;

namespace Category.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(Guid ParentId, string Title, string Slug, int OrderNumber, bool Status) : IRequest<Result<Guid>>;
