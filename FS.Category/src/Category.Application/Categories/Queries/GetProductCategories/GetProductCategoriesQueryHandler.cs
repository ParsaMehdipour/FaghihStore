using Category.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Category.Application.Categories.Queries.GetProductCategories;

public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, Dictionary<Guid, string>>
{
    public GetProductCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    protected ICategoryRepository _categoryRepository { get; }

    public async Task<Dictionary<Guid, string>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        Dictionary<Guid, string> productCategories = new();

        var categories = await _categoryRepository.Get(_ => request.CategoryDictionary.Values.Contains(_.Id)).ToListAsync(cancellationToken);

        foreach (var dictionary in request.CategoryDictionary)
        {
            string title = categories.FirstOrDefault(_ => _.Id == dictionary.Value)!.Title;

            productCategories.Add(dictionary.Key, title);
        }

        return productCategories;
    }
}
