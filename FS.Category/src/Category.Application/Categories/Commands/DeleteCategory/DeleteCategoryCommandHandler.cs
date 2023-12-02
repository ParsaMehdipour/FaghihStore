using Category.Domain.Repositories;
using FluentResults;
using MediatR;

namespace Category.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
{

    protected ICategoryRepository _categoryRepository { get; }

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Domain.Models.Category category = await _categoryRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(category, nameof(category));

        if (category.ParentCategoryId == null)
            if (await _categoryRepository.IsExistsAsync(_ => _.ParentCategoryId == category.Id, cancellationToken))
                return Result.Fail("Can't Delete Category"); //TODO Use Toastify Notification

        if (request.IsRestored is true) category.RestoreItem();
        else category.DeleteItem();

        _categoryRepository.Update(category);

        await _categoryRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
