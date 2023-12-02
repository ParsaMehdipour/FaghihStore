using FluentResults;
using MediatR;

namespace Category.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id, bool IsRestored) : IRequest<Result>;
