using ApiBiblioteca.Application.DTOs.DtosVenda;
using FluentValidation;

namespace ApiBiblioteca.Application.Validators.VendaDtoValidators;

public class CreateVendaDtoValidator : AbstractValidator<CreateVendaDto>
{
    public CreateVendaDtoValidator()
    {
        RuleFor(x => x.ClienteId)
            .NotEmpty().WithMessage("O Id do cliente é obrigatório.")
            .GreaterThan(0).WithMessage("O Id do cliente deve ser maior que zero.");
    }
}
