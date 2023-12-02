using FaghihstoreQuery.Models.Product.QueryModel;
using FaghihstoreQuery.Models.Product.SerachModel;
using FluentResults;
using SH.Infrastructure.Criteria;

namespace FaghihstoreQuery.Interfaces;

public interface IProductQuery
{
    Task<Result<ResponseModel<IEnumerable<ProductQueryModel>, ProductSearchModel>>> GetAsync(ProductSearchModel searchModel, CancellationToken cancellationToken);
    Task<Result<SingleProductQueryModel>> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
}
