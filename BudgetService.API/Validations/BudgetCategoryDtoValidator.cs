using BudgetService.API.DTOs;
using FluentValidation;

namespace BudgetService.API.Validations
{
    public class BudgetCategoryDtoValidator : AbstractValidator<BudgetCategoryDto>
    {
        public BudgetCategoryDtoValidator() 
        {
            RuleFor(c => c.CategoryName)
                .NotEmpty().WithMessage("El nombre de la categoria es obligatorio.")
                .MaximumLength(50).WithMessage("El maximo es 50 caracteres.");

            RuleFor(c => c.Amount)
                .GreaterThan(0).WithMessage("El monto debe ser mayor que 0.");
        }
    }
}
