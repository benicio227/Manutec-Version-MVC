using Manutec.Application.Models;
using Manutec.Application.Models.MaintenanceModel;
using Manutec.Core.Repositories;
using MediatR;

namespace Manutec.Application.Queries.MaintenanceEntity;
public class GetAllWorkShopMaintenancesHandler : IRequestHandler<GetAllWorkShopMaintenancesQuery, ResultViewModel<List<MaintenancesViewModel>>>
{
    private readonly IMaintenanceRepository _repository;

    public GetAllWorkShopMaintenancesHandler(IMaintenanceRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel<List<MaintenancesViewModel>>> Handle(GetAllWorkShopMaintenancesQuery request, CancellationToken cancellationToken)
    {
        var maintenances = await _repository.GetAllByWorkShopId(request.WorkShopId);

        if (maintenances is null || !maintenances.Any())
            return ResultViewModel<List<MaintenancesViewModel>>.Error("Nenhuma manutenção encontrada.");

        var model = MaintenancesViewModel.FromEntity(maintenances);

        return ResultViewModel<List<MaintenancesViewModel>>.Success(model);
    }
}
