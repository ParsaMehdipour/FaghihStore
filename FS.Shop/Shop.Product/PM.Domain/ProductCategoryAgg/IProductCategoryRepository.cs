using _0_Framework.Domain;

using PM.Application.Contracts.ProductCategory;

using System;
using System.Collections.Generic;

namespace PM.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<Guid, ProductCategory>
    {
        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategory GetDetails(Guid id);
        string GetSlugById(Guid id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
