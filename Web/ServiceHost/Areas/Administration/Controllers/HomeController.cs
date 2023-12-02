namespace ServiceHost.Areas.Administration.Controllers;
public class HomeController : AdminController
{
    public IActionResult Index()
    {
        return View();
    }
}
