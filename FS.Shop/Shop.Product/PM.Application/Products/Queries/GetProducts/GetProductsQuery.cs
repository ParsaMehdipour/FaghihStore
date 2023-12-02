using MediatR;

using PM.Application.Criteria;

using SH.Infrastructure.Criteria;

namespace PM.Application.Products.Queries.GetProducts;
public record GetProductsQuery(ProductQueryStringParameters Parameters) : IRequest<Result<ResponseModel<IEnumerable<GetProductsDto>, ProductQueryStringParameters>>>;

public record GetProductsDto(Guid Id, string PersianTitle, string CreateDate, string ThumbnailPicture, string Category, int DescriptionCount);