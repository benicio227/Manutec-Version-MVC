using Manutec.Application.Commands.CustomerEntity;
using Manutec.Application.Models.CustomerModel;
using Manutec.Application.Queries.CustomerEntity;
using Manutec.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manutec.MVC.Controllers;

[Route("Customer")]
[Authorize]
public class CustomerController : Controller
{

    private readonly IMediator _mediator;
    private readonly ILoggedUser _loggedUser;

    public CustomerController(IMediator mediator, ILoggedUser loggedUser)
    {
        _mediator = mediator;
        _loggedUser = loggedUser;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        return RedirectToAction("List");
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpGet("List")]
    public async Task<IActionResult> List()
    {
        var query = new GetAllCustomerQuery
        {
            WorkShopId = _loggedUser.WorkShopId
        };

        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return View(new List<CustomerViewModel>());
        }

        return View(result.Data);
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InsertCustomerCommand command)
    {
        command.WorkShopId = _loggedUser.WorkShopId;

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message ?? "Erro ao cadastrar cliente.");
            return View(command);
        }

        TempData["SuccessMessage"] = "Cliente cadastrado com sucesso!";
        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _mediator.Send(new GetByIdCustomerQuery(_loggedUser.WorkShopId, id));

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message ?? "Cliente não encontrado.";
            return RedirectToAction("List");

        }
        return View(result.Data); 
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateCustomerCommand command)
    {
        if (!ModelState.IsValid)
            return View(command);

        command.Id = id;
        command.WorkShopId = _loggedUser.WorkShopId;

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message ?? "Erro ao editar cliente.");
            return View(command);
        }

        TempData["SuccessMessage"] = "Cliente editado com sucesso!";
        return RedirectToAction("List");
    }


    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var result = await _mediator.Send(new GetByIdCustomerQuery(_loggedUser.WorkShopId, id));

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message ?? "Cliente não encontrado.";
            return RedirectToAction("List");
        }

        return View(result.Data); 
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new GetByIdCustomerQuery(_loggedUser.WorkShopId, id));

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message ?? "Cliente não encontrado.";
            return RedirectToAction("List");
        }

        return View(result.Data);
    }

    [HttpPost("DeleteConfirmed/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _mediator.Send(new DeleteCustomerCommand(id, _loggedUser.WorkShopId));

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message ?? "Erro ao deletar cliente.";
            return RedirectToAction("List");
        }

        TempData["SuccessMessage"] = "Cliente excluído com sucesso!";
        return RedirectToAction("List");
    }

}

