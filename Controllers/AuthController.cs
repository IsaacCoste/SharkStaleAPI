using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharkStyleApi.DAL;
using SharkStyleApi.data.Models;

namespace SharkStyleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Contexto _context;

        public AuthController(Contexto context)
        {
            _context = context;
        }

        // POST: api/Auth/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
            {
                return BadRequest(new { message = "El email ya está registrado." });
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registro exitoso", usuarioId = usuario.UsuarioId });
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == login.Email && u.PasswordHash == login.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Email o contraseña incorrectos." });
            }

            return Ok(new
            {
                message = "Login exitoso",
                usuarioId = user.UsuarioId,
                nombre = user.Nombre,
                email = user.Email
            });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
