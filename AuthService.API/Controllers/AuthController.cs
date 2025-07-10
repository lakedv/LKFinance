using AuthService.API.DTOs;
using AuthService.API.Services;

using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _userService.RegisterUserAsync(request);
            if (!success)
                return BadRequest("El correo ya existe o es invalido.");
            return Ok("Usuario registrado correctamente.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _userService.LoginUserAsync(request);
            if (token == null)
                return Unauthorized("Correo o contraseña invalidos.");

            return Ok(new { token });
        }
    }
}
