using Manutec.Application.Models;
using Manutec.Application.Models.MaintenanceModel;
using Manutec.Core.Repositories;
using MediatR;

namespace Manutec.Application.Queries.MaintenanceEntity;
public class GetAllMaintenanceHandler : IRequestHandler<GetAllMaintenanceQuery, ResultViewModel<List<MaintenancesViewModel>>>
{
    private readonly IMaintenanceRepository _maintenanceRepository;

    public GetAllMaintenanceHandler(IMaintenanceRepository maintenanceRepository)
    {
        _maintenanceRepository = maintenanceRepository;
    }
    public async Task<ResultViewModel<List<MaintenancesViewModel>>> Handle(GetAllMaintenanceQuery request, CancellationToken cancellationToken)
    {
        var maintenances = await _maintenanceRepository.GetAllByWorkShopIdAndVehicleId(request.WorkShopId, request.VehicleId);

        if (maintenances is null)
        {
            return ResultViewModel<List<MaintenancesViewModel>>.Error("Nenhuma manutenção encontrada");
        }

        var model = MaintenancesViewModel.FromEntity(maintenances);

        return ResultViewModel<List<MaintenancesViewModel>>.Success(model);
    }
}
