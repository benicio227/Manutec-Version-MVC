using Manutec.Core.Enums;
using Microsoft.Extensions.Primitives;

namespace Manutec.Application.Models.UserModel;
public class LoginViewModel
{
    public LoginViewModel(int workShopId, string email, UserRole role, string token)
    {
        WorkShopId = workShopId;
        Email = email;
        Role = role;
        Token = token;
    }
    public string Token {  get; set; }
    public int WorkShopId {  get; set; }
    public string Email {  get; set; }
    public UserRole  Role {  get; set; }
}
