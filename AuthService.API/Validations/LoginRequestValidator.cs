using FluentValidation;
using AuthService.API.DTOs;
namespace AuthService.API.Validations
{
    public class LoginRequestValidator : AbstractValidator<UserLoginRequest>
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
