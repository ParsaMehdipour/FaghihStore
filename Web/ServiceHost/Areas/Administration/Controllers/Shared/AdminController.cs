namespace ServiceHost.Areas.Administration.Controllers.Shared;

[Authorize]
[Area("Administration")]
public class AdminController : Controller
{
    protected IMediator _mediator { get; }

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public AdminController()
    {

    }
}
