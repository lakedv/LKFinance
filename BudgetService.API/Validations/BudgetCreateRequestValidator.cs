using BudgetService.API.DTOs; 
using FluentValidation;

namespace BudgetService.API.Validations
{
    public class BudgetCreateRequestValidator : AbstractValidator<BudgetCreateRequest> 
    {
        public BudgetCreateRequestValidator() 
        {
            RuleFor(b => b.Month)
                .InclusiveBetween(1, 12).WithMessage("El mes debe estar entre Enero y Diciembre.");

            RuleFor(b => b.Year)
                .InclusiveBetween(2000, 2100).WithMessage("Ingrese un Año valido.");

            RuleFor(b => b.Incomes)
                .NotNull().WithMessage("Debe especificar su ingreso.")
                .Must(i => i.Count > 0).WithMessage("Su monto debe ser mayor que 0");

            RuleForEach(b => b.Incomes).SetValidator(new IncomeDtoValidator());
            RuleForEach(b => b.Categories).SetValidator(new BudgetCategoryDtoValidator());
        }
    }
}
