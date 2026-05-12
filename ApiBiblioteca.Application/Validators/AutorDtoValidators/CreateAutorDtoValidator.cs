using ApiBiblioteca.Application.DTOs.DtosAutor;
using FluentValidation;

namespace ApiBiblioteca.Application.Validators.AutorDtoValidators;

public class CreateAutorDtoValidator : AbstractValidator<AutorDto>
{
    public CreateAutorDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(100).WithMessage("O nome do autor deve ter no máximo 100 caracteres.");

        RuleFor(x => x.DataNascimento)
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("A Data de Nascimento deve ser anterior à data atual.");

        RuleFor(x => x.Nacionalidade)
            .NotEmpty().WithMessage("Nacionalidade é obrigatória")
            .MaximumLength(100).WithMessage("A nacionalidade deve ter no máximo 100 caracteres.");    
    }
}
