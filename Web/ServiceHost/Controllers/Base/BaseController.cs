namespace ServiceHost.Controllers.Base;

[AllowAnonymous]
public class BaseController : Controller
{
    protected IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}