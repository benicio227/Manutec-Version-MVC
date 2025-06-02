using Manutec.Application.Commands.WorkShopEntity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Manutec.Mvc.Controllers;

public class WorkShopController : Controller
{
    private readonly IMediator _mediator;

    public WorkShopController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(); 
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(InsertWorkShopCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View(command);
        }

        TempData["SuccessMessage"] = "Oficina registrada com sucesso!";
        return RedirectToAction("Create", "Usuario");
    }
}
