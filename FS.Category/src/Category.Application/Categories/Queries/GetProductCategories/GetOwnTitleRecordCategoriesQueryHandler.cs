using Category.Domain.Repositories;

using FluentResults;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Category.Application.Categories.Queries.GetProductCategories;

public class GetOwnTitleRecordCategoriesQueryHandler : IRequestHandler<GetOwnTitleRecordCategoriesQuery, Result<Dictionary<Guid, string>>>
{
    protected ICategoryRepository _categoryRepository { get; }

    public GetOwnTitleRecordCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<Dictionary<Guid, string>>> Handle(GetOwnTitleRecordCategoriesQuery request, CancellationToken cancellationToken)
    {
        Dictionary<Guid, string> modelCategories = new();

        var categories = await _categoryRepository.Get(_ => request.CategoryDictionary.Values.Contains(_.Id)).ToListAsync(cancellationToken);

        foreach (var dictionary in request.CategoryDictionary)
        {
            if (dictionary.Value.Equals(Guid.Empty))
            {
                modelCategories.Add(dictionary.Key, string.Empty);
                continue;
            }

            string title = categories.FirstOrDefault(_ => _.Id == dictionary.Value).Title;

            modelCategories.Add(dictionary.Key, title ?? string.Empty);
        }

        return modelCategories;
    }
}