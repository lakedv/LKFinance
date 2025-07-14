using BudgetService.API.DTOs;
using FluentValidation;

namespace BudgetService.API.Validations
{
    public class IncomeDtoValidator : AbstractValidator<IncomeDto>
    {
        public IncomeDtoValidator() 
        {
            RuleFor(i => i.Amount)
                .GreaterThan(0).WithMessage("El monto debe ser mayor que 0.");

            RuleFor(i => i.Currency)
                .NotEmpty().WithMessage("La moneda es obligatoria.")
                .Must(c => new[] { "ARS", "USD", "EUR" }.Contains(c))
                .WithMessage("La moneda debe ser: ARS, USD o EUR.");
        }
    }
}
