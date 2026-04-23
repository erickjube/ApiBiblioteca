using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Application.Interfaces.Services;
using ApiBiblioteca.Application.Services;
using ApiBiblioteca.Application.Validators.AutorDtoValidators;
using ApiBiblioteca.Application.Validators.CategoriaDtoValidators;
using ApiBiblioteca.Application.Validators.ClienteDtoValidators;
using ApiBiblioteca.Application.Validators.EmprestimoDto;
using ApiBiblioteca.Application.Validators.ExemplarDtoValidators;
using ApiBiblioteca.Application.Validators.ItemEmprestimoDto;
using ApiBiblioteca.Application.Validators.LivroDtoValidators;
using ApiBiblioteca.Application.Validators.VendaDtoValidators;
using ApiBiblioteca.DTOs;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBiblioteca.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ILivroService, LivroService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IAutorService, AutorService>();
        services.AddScoped<IExemplarService, ExemplarService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IEmprestimoService, EmprestimoService>();
        services.AddScoped<IVendaService, VendaService>();


        services.AddValidatorsFromAssemblyContaining<CreateEmprestimoDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<DevolverItemDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<EstenderDevolucaoDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<CreateAutorDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<CreateCategoriaDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<CreateClienteDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateClienteDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<CreateExemplarDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateExemplarDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<CreateLivroDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateLivroDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<CreateVendaDtoValidator>();

        services.AddAutoMapper(typeof(DtoMappingProfile));

        return services;
    }
}
