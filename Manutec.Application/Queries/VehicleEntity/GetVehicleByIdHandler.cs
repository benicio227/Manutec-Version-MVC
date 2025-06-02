using Manutec.Application.Models;
using Manutec.Application.Models.VehicleModel;
using Manutec.Core.Repositories;
using MediatR;

namespace Manutec.Application.Queries.VehicleEntity;
public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdQuery, ResultViewModel<GetByIdVehicleViewModel>>
{
    private readonly IVehicleRepository _vehicleRepository;

    public GetVehicleByIdHandler(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }
    public async Task<ResultViewModel<GetByIdVehicleViewModel>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(request.WorkShopId, request.VehicleId);

        if (vehicle == null)
            return ResultViewModel<GetByIdVehicleViewModel>.Error("Veículo não encontrado.");

        var model = GetByIdVehicleViewModel.FromEntity(vehicle);

        return ResultViewModel<GetByIdVehicleViewModel>.Success(model);
    }
}
