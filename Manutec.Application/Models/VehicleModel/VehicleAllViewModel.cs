using Manutec.Core.Entities;

namespace Manutec.Application.Models.VehicleModel;
public class VehicleAllViewModel
{
    public VehicleAllViewModel(int id, string brand, string model, int year, string licensePlate, int currentMileage, int toleranceKm)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        LicensePlate = licensePlate;
        CurrentMileage = currentMileage;
        ToleranceKm = toleranceKm;
    }

    public int Id { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }
    public string LicensePlate { get; private set; }
    public int CurrentMileage { get; private set; }
    public int ToleranceKm { get; private set; }

    public static List<VehicleAllViewModel> FromEntity(List<Vehicle> vehicles)
    {
        return vehicles.Select(vehicle => new VehicleAllViewModel(vehicle.Id, vehicle.Brand, vehicle.Model, vehicle.Year, vehicle.LicensePlate, vehicle.CurrentMileage, vehicle.ToleranceKm)).ToList();
    }
}
