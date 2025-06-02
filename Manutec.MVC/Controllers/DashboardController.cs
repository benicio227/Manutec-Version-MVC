using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manutec.MVC.Controllers;

[Route("Dashboard")]
[Authorize]
public class DashboardController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
