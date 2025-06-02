using FluentValidation;
using FluentValidation.AspNetCore;
using Manutec.Api.ExceptionHandler;
using Manutec.Application.Commands.UserEntity;
using Manutec.Core.Repositories;
using Manutec.Infrastructure.Auth;
using Manutec.Infrastructure.Persistence;
using Manutec.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace Manutec.MVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);




        // Configuração de variáveis e secrets
        var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET");

        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<Program>();

        var jwtSettings = builder.Configuration.GetSection("JWT");
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        // MVC com views
        builder.Services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        builder.Services
        .AddAuthentication(options =>
            {
              options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; // ? define "Cookies" como padrão
              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    // ? se quiser, pode manter JWT como challenge
            })
        .AddCookie(options =>
        {
            options.LoginPath = "/Login";            // rota para tela de login
            options.AccessDeniedPath = "/AccessDenied"; // opcional, se quiser página de “acesso negado”
        })
        .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                };

                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Token inválido: {context.Exception.Message}");
                        return Task.CompletedTask;
                    }
                };
            });

        // Swagger (opcional em projeto MVC, mas útil para testar APIs internas)
        builder.Services.AddEndpointsApiExplorer();


        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // MediatR e FluentValidation
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertUserCommand>());
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<InsertUserCommand>();

        builder.Services.AddExceptionHandler<ApiExceptionHandler>();
        builder.Services.AddProblemDetails();

 

        // Contexto do banco
        var connectionString = builder.Configuration.GetConnectionString("Manutec");
        builder.Services.AddDbContext<ManutecDbContext>(o => o.UseSqlServer(connectionString));

        // Injeções de dependência
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
        builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
        builder.Services.AddScoped<IWorkShopRepository, WorkShopRepository>();
        builder.Services.AddScoped<IGeneralMaintenanceRepository, GeneralMaintenanceRepository>();

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<ILoggedUser, LoggedUser>();

        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Ambiente dev/prod
        //if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}


        app.UseExceptionHandler("/Home/Error");
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        // Rota padrão MVC
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=WorkShop}/{action=Register}/{id?}");

        app.Run();
    }
}
