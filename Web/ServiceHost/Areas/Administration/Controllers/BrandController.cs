using PB.Application.Brands.Commands.CreateBrand;
using PB.Application.Brands.Commands.DeleteBrand;
using PB.Application.Brands.Commands.EditBrand;
using PB.Application.Brands.Queries.GetBrand;
using PB.Application.Brands.Queries.GetBrands;
using PB.Application.Criteria;
using PB.Infrastructure.EfCore;

namespace ServiceHost.Areas.Administration.Controllers;

[Authorize(Policy = ProductBrandManagementPermissionExposer.Permissions.PRODUCTBRANDS_FullManagement)]
public class BrandController : AdminController
{
    public BrandController(IMediator mediator) : base(mediator)
    {

    }

    public async Task<IActionResult> Index([FromQuery] BrandQueryStringParameters parameters, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetBrandsQuery(parameters), cancellationToken);

        return View(response.Value);
    }

    public IActionResult Create()
    {
        return PartialView("Create");
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBrandCommand command, CancellationToken cancellationToken)
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

    public async Task<IActionResult> Edit(Guid Id, CancellationToken cancellationToken)
    {
        var model = await _mediator.Send(new GetBrandQuery(Id), cancellationToken);

        return PartialView("Edit", model.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditBrandCommand command, CancellationToken cancellationToken)
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

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBrandCommand(id, false), cancellationToken);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBrandCommand(id, true), cancellationToken);

        return RedirectToAction("Index", new BrandQueryStringParameters() { IsDeleted = true });
    }
}
