using Manutec.Application.Models;
using Manutec.Application.Models.CustomerModel;
using Manutec.Core.Entities;
using MediatR;

namespace Manutec.Application.Queries.CustomerEntity;
public class GetAllCustomerQuery : IRequest<ResultViewModel<List<GetAllCustomerViewModel>>>
{
    public int WorkShopId {  get; set; }
}
