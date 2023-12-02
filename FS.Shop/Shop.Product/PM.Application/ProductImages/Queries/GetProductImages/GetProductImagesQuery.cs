using MediatR;

using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Searching;

using System.Collections.ObjectModel;

namespace PM.Application.ProductImages.Queries.GetProductImages;

public record GetProductImagesQuery(Guid ProductId, QueryStringParameters Parameters) : IRequest<Result<ResponseModel<GetProductImagesViewModel>>>;

public record GetProductImagesDto(Guid Id, string Url, string Alt, string Title, string CreateDate, bool IsThumbnail);

public class GetProductImagesViewModel
{
    public ReadOnlyCollection<GetProductImagesDto> Images { get; set; }
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; }
}