using Manutec.Application.Models;
using Manutec.Application.Models.UserModel;
using Manutec.Core.Entities;
using Manutec.Core.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Manutec.Application.Commands.UserEntity;
public class UpdateUserCommand : IRequest<ResultViewModel<UpdateUserViewModel>>
{
    public int Id {  get; set; }

    [Display(Name = "Nome do Usuário")]
    public string UserName { get; set; }
    public int WorkShopId {  get; set; }

    [Display(Name = "Senha")]
    public string PasswordHash { get; set; }

    [Display(Name = "E-mail")]
    public string Email { get; set; }

    [Display(Name = "Telefone")]
    public string Phone { get; set; }

    [Display(Name = "Função")]
    public UserRole Role { get; set; }
    public User ToEntity()
    {
        return new User(UserName, WorkShopId, PasswordHash, Email, Phone, Role);
    }
}
