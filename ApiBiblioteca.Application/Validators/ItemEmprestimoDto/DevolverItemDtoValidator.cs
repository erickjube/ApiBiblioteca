using ApiBiblioteca.Application.DTOs.DtosItemEmprestimo;
using FluentValidation;

namespace ApiBiblioteca.Application.Validators.ItemEmprestimoDto;

public class DevolverItemDtoValidator : AbstractValidator<DevolverItemEmprestimoDto>
{
    public DevolverItemDtoValidator()
    {
        RuleFor(x => x.ItemId).GreaterThan(0).WithMessage("Id do item deve ser maior que zero.");
        RuleFor(x => x.Condicao).IsInEnum().WithMessage("Condição do item inválida.");
    }
}
