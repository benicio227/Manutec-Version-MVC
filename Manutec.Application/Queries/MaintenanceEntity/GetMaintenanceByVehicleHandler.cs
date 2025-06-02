using Manutec.Application.Models;
using Manutec.Application.Models.MaintenanceModel;
using Manutec.Core.Repositories;
using MediatR;

namespace Manutec.Application.Queries.MaintenanceEntity;
public class GetMaintenanceByVehicleHandler : IRequestHandler<GetMaintenanceByVehicleQuery, ResultViewModel<List<MaintenancesViewModel>>>
{
    private readonly IMaintenanceRepository _repository;

    public GetMaintenanceByVehicleHandler(IMaintenanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<List<MaintenancesViewModel>>> Handle(GetMaintenanceByVehicleQuery request, CancellationToken cancellationToken)
    {
        var maintenances = await _repository.GetAllByWorkShopIdAndVehicleId(request.WorkShopId, request.VehicleId);

        var viewModels = MaintenancesViewModel.FromEntity(maintenances);

        return ResultViewModel<List<MaintenancesViewModel>>.Success(viewModels);
    }
}
