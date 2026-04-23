using ApiBiblioteca.Application.DTOs.DtosCliente;
using FluentValidation;

namespace ApiBiblioteca.Application.Validators.ClienteDtoValidators;

public class UpdateClienteDtoValidator : AbstractValidator<UpdateClienteDto>
{
    public UpdateClienteDtoValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome do cliente é obrigatório.");
        RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatorio.")
                .MaximumLength(100).WithMessage("O email deve ter no máximo 100 caracteres.");
        RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .MaximumLength(20).WithMessage("O Telefone deve conter no máximo 20 caracteres.");
        RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("A data é obrigatoria.")
                .GreaterThan(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("A Data de Nascimento deve ser uma data futura.");
    }
}
