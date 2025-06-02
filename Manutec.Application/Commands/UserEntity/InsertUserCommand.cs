using Manutec.Application.Models;
using Manutec.Application.Models.UserModel;
using Manutec.Core.Entities;
using Manutec.Core.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Manutec.Application.Commands.UserEntity;
public class InsertUserCommand : IRequest<ResultViewModel<UserViewModel>>
{
    [Required(ErrorMessage = "O campo Nome do Usuário é obrigatório")]
    [Display(Name = "Nome do Usuário")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "O campo Nome da Oficina é obrigatório")]
    [Display(Name = "Nome da Oficina")]
    public int WorkShopId { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório")]
    [Display(Name = "Senha")]
    public string Password { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório")]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Telefone é obrigatório")]
    [Display(Name = "Telefone")]
    public string Phone { get; set; }

    [Display(Name = "Função")]
    public UserRole Role { get; set; }

    public User ToEntity()
    {
        return new User(UserName, WorkShopId, Password, Email, Phone, Role);
    }
}
