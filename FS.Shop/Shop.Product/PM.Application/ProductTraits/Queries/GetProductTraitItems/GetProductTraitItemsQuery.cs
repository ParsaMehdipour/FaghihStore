using MediatR;

using PM.Application.Criteria;

using SH.Infrastructure.Criteria;

namespace PM.Application.ProductTraits.Queries.GetProductTraitItems;

public record GetProductTraitItemsQuery(Guid ProductId, ProductTraitQueryStringParameters Parameters) : IRequest<Result<ResponseModel<GetProductTraitItemsViewModel, ProductTraitQueryStringParameters>>>;

public record GetProductTraitItemsDto(Guid Id, string Value, string Trait, string TraitGroup, string CreateDate, bool IsDeleted);

public class GetProductTraitItemsViewModel
{
    public string ProductTitle { get; set; }
    public string ProductId { get; set; }
    public IEnumerable<GetProductTraitItemsDto> GetProductTraitItems { get; set; }
}