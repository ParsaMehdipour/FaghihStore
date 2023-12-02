using Category.Application.Categories.Queries.GetCategories;
using Category.Application.Categories.Queries.GetCategoriesByParent;

using PB.Application.Brands.Queries.GetBrands;

using PM.Application.Criteria;
using PM.Application.ProductDescriptions.Commands.CreateProductDescription;
using PM.Application.ProductDescriptions.Commands.EditProductDescription;
using PM.Application.ProductDescriptions.Queries.GetProductDescription;
using PM.Application.ProductDescriptions.Queries.GetProductDescriptions;
using PM.Application.Products.Commands.CreateProduct;
using PM.Application.Products.Commands.EditProduct;
using PM.Application.Products.Queries.GetProduct;
using PM.Application.Products.Queries.GetProducts;
using PM.Infrastructure.EFCore;

namespace ServiceHost.Areas.Administration.Controllers;

public class ProductController : AdminController
{
    public ProductController(IMediator mediator) : base(mediator)
    {
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCT_View)]
    public async Task<IActionResult> Index([FromQuery] ProductQueryStringParameters parameters, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductsQuery(parameters), cancellationToken);
        var categoriesResponse = await _mediator.Send(new GetCategoriesQuery(new()));
        ViewBag.Categories = new SelectList(categoriesResponse.Value.Model, "Id", "Title");

        return View(response.Value);
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCT_Create)]
    public async Task<IActionResult> Create()
    {
        var categoriesResponse = await _mediator.Send(new GetCategoriesByParentQuery(Guid.Empty));
        var brandsResponse = await _mediator.Send(new GetBrandsQuery(new()));

        ViewBag.Categories = new SelectList(categoriesResponse.Value.Model.Children, "Id", "Title");
        ViewBag.Brands = new SelectList(brandsResponse.Value.Model, "Id", "Title");

        return PartialView();
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCT_Create)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailed)
            return Json(new
            {
                isSuccess = false,
                messages = result.Errors.Select(_ => _.Message).ToList(),
            });

        return Json(result);
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCT_Edit)]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var product = await _mediator.Send(new GetProductQuery(id), cancellationToken);

        var categoriesResponse = await _mediator.Send(new GetCategoriesByParentQuery(Guid.Empty), cancellationToken);
        var brandsResponse = await _mediator.Send(new GetBrandsQuery(new()));

        ViewBag.Categories = new SelectList(categoriesResponse.Value.Model.Children, "Id", "Title", product.Value.CategoryId);
        ViewBag.Brands = new SelectList(brandsResponse.Value.Model, "Id", "Title", product.Value.BrandId);

        return PartialView(product.Value);
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCT_Edit)]
    [HttpPost]
    public async Task<IActionResult> Edit(EditProductCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailed)
            return Json(new
            {
                isSuccess = false,
                messages = result.Errors.Select(_ => _.Message).ToList(),
            });

        return Json(result);
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCTDESCRIPTION_Create)]
    public IActionResult CreateDescription(Guid productId)
    {
        CreateProductDescriptionCommand command = new(null, null, productId);

        return View(command);
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCTDESCRIPTION_Create)]
    [HttpPost]
    public async Task<IActionResult> CreateDescription(CreateProductDescriptionCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsFailed)
        {
            ModelState.AddModelError(string.Empty, response.Errors.Select(_ => _.Message).FirstOrDefault());
            return View(command);
        }

        return RedirectToAction("Index");
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCTDESCRIPTION_View)]
    public async Task<IActionResult> Descriptions(Guid productId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductDescriptionsQuery(productId), cancellationToken);

        return View(response.Value);
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCTDESCRIPTION_Edit)]
    public async Task<IActionResult> EditProductDescription(Guid productDescriptionId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductDescriptionQuery(productDescriptionId), cancellationToken);

        return View(response.Value);
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCTDESCRIPTION_Edit)]
    [HttpPost]
    public async Task<IActionResult> EditProductDescription(EditProductDescriptionCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsFailed)
        {
            ModelState.AddModelError(string.Empty, response.Errors.Select(_ => _.Message).FirstOrDefault());
            return View(command);
        }

        return RedirectToAction("Descriptions", new { productId = command.ProductId });
    }

    [HttpGet("/api/category/[action]/{parentId?}")]
    public async Task<IActionResult> GetCategoriesByParent(Guid parentId, CancellationToken cancellationToken)
    {
        var categories = await _mediator.Send(new GetCategoriesByParentQuery(parentId), cancellationToken);

        if (categories.IsFailed)
            return Json(new
            {
                isSuccess = false,
                messages = categories.Errors.Select(_ => _.Message).ToList(),
            });

        return Ok(categories.Value.Model);
    }
}