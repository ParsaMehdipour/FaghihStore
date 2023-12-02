using Category.Application.Categories.Commands.EditCategory;
using Category.Domain.Repositories;
using FluentResults;
using MediatR;

namespace Category.Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Result<EditCategoryCommand>>
{

    protected ICategoryRepository _categoryRepository { get; }

    public GetCategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<EditCategoryCommand>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        Domain.Models.Category category = await _categoryRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(category, nameof(category));

        return Result.Ok(request.ToCommand(category));
    }
}
