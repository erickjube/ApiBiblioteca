using ApiBiblioteca.Application.DTOs.DtosCategoria;
using FluentValidation;

namespace ApiBiblioteca.Application.Validators.CategoriaDtoValidators;

public class CreateCategoriaDtoValidator : AbstractValidator<CategoriaDto>
{
    public CreateCategoriaDtoValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da categoria deve ter no máximo 100 caracteres.");
    }
}
