using Category.Application.Criteria;
using Category.Domain.Repositories;

using FluentResults;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Extensions;

namespace Category.Application.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Result<ResponseModel<IEnumerable<GetCategoryDto>, CategoryQueryStringParameters>>>
{
    protected ICategoryRepository _categoryRepository { get; }

    public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetCategoryDto>, CategoryQueryStringParameters>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = _categoryRepository.Get();

        if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
            categories = categories.Where(_ => _.Title.Contains(request.Parameters.Search));

        int count = await categories.CountAsync(cancellationToken);
        var pager = new Pager(count, request.Parameters.PageNumber);

        var result = await categories.Paginate(pager)
            .AsNoTracking()
            .Where(_ => _.ParentCategoryId == request.Parameters.ParentId)
            .OrderBy(_ => _.Title)
            .Select(_ => new GetCategoryDto(
                _.Id,
                _.Title,
                _.Slug,
                _.Parent.Title,
                _.CreatedDate.ToPersian(),
                _.Children.Count > 0
          )).ToListAsync(cancellationToken);

        ResponseModel<IEnumerable<GetCategoryDto>, CategoryQueryStringParameters> responseModel = new()
        {
            Model = result.AsReadOnly(),
            Parameters = request.Parameters,
            Pager = pager
        };

        return Result.Ok(responseModel);
    }
}
