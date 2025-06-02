using Manutec.Application.Models;
using Manutec.Application.Models.CustomerModel;
using Manutec.Core.Repositories;
using MediatR;

namespace Manutec.Application.Queries.CustomerEntity;
public class GetByIdCustomerHandler : IRequestHandler<GetByIdCustomerQuery, ResultViewModel<GetByIdCustomerViewModel>>
{
    private readonly ICustomerRepository _customerRepository;
    public GetByIdCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<ResultViewModel<GetByIdCustomerViewModel>> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetById(request.WorkShopId, request.Id);

        if (customer is null)
        {
            return ResultViewModel<GetByIdCustomerViewModel>.Error("Nenhum cliente encontrado");
        }

        var model = GetByIdCustomerViewModel.FromEntity(customer);

        return ResultViewModel<GetByIdCustomerViewModel>.Success(model);
    }
}
