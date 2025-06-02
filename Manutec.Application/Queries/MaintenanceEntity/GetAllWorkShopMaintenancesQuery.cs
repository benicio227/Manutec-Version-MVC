using Manutec.Application.Models;
using Manutec.Application.Models.MaintenanceModel;
using MediatR;

namespace Manutec.Application.Queries.MaintenanceEntity;
public class GetAllWorkShopMaintenancesQuery : IRequest<ResultViewModel<List<MaintenancesViewModel>>>
{
    public int WorkShopId { get; set; }

    public GetAllWorkShopMaintenancesQuery(int workShopId)
    {
        WorkShopId = workShopId;
    }
}
