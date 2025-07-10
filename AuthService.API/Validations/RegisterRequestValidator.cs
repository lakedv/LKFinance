using FluentValidation;
using AuthService.API.DTOs;

namespace AuthService.API.Validations
{
    public class RegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Name)
                   .NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no tiene un formato valido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
        }
    }
}
