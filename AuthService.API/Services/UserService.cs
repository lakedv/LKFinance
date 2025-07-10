using AuthService.API.DTOs;
using AuthService.API.Exceptions;
using AuthService.API.Models;
using AuthService.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.API.Services
{
    public class UserService : IUserService
    {
        private readonly IJwtService _jwtService;
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IJwtService jwtService,
            IOptions<JwtOptions> jwtOptions)
        {
            _jwtService = jwtService;
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new ConflictException("Ya existe un usuario registrado con ese email.");
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                Name = request.Name
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new ValidationException(string.Join(" | ", result.Errors.Select(e => e.Description)));

            return true;
        }

        public async Task<string?> LoginUserAsync(UserLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new NotFoundException("Usuario no encontrado.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new BadRequestException("Credenciales incorrectas.");

            return _jwtService.GenerateToken(user, _jwtOptions);
        }
    }
}
