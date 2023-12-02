using _0_Framework.Application;

using System;
using System.Collections.Generic;

namespace PM.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture command);
        OperationResult Edit(EditProductPicture command);
        OperationResult Remove(Guid id);
        OperationResult Restore(Guid id);
        EditProductPicture GetDetails(Guid id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
