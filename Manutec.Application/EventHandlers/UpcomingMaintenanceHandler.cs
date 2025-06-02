using Manutec.Application.Events;
using MediatR;


namespace Manutec.Application.EventHandlers;
public class UpcomingMaintenanceHandler : INotificationHandler<UpcomingMaintenanceNotification>
{
    public Task Handle(UpcomingMaintenanceNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"🔔 Enviar e-mail para {notification.CustomerEmail}:");
        Console.WriteLine($"Manutenção do veículo {notification.VehicleModel} agendada para {notification.ScheduledDate:d}, restam {notification.RemainingKm} km.");

        return Task.CompletedTask;
    }
}