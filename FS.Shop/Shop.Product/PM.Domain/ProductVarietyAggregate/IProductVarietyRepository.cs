using SH.Domain.Interfaces;

namespace PM.Domain.ProductVarietyAggregate;

public interface IProductVarietyRepository : IBaseRepository<ProductVariety>
{
    bool ProductVarietyExists();
}
