using Manutec.Application.Models.VehicleModel;
using Manutec.Application.Models;
using MediatR;

namespace Manutec.Application.Queries.VehicleEntity;
public class GetVehicleByIdQuery : IRequest<ResultViewModel<GetByIdVehicleViewModel>>
{
    public GetVehicleByIdQuery(int vehicleId, int workShopId)
    {
        VehicleId = vehicleId;
        WorkShopId = workShopId;
    }

    public int VehicleId { get; }
    public int WorkShopId { get; }
}
