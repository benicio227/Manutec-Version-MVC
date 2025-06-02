using Manutec.Application.Models;
using Manutec.Application.Models.CustomerModel;
using Manutec.Core.Repositories;
using MediatR;

namespace Manutec.Application.Commands.CustomerEntity;
public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, ResultViewModel<GetByIdCustomerViewModel>>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<ResultViewModel<GetByIdCustomerViewModel>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetById(request.WorkShopId, request.Id);

        if (customer is null)
        {
            return ResultViewModel<GetByIdCustomerViewModel>.Error("Cliente não encontrado");
        }

        if (customer.IsDeleted)
        {
            return ResultViewModel<GetByIdCustomerViewModel>.Error("Cliente já foi excluído.");
        }

        await _customerRepository.Delete(customer);

        var model = GetByIdCustomerViewModel.FromEntity(customer);

        return ResultViewModel<GetByIdCustomerViewModel>.Success(model);
    }
}
