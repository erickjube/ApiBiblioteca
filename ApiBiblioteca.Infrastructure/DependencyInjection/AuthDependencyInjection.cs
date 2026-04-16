using ApiBiblioteca.ApiBiblioteca.Infrastructure.Data;
using ApiBiblioteca.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiBiblioteca.Infrastructure.DependencyInjection;

public static class AuthDependencyInjection
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
    {
        var secretKey = config["JWT:SecretKey"]
            ?? throw new ArgumentException("Invalid Secret Key!");

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<MeuDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            option.SaveToken = true;
            // Em produção o ideal é true
            option.RequireHttpsMetadata = false;
            option.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = config["JWT:ValidAudience"],
                ValidIssuer = config["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Funcionarios", policy =>
                policy.RequireRole("Admin", "Bibliotecario"));
        });

        return services;
    }
}
