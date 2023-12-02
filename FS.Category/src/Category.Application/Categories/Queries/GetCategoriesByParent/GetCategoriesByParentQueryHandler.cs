using Category.Application.Categories.Queries.GetCategoriesByParent;
using Category.Domain.Repositories;

using FluentResults;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SH.Infrastructure.Criteria;

namespace Category.Application.Categories.Queries.GetCategoryChildren;

public class GetCategoriesByParentQueryHandler : IRequestHandler<GetCategoriesByParentQuery, Result<ResponseModel<GetCategoriesByParentViewModel>>>
{
    protected ICategoryRepository _categoryRepository { get; }

    public GetCategoriesByParentQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<ResponseModel<GetCategoriesByParentViewModel>>> Handle(GetCategoriesByParentQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.Get()
            .AsNoTracking()
            .Where(_ => request.ParentId == Guid.Empty ? _.Parent == null : _.ParentCategoryId == request.ParentId)
            .OrderBy(_ => _.Title)
            .Select(_ => new GetCategoryDto(
                _.Id,
                _.Title))
            .ToListAsync(cancellationToken);

        var parentCategory = await _categoryRepository.Get(_ => _.Id == request.ParentId)
            .AsNoTracking()
            .Select(_ => _.ParentCategoryId.HasValue ? _.ParentCategoryId.Value : Guid.Empty).FirstOrDefaultAsync(cancellationToken);

        ResponseModel<GetCategoriesByParentViewModel> response = new()
        {
            Model = new()
            {
                ParentId = parentCategory,
                Children = categories.AsReadOnly()
            }
        };

        return Result.Ok(response);
    }
}