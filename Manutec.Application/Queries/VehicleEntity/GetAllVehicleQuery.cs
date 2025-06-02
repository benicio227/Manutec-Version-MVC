using Manutec.Application.Models;
using Manutec.Application.Models.VehicleModel;
using Manutec.Core.Entities;
using MediatR;

namespace Manutec.Application.Queries.VehicleEntity;
public class GetAllVehicleQuery : IRequest<ResultViewModel<List<VehicleAllViewModel>>>
{
    public int WorkShopId {  get; set; }
    public int CustomerId {  get; set; }
}
