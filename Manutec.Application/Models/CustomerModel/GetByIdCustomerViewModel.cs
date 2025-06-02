using Manutec.Core.Entities;

namespace Manutec.Application.Models.CustomerModel;
public class GetByIdCustomerViewModel
{
    public GetByIdCustomerViewModel(int id, string name, string email, string phone)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }

    public static GetByIdCustomerViewModel FromEntity(Customer customer)
    {
        return new GetByIdCustomerViewModel(customer.Id, customer.Name, customer.Email, customer.Phone);
    }
}
