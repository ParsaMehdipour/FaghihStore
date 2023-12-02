using FaghihstoreQuery.Interfaces;
using FaghihstoreQuery.Models.Product.QueryFilter;
using FaghihstoreQuery.Models.Product.SerachModel;
using PB.Application.Brands.Queries.GetBrands;
using ServiceHost.Controllers.Base;

namespace ServiceHost.Controllers;
public class ProductsController : BaseController
{
    protected IProductQuery _productQuery { get; }

    public ProductsController(IProductQuery productQuery, IMediator mediator) : base(mediator)
    {
        _productQuery = productQuery;
    }

    public async Task<IActionResult> Index(ProductSearchModel searchModel)
    {
        var brandsResponse = await _mediator.Send(new GetBrandsQuery(new()), CancellationToken.None);
        ViewBag.Brands = brandsResponse.Value.Model.Select(_ => new BrandQueryFilter { Id = _.Id, Title = _.Title });

        var result = await _productQuery.GetAsync(searchModel, CancellationToken.None);

        return View(result.Value);
    }

    [HttpGet("[controller]/{id}")]
    public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productQuery.GetByIdAsync(id, cancellationToken);

        return View(result.Value);
    }
}
