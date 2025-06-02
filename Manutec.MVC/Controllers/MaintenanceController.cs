using Manutec.Application.Commands.MaintenanceEntity;
using Manutec.Application.Models.MaintenanceModel;
using Manutec.Application.Queries.MaintenanceEntity;
using Manutec.Application.Queries.VehicleEntity;
using Manutec.Core.Repositories;
using Manutec.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Manutec.MVC.Controllers;


[Route("Maintenance")]
[Authorize]
public class MaintenanceController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILoggedUser _loggedUser;
    private readonly IVehicleRepository _vehicleRepository;

    public MaintenanceController(IMediator mediator, ILoggedUser loggedUser, IVehicleRepository vehicleRepository)
    {
        _mediator = mediator;
        _loggedUser = loggedUser;
        _vehicleRepository = vehicleRepository;

    }


    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var result = await _mediator.Send(new GetAllWorkShopMaintenancesQuery(_loggedUser.WorkShopId));

        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Message;
            return RedirectToAction("Error", "Home");
        }

        return View(result.Data);
    }

  
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id, int vehicleId)
    {
        var result = await _mediator.Send(new GetByIdMaintenanceQuery(_loggedUser.WorkShopId, vehicleId, id));

        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Message;
            return RedirectToAction("Index", new { vehicleId });
        }

        return View(result.Data);
    }


    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        var workShopId = _loggedUser.WorkShopId;

        var vehicles = await _vehicleRepository.GetAllByWorkShopId(workShopId); 

        ViewBag.Vehicles = new SelectList(vehicles, "Id", "LicensePlate");
        return View();
    }


    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InsertMaintenanceCommand command)
    {
        command.WorkShopId = _loggedUser.WorkShopId;

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);


            var vehicles = await _vehicleRepository.GetAllByWorkShopId(_loggedUser.WorkShopId);
            ViewBag.Vehicles = new SelectList(vehicles, "Id", "LicensePlate"); 

            return View(command);
        }

        return RedirectToAction("Index", new { vehicleId = command.VehicleId });
    }

 
    [HttpPost("Complete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete(int id, int vehicleId)
    {
        var command = new UpdateMaintenanceStatusCompletedCommand
        {
            Id = id,
            WorkShopId = _loggedUser.WorkShopId,
            VehicleId = vehicleId
        };

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Message;
        }

        return RedirectToAction(nameof(Details), new { id, vehicleId });
    }

    [HttpGet("by-vehicle")]
    public async Task<IActionResult> ByVehicle(int vehicleId)
    {
        var result = await _mediator.Send(new GetMaintenanceByVehicleQuery(_loggedUser.WorkShopId, vehicleId));

        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Message;
            return RedirectToAction("Error", "Home");
        }

        var vehicleResult = await _mediator.Send(new GetVehicleByIdQuery(vehicleId, _loggedUser.WorkShopId));

        var vehicle = vehicleResult.Data;

        var viewModel = new MaintenancesByVehicleViewModel
        {
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            Year = vehicle.Year,
            LicensePlate = vehicle.LicensePlate,
            CurrentMileage = vehicle.CurrentMileage,
            ToleranceKm = vehicle.ToleranceKm,
        };



        ViewBag.VehicleId = vehicleId;
        return View("ByVehicle", viewModel);
    }
}
