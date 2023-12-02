using PM.Application.Criteria;
using PM.Application.ProductImages.Commands.CreateProductImage;
using PM.Application.ProductImages.Commands.DeleteProductImage;
using PM.Application.ProductImages.Commands.SetThumbnailProductImage;
using PM.Application.ProductImages.Queries.GetProductImage;
using PM.Application.ProductImages.Queries.GetProductImages;
using PM.Application.ProductImages.Queries.GetThumbnailProductImage;

namespace ServiceHost.Areas.Administration.Controllers;

public class ProductImageController : AdminController
{
    public ProductImageController(IMediator mediator) : base(mediator)
    {

    }

    public async Task<IActionResult> Index(Guid productId, ProductImageQueryStringParameters parameters, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductImagesQuery(productId, parameters), cancellationToken);

        if (response.IsFailed)
            return NotFound();

        return View(response.Value);
    }

    public IActionResult Create(Guid productId)
    {
        return PartialView(new CreateProductImageCommand(productId, null));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductImageCommand command, CancellationToken cancellationToken)
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

    public async Task<IActionResult> SetThumbnail(Guid id, Guid productId)
    {
        var productImage = await _mediator.Send(new GetProductImageQuery(id, productId));
        if (productImage.IsFailed)
            return NotFound();

        var thumbnailProductImage = await _mediator.Send(new GetThumbnailProductImageQuery(productId));

        if (thumbnailProductImage.IsFailed)
            return NotFound();

        return PartialView(productImage.Value.ToCommand(thumbnailProductImage.Value));
    }

    [HttpPost]
    public async Task<IActionResult> SetThumbnail(SetThumbnailProductImageCommand command, CancellationToken cancellationToken)
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

    public async Task<IActionResult> Delete(Guid id, Guid productId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteProductImageCommand(id, productId), cancellationToken);

        if (result.IsFailed)
            return NotFound(result.Errors.First());

        return RedirectToAction("Index", new { productId });
    }
}