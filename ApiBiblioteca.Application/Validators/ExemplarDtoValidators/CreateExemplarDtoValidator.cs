using ApiBiblioteca.Application.DTOs.DtosExemplar;
using FluentValidation;

namespace ApiBiblioteca.Application.Validators.ExemplarDtoValidators;

public class CreateExemplarDtoValidator : AbstractValidator<CreateExemplarDto>
{
    public CreateExemplarDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(200).WithMessage("O nome deve ter no máximo 200 caracteres.");

        RuleFor(x => x.CodigoDeBarras)
            .NotEmpty().WithMessage("O código de barras é obrigatório.")
            .Matches(@"^\d{13}$").WithMessage("Código de barras inválido. Deve conter exatamente 13 dígitos.");

        RuleFor(x => x.Preco)
            .NotEmpty().WithMessage("O preço é obrigatório.")
            .GreaterThan(0).WithMessage("O preço deve ser um valor positivo.");

        RuleFor(x => x.LivroId)
            .NotEmpty().WithMessage("O Id do livro é obrigatório.")
            .GreaterThan(0).WithMessage("O Id do livro deve ser maior que zero.");
    }
}
