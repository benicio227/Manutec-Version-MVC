using Manutec.Core.Entities;
using Manutec.Core.Enums;

namespace Manutec.Application.Models.MaintenanceModel;
public class MaintenancesViewModel
{
    public MaintenancesViewModel(int id, MaintenanceType type, DateTime scheduledDate,
        int scheduledMileage, DateTime? performedDate, int? performedMileage, decimal cost, string description, string vehiclePlate, int vehicleId)
    {
        Id = id;
        Type = type;
        ScheduledDate = scheduledDate;
        ScheduledMileage = scheduledMileage;
        PerformedDate = performedDate;
        PerformedMileage = performedMileage;
        Cost = cost;
        Description = description;
        VehiclePlate = vehiclePlate;
        VehicleId = vehicleId;
        IsCompleted = false;
        IsDeleted = false;
    }

    public int Id {  get; private set; }
    public MaintenanceType Type { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public int ScheduledMileage { get; private set; }
    public DateTime? PerformedDate { get; private set; }
    public int? PerformedMileage { get; private set; }
    public decimal Cost { get; private set; }
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public bool IsDeleted { get; private set; }

    public int VehicleId { get; private set; }
    public string VehiclePlate { get; private set; }

    public static List<MaintenancesViewModel> FromEntity(List<Maintenance> maintenances)
    {
        return maintenances.Select(maintenance => new MaintenancesViewModel(maintenance.Id, maintenance.Type, maintenance.ScheduledDate, maintenance.ScheduledMileage, maintenance.PerformedDate, maintenance.PerformedMileage, maintenance.Cost, maintenance.Description, maintenance.Vehicle.LicensePlate, maintenance.VehicleId)).ToList();
    }
}
