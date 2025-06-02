using Manutec.Application.Models;
using Manutec.Application.Models.MaintenanceModel;
using Manutec.Core.Entities;
using Manutec.Core.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Manutec.Application.Commands.MaintenanceEntity;
public class InsertMaintenanceCommand : IRequest<ResultViewModel<InsertMaintenanceViewModel>>
{

    [Display(Name = "Selecionar Veículo")]
    public int VehicleId { get;  set; }
    public int WorkShopId { get; set; }

    [Display(Name = "Tipo de Manutenção")]
    public MaintenanceType Type { get; set; }

    [Required(ErrorMessage = "O campo Data Agendada é obrigatório")]
    [Display(Name = "Data Agendada")]
    public DateTime ScheduledDate { get; set; }

    [Required(ErrorMessage = "O campo Quilometragem Agendada é obrigatório")]
    [Display(Name = "Quilometragem Agendada")]
    public int ScheduledMileage { get; set; }

    [Display(Name = "Data Realizada")]
    public DateTime? PerformedDate { get; set; }

    [Display(Name = "Quilometragem Realizada")]
    public int? PerformedMileage { get; set; }

    [Required(ErrorMessage = "O campo Custo é obrigatório")]
    [Display(Name = "Custo")]
    public decimal Cost { get; set; }

    [Display(Name = "Descrição")]
    public string Description { get; set; }

    public Maintenance ToEntity()
    {
        return new Maintenance(VehicleId, WorkShopId, Type, ScheduledDate, ScheduledMileage, PerformedDate, PerformedMileage, Cost, Description);
    }
}
