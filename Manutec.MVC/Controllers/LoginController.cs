using Manutec.Application.Commands.UserEntity;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Manutec.MVC.Controllers
{
    [Route("LoginUsuario")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            try
            {

                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.Message ?? "Login inválido.");
                    return View(command);
                }

                var claims = new List<Claim>
                {
                    new Claim("WorkShopId", result.Data.WorkShopId.ToString()),
                    new Claim(ClaimTypes.Email, result.Data.Email),
                    new Claim(ClaimTypes.Role, ((int)result.Data.Role).ToString()),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    new AuthenticationProperties { IsPersistent = true }
                );

                TempData["SuccessMessage"] = "Login realizado com sucesso!";
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao processar login.");
                return View(command);
            }

        }
    }
}
