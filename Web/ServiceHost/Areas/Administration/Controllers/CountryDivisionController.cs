using CD.Application.CountryDivisions.Commands.CreateCountryDivision;
using CD.Application.CountryDivisions.Commands.DeleteCountryDivision;
using CD.Application.CountryDivisions.Commands.EditCountryDivision;
using CD.Application.CountryDivisions.Queries.GetCountryDivision;
using CD.Application.CountryDivisions.Queries.GetCountryDivisions;
using CD.Application.CountryDivisions.Queries.GetParentCountryDivisions;
using CD.Application.Criteria;
using CD.Infrastructure.EfCore;

namespace ServiceHost.Areas.Administration.Controllers;

[Authorize(Policy = CountryDivisionManagementPermissionExposer.Permissions.COUNTRYDIVISION_FullManagement)]
public class CountryDivisionController : AdminController
{
    public CountryDivisionController(IMediator mediator) : base(mediator)
    {
    }

    public async Task<IActionResult> Index([FromQuery] CountryDivisionQueryString parameters, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCountryDivisionsQuery(parameters), cancellationToken);

        return View(response.Value);
    }

    public async Task<IActionResult> Create()
    {
        var response = await _mediator.Send(new GetParentCountryDivisionsQuery());
        ViewBag.ParentLinks = new SelectList(response.Value, "Id", "Name");

        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCountryDivisionCommand command, CancellationToken cancellationToken)
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

    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var countryDivision = await _mediator.Send(new GetCountryDivisionQuery(id), cancellationToken);

        var parentCountryDivisions = await _mediator.Send(new GetParentCountryDivisionsQuery(), cancellationToken);
        ViewBag.ParentLinks = new SelectList(parentCountryDivisions.Value, "Id", "Name", countryDivision.Value.Id);

        return PartialView(countryDivision.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditCountryDivisionCommand command, CancellationToken cancellationToken)
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
        var result = await _mediator.Send(new DeleteCountryDivisionCommand(id, false), cancellationToken);

        if (result.IsFailed)
        {
            //TODO: should be there a toastify or sweetAlert message for show
            return NotFound(result.Errors.First()); //TODO: this code is temporary
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteCountryDivisionCommand(id, true), cancellationToken);

        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("error", error.Message);
        }

        return View("Index");
    }
}
