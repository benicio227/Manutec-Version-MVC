namespace Manutec.Application.Models.MaintenanceModel;
public class MaintenancesByVehicleViewModel
{
    public int VehicleId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public int CurrentMileage { get; set; }
    public int ToleranceKm { get; set; }

    public List<MaintenanceViewModel> Maintenances { get; set; } = new();
}
