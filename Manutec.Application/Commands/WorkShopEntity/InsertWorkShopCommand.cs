using Manutec.Application.Models;
using Manutec.Application.Models.WorkShopModel;
using Manutec.Core.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Manutec.Application.Commands.WorkShopEntity;
public class InsertWorkShopCommand : IRequest<ResultViewModel<WorkShopViewModelId>>
{
    [Required(ErrorMessage = "O campo Nome da Oficina é obrigatório")]
    [Display(Name = "Nome da oficina")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório")]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Telefone é obrigatório")]
    [Display(Name = "Telefone")]
    public string Phone { get; set; }

    public WorkShop ToEntity()
    {
        return new WorkShop(Name, Email, Phone);
    }
}
