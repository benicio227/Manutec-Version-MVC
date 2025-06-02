using Manutec.Application.Models;
using Manutec.Application.Models.VehicleModel;
using Manutec.Core.Repositories;
using MediatR;

namespace Manutec.Application.Queries.VehicleEntity;
public class GetAllVehicleHandler : IRequestHandler<GetAllVehicleQuery, ResultViewModel<List<VehicleAllViewModel>>>
{
    private readonly IVehicleRepository _vehicleRepository;

    public GetAllVehicleHandler(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }
    public async Task<ResultViewModel<List<VehicleAllViewModel>>> Handle(GetAllVehicleQuery request, CancellationToken cancellationToken)
    {
        var vehicles = await _vehicleRepository.GetAllByWorkShopIdAndCustomerId(request.WorkShopId, request.CustomerId);

        if (vehicles is null)
        {
            return ResultViewModel<List<VehicleAllViewModel>>.Error("Nenhum veículo encontrado");
        }

        var model = VehicleAllViewModel.FromEntity(vehicles);

        return ResultViewModel<List<VehicleAllViewModel>>.Success(model);
    }
}
