using Manutec.Core.Entities;
using Manutec.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Manutec.Application.Models.UserModel;
public class GetByIdUserViewModel
{
    public GetByIdUserViewModel(int id, string userName, string email, string passwordHash, string phone, UserRole role, DateTime createdAt)
    {
        Id = id;
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        Phone = phone;
        Role = role;
        CreatedAt = createdAt;
    }

    public int Id {  get; private set; }

    [Display(Name = "Nome do Usuário")]
    public string UserName { get; private set; }

    [Display(Name = "E-mail")]
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    [Display(Name = "Telefone")]
    public string Phone { get; private set; }

    [Display(Name = "Função")]
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static GetByIdUserViewModel FromEntity(User user)
    {
        return new GetByIdUserViewModel(user.Id, user.UserName, user.Email, user.PasswordHash, user.Phone, user.Role, user.CreatedAt);
    }
}
