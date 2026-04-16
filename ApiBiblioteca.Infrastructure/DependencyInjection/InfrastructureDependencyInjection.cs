using ApiBiblioteca.ApiBiblioteca.Infrastructure.Data;
using ApiBiblioteca.Application.Interfaces;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Domain.Repositories;
using ApiBiblioteca.Infrastructure.Services;
using ApiBiblioteca.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBiblioteca.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connection = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<MeuDbContext>(options =>
            options.UseSqlServer(connection));

        services.AddScoped<ILivroRepository, LivroRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IAutorRepository, AutorRepository>();
        services.AddScoped<IExemplarRepository, ExemplarRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
        services.AddScoped<IVendaRepository, VendaRepository>();
        services.AddScoped<IMultaRepository, MultaRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
