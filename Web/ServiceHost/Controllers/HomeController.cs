using ServiceHost.Controllers.Base;

namespace ServiceHost.Controllers;

public class HomeController : BaseController
{
    public HomeController(IMediator mediator) : base(mediator)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}