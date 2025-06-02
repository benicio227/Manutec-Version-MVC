using Manutec.Application.Models;
using Manutec.Application.Models.CustomerModel;
using Manutec.Core.Repositories;
using MediatR;

namespace Manutec.Application.Queries.CustomerEntity;
public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, ResultViewModel<List<GetAllCustomerViewModel>>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<ResultViewModel<List<GetAllCustomerViewModel>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllByWorkShopId(request.WorkShopId);

        var model = GetAllCustomerViewModel.FromEntity(customers);

        return ResultViewModel<List<GetAllCustomerViewModel>>.Success(model);
    }
}
