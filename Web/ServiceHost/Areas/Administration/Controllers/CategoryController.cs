using Category.Application.Categories.Commands.CreateCategory;
using Category.Application.Categories.Commands.DeleteCategory;
using Category.Application.Categories.Commands.EditCategory;
using Category.Application.Categories.Queries.GetCategories;
using Category.Application.Categories.Queries.GetCategory;
using Category.Application.Criteria;
using Category.InfrastructureEfCore;

namespace ServiceHost.Areas.Administration.Controllers;

public class CategoryController : AdminController
{
    public CategoryController(IMediator mediator) : base(mediator)
    {
    }

    [Authorize(Policy = CategoryManagementPermissionExposer.Permissions.CATEGORY_View)]
    public async Task<IActionResult> Index([FromQuery] CategoryQueryStringParameters parameters, CancellationToken cancellationToken)
    {
        ViewBag.ParentId = parameters.ParentId;

        var response = await _mediator.Send(new GetCategoriesQuery(parameters), cancellationToken);

        return View(response.Value);
    }

    [Authorize(Policy = CategoryManagementPermissionExposer.Permissions.CATEGORY_Create)]
    public IActionResult Create(Guid? parentId)
    {
        ViewBag.ParentId = parentId;

        return PartialView();
    }

    [Authorize(Policy = CategoryManagementPermissionExposer.Permissions.CATEGORY_Create)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
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

    [Authorize(Policy = CategoryManagementPermissionExposer.Permissions.CATEGORY_Edit)]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var category = await _mediator.Send(new GetCategoryQuery(id), cancellationToken);

        return PartialView(category.Value);
    }

    [Authorize(Policy = CategoryManagementPermissionExposer.Permissions.CATEGORY_Edit)]
    [HttpPost]
    public async Task<IActionResult> Edit(EditCategoryCommand command, CancellationToken cancellationToken)
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

    [Authorize(Policy = CategoryManagementPermissionExposer.Permissions.CATEGORY_Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(id, false), cancellationToken);

        if (result.IsFailed)
        {
            //TODO: should be there a toastify or sweetAlert message for show
            return NotFound(result.Errors.First()); //TODO: this code is temporary
        }

        return RedirectToAction("Index");
    }
}
