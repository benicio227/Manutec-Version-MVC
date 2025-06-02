using Manutec.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Manutec.Application.Models.UserModel;
public class GetAllUserViewModel
{
    public GetAllUserViewModel(int id, string userName, string email, string phone, DateTime createdAt)
    {
        Id = id;
        UserName = userName;
        Email = email;
        Phone = phone;
        CreatedAt = createdAt;
    }
    public int Id {  get; private set; }

    [Display(Name = "Nome do Usuário")]
    public string UserName { get; private set; }

    [Display(Name = "E-mail")]
    public string Email { get; private set; }

    [Display(Name = "Telefone")]
    public string Phone { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public static List<GetAllUserViewModel> FromEntity(List<User> users)
    {
        return users.Select(user => new GetAllUserViewModel(user.Id, user.UserName, user.Email, user.Phone, user.CreatedAt)).ToList();
    }
}
