namespace Manutec.Application.Events;
using MediatR;
public class UpcomingMaintenanceNotification : INotification
{
    public string CustomerEmail { get; set; }
    public string VehicleModel { get; set; }
    public int RemainingKm { get; set; }
    public DateTime ScheduledDate { get; set; }
}
