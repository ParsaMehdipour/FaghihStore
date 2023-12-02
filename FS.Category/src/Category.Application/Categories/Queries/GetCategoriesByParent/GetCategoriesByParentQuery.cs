using FluentResults;

using MediatR;

using SH.Infrastructure.Criteria;

namespace Category.Application.Categories.Queries.GetCategoriesByParent;

public record GetCategoriesByParentQuery(Guid ParentId) : IRequest<Result<ResponseModel<GetCategoriesByParentViewModel>>>;

public record GetCategoryDto(Guid Id, string Title);

public class GetCategoriesByParentViewModel
{
    public Guid ParentId { get; set; }
    public IEnumerable<GetCategoryDto> Children { get; set; }
}