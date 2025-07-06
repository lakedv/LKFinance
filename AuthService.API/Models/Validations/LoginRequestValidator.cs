using FluentValidation;
using AuthService.API.Models.DTOs;
namespace AuthService.API.Models.Validations
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Debe ingresar un correo.")
                .EmailAddress().WithMessage("El formato es invalido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Debe ingresar una contraseña valida.");
        }
    }
}
