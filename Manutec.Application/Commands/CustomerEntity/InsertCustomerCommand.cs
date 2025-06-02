using Manutec.Application.Models;
using Manutec.Application.Models.CustomerModel;
using Manutec.Core.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Manutec.Application.Commands.CustomerEntity;
public class InsertCustomerCommand : IRequest<ResultViewModel<CustomerViewModel>>
{
    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    [Display(Name = "Nome")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório")]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Telefone é obrigatório")]
    [Display(Name = "Telefone")]
    public string Phone { get; set; }
    public int WorkShopId { get; set; }

    public Customer ToEntity()
    {
        return new Customer(WorkShopId, Name, Email, Phone);
    }
}
