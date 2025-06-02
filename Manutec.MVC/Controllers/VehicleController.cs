using Manutec.Application.Commands.VehicleEntity;
using Manutec.Application.Queries.VehicleEntity;
using Manutec.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manutec.Mvc.Controllers;

[Route("Vehicle")]
[Authorize]
public class VehicleController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILoggedUser _loggedUser;

    public VehicleController(IMediator mediator, ILoggedUser loggedUser)
    {
        _mediator = mediator;
        _loggedUser = loggedUser;
    }

    [HttpGet("/customers/{customerId}/vehicles")]
    public async Task<IActionResult> List(int customerId)
    {
        var query = new GetAllVehicleQuery
        {
            WorkShopId = _loggedUser.WorkShopId,
            CustomerId = customerId
        };

        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("List", "Customer");
        }
        ViewBag.CustomerId = customerId;
        return View(result.Data); 
    }

    [HttpGet("/customers/{customerId}/vehicles/register")]
    public IActionResult Register(int customerId)
    {
        ViewBag.CustomerId = customerId;
        return View(); 
    }

    [HttpPost("/customers/{customerId}/vehicles/register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(int customerId, InsertVehicleCommand command)
    {
        if (!ModelState.IsValid)
            return View(command);

        command.WorkShopId = _loggedUser.WorkShopId;
        command.CustomerId = customerId;

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View(command);
        }

        TempData["SuccessMessage"] = "Veículo registrado com sucesso!";
        return RedirectToAction("List", new { customerId });
    }

    [HttpGet("/customers/{customerId}/vehicles/edit/{id}")]
    public async Task<IActionResult> Edit(int customerId, int id)
    {
        var result = await _mediator.Send(new GetByIdVehicleQuery(id, customerId, _loggedUser.WorkShopId));

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message ?? "Veículo não encontrado.";
            return RedirectToAction("List", new { customerId });
        }

        ViewBag.CustomerId = customerId;
        return View(result.Data); 
    }

    [HttpPost("/customers/{customerId}/vehicles/edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int customerId, int id, UpdateVehicleCommand command)
    {
        if (!ModelState.IsValid)
            return View(command);

        command.Id = id;
        command.WorkShopId = _loggedUser.WorkShopId;
        command.CustomerId = customerId;

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View(command);
        }

        TempData["SuccessMessage"] = "Veículo atualizado com sucesso!";
        return RedirectToAction("List", new { customerId });
    }


    [HttpGet("/customers/{customerId}/vehicles/detail/{id}")]
    public async Task<IActionResult> Details(int customerId, int id)
    {
        var result = await _mediator.Send(new GetByIdVehicleQuery(id, customerId, _loggedUser.WorkShopId));

        if (!result.IsSuccess || result.Data == null)
        {
            TempData["ErrorMessage"] = result.Message ?? "Veículo não encontrado.";
            return RedirectToAction("List", new { customerId });
        }

        ViewBag.CustomerId = customerId;
        return View(result.Data);
    }

    [HttpGet("/customers/{customerId}/vehicles/delete/{id}")]
    public async Task<IActionResult> Delete(int customerId, int id)
    {
        var result = await _mediator.Send(new GetByIdVehicleQuery(id, customerId, _loggedUser.WorkShopId));

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message ?? "Veículo não encontrado.";
            return RedirectToAction("List", new { customerId });
        }

        ViewBag.CustomerId = customerId;
        return View(result.Data); 
    }

    [HttpPost("/customers/{customerId}/vehicles/delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int customerId, int id)
    {
        var result = await _mediator.Send(new DeleteVehicleCommand
        {
            Id = id,
            CustomerId = customerId,
            WorkShopId = _loggedUser.WorkShopId
        });

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message ?? "Erro ao excluir veículo.";
        }
        else
        {
            TempData["SuccessMessage"] = "Veículo excluído com sucesso!";
        }

        return RedirectToAction("List", new { customerId });
    }
}
