using Category.Domain.Repositories;

using FluentResults;

using MediatR;

namespace Category.Application.Categories.Commands.EditCategory;

public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, Result>
{

    protected ICategoryRepository _categoryRepository { get; }

    public EditCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        this._categoryRepository = categoryRepository;
    }

    public async Task<Result> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        Domain.Models.Category category = await _categoryRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(category, nameof(category));

        category.SetTitle(request.Title);
        category.SetSlug(request.Slug);
        category.SetOrderNumber(request.OrderNumber);
        category.SetStatus(request.Status);
        category.SetParent(request.ParentId);

        _categoryRepository.Update(category);
        await _categoryRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
