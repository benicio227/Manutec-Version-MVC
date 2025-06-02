using Manutec.Application.Models;
using Manutec.Application.Models.UserModel;
using Manutec.Core.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Manutec.Application.Commands.UserEntity;
public class LoginUserCommand : IRequest<ResultViewModel<LoginViewModel>>
{
    [Required(ErrorMessage = "O campo Email é obrigatório")]
    [Display(Name = "E-mail")]
    public string Email {  get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório")]
    [Display(Name = "Senha")]
    public string Password { get; set; }
}
