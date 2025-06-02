using Manutec.Application.Models;
using Manutec.Application.Models.MaintenanceModel;
using MediatR;

namespace Manutec.Application.Queries.MaintenanceEntity;
public class GetMaintenanceByVehicleQuery : IRequest<ResultViewModel<List<MaintenancesViewModel>>>
{
    public GetMaintenanceByVehicleQuery(int workShopId, int vehicleId)
    {
        WorkShopId = workShopId;
        VehicleId = vehicleId;
    }

    public int WorkShopId { get; }
    public int VehicleId { get; }
}
