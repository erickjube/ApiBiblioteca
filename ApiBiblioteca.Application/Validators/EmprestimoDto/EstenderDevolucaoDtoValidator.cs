using ApiBiblioteca.Application.DTOs.DtosEmprestimo;
using FluentValidation;

namespace ApiBiblioteca.Application.Validators.EmprestimoDto;

public class EstenderDevolucaoDtoValidator : AbstractValidator<EstenderDevolucaoDto>
{
    public EstenderDevolucaoDtoValidator()
    {
        RuleFor(x => x.EmprestimoId).GreaterThan(0).WithMessage("O Id do empréstimo deve ser maior que zero.");
        RuleFor(x => x.NovoPrazoDevolucao).GreaterThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("O novo prazo de devolução deve ser uma data futura.");
    }
}
