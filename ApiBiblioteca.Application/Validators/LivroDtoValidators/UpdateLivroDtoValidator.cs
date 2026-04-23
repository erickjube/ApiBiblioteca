using ApiBiblioteca.Application.DTOs.DtosLivro;
using FluentValidation;

namespace ApiBiblioteca.Application.Validators.LivroDtoValidators;

public class UpdateLivroDtoValidator : AbstractValidator<UpdateLivroDto>
{
    public UpdateLivroDtoValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .MaximumLength(250).WithMessage("O título deve conter no máximo 250 caracteres.");

        RuleFor(x => x.DataPublicacao)
            .NotEmpty().WithMessage("A data de publicação é obrigatória.")
            .GreaterThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Data da Publicação deve ser uma data futura.");
        
        RuleFor(x => x.NumeroDePaginas)
            .NotEmpty().WithMessage("O número de páginas é obrigatório.")
            .GreaterThan(0).WithMessage("O número de páginas deve ser maior que zero.");

        RuleFor(x => x.CategoriaId)
            .NotEmpty().WithMessage("A Categoria Id é obrigatória.")
            .GreaterThan(0).WithMessage("A Categoria Id deve ser um valor positivo.");
    }
}
