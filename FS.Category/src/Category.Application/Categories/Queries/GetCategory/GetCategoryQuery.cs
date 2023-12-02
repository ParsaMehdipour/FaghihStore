using Category.Application.Categories.Commands.EditCategory;

using FluentResults;

using MediatR;

namespace Category.Application.Categories.Queries.GetCategory;

public record GetCategoryQuery(Guid Id) : IRequest<Result<EditCategoryCommand>>
{
    internal EditCategoryCommand ToCommand(Domain.Models.Category category) =>
        new EditCategoryCommand(
            category.Id,
            category.ParentCategoryId.GetValueOrDefault(Guid.Empty),
            category.Title,
            category.Slug,
            category.OrderNumber,
            category.Status);

}

