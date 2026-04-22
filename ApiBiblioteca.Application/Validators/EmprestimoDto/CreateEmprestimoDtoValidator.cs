using ApiBiblioteca.Application.DTOs.DtosEmprestimo;
using FluentValidation;

namespace ApiBiblioteca.Application.Validators.EmprestimoDto;

public class CreateEmprestimoDtoValidator : AbstractValidator<CreateEmprestimoDto>
{
    public CreateEmprestimoDtoValidator()
    {
        RuleFor(x => x.ClienteId).GreaterThan(0).WithMessage("ClienteId deve ser maior que 0");
    }

}
