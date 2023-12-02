using Category.Domain.Models;
using Category.Domain.Repositories;

using FluentResults;

using MediatR;

namespace Category.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
    protected ICategoryRepository _categoryRepository { get; }
    protected CategoryFactory _categoryFactory { get; }

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, CategoryFactory categoryFactory)
    {
        _categoryRepository = categoryRepository;
        _categoryFactory = categoryFactory;
    }


    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Domain.Models.Category category = _categoryFactory.Create(request.Title, request.Slug, request.Status);

        if (request.ParentId.Equals(Guid.Empty) is false)
            category.SetParent(request.ParentId);


        await _categoryRepository.AddAsync(category, cancellationToken);
        await _categoryRepository.SaveAsync(cancellationToken);

        return Result.Ok(category.Id);
    }
}
