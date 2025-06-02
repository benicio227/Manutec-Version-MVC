using Manutec.Application.Commands.UserEntity;
using Manutec.Application.Models.UserModel;
using Manutec.Application.Queries.UserEntity;
using Manutec.Core.Repositories;
using Manutec.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Manutec.MVC.Controllers;

[Route("Usuario")]
[AllowAnonymous]
public class UserController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILoggedUser _loggedUser;
    private readonly IWorkShopRepository _workShopRepository;
    public UserController(IMediator mediator, ILoggedUser loggedUser, IWorkShopRepository workShopRepository)
    {
        _mediator = mediator;
        _loggedUser = loggedUser;
        _workShopRepository = workShopRepository;

    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create(bool returnToList = false)
    {
        var workShops = await _workShopRepository.GetAllAsync();
        ViewBag.WorkShops = new SelectList(workShops, "Id", "Name");


        ViewBag.ReturnToList = returnToList;
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InsertUserCommand command, bool returnToList = false)
    {
        try
        {

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Erro ao cadastrar.");
                return View(command);
            }

            TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";

            if (returnToList)
            {
                return RedirectToAction("Listar");
            }

            return RedirectToAction("Login", "LoginUsuario");
        }
        catch (Exception ex)
        {
          
            ModelState.AddModelError(string.Empty, $"Erro inesperado: {ex.Message}");

      
            Console.WriteLine(ex); 

            return View(command);
        }

    }

    [HttpGet("Listar")]
    public async Task<IActionResult> Listar()
    {
        var query = new GetAllUserQuery { WorkShopId = _loggedUser.WorkShopId };
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return View(new List<GetAllUserViewModel>());
        }

        return View(result.Data);
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var result = await _mediator.Send(new GetByIdUserQuery(_loggedUser.WorkShopId, id));

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("Listar");
        }

        return View(result.Data);
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _mediator.Send(new GetByIdUserQuery(_loggedUser.WorkShopId, id));

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("Listar");
        }

        var user = result.Data;

        var command = new UpdateUserCommand
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Phone = user.Phone,
            Role = user.Role
        };

        return View(command); 
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateUserCommand command)
    {
        try
        {
            if (id != command.Id)
                return BadRequest();

            command.WorkShopId = _loggedUser.WorkShopId;

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(command);
            }

            TempData["SuccessMessage"] = "Usuário atualizado com sucesso!";
            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
           
            ModelState.AddModelError(string.Empty, $"Erro: {ex.Message}");
            Console.WriteLine(ex); 
            return View(command);
        }
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new GetByIdUserQuery(_loggedUser.WorkShopId, id));

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("Listar");
        }

        return View(result.Data);
    }

    [HttpPost("Delete/{id}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id, _loggedUser.WorkShopId));

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("Listar");
        }

        TempData["SuccessMessage"] = "Usuário removido com sucesso!";
        return RedirectToAction("Listar");
    }


}

