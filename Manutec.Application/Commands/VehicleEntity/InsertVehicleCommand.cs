using Manutec.Application.Models;
using Manutec.Application.Models.VehicleModel;
using Manutec.Core.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Manutec.Application.Commands.VehicleEntity;
public class InsertVehicleCommand : IRequest<ResultViewModel<VehicleViewModel>>
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int WorkShopId { get; set; }

    [Display(Name = "Marca")]
    public string Brand { get; set; }

    [Display(Name = "Modelo")]
    public string Model { get; set; }

    [Display(Name = "Ano")]
    public int Year { get; set; }

    [Display(Name = "Placa")]
    public string LicensePlate { get; set; }

    [Display(Name = "Quilometragem Atual")]
    public int CurrentMileage { get; set; }

    [Display(Name = "Tolerância em Km")]
    public int ToleranceKm {  get; set; }

    public Vehicle ToEntity()
    {
        return new Vehicle(CustomerId, WorkShopId, Brand, Model, Year, LicensePlate, CurrentMileage, ToleranceKm);
    }
}
