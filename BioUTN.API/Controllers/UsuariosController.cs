using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BioUTN.Modelos;
using BioUTN.API.Data;

namespace BioUTN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BioUTNContext _context;
        private readonly BioUTN.Servicios.IEmailService _emailService;

        public UsuariosController(BioUTNContext context, BioUTN.Servicios.IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            try
            {
                var list = await _context.Usuarios
                    .Include(u => u.Rol)
                    .ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            try
            {
                var item = await _context.Usuarios
                    .Include(u => u.Rol)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (item == null)
                {
                    return NotFound($"No se encontrÃ³ con ID {id}.");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario item)
        {
            if (id != item.Id)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto.");
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound("Elemento no encontrado.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar: " + ex.Message);
            }
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario item)
        {
            try
            {
                if (!item.Email.EndsWith("@utn.edu.ec"))
                {
                    return BadRequest("El correo debe ser institucional (@utn.edu.ec).");
                }

                if (_context.Usuarios.Any(u => u.Email == item.Email || u.NumeroIdentificacion == item.NumeroIdentificacion))
                {
                    return BadRequest("Ya existe un usuario con ese correo o cédula.");
                }

                item.ContrasenaHash = ComputeMD5Hash("BIO" + item.NumeroIdentificacion);
                item.IntentosFallidos = 0;
                item.CuentaBloqueada = false;
                item.Activo = true;
                
                _context.Usuarios.Add(item);
                await _context.SaveChangesAsync();

                try 
                {
                    await _emailService.EnviarCorreoRegistro(item.Email, $"{item.Nombres} {item.Apellidos}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error enviando correo: " + ex.Message);
                }

                return CreatedAtAction(nameof(GetUsuario), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            try
            {
                var item = await _context.Usuarios.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                item.Activo = false;
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        private string ComputeMD5Hash(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes).ToLower();
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] LoginRequest request)
        {
            try
            {
                string hashedInput = ComputeMD5Hash(request.Password);

                var usuario = await _context.Usuarios
                    .Include(u => u.Rol)
                    .FirstOrDefaultAsync(u => u.Email == request.Email && u.ContrasenaHash == hashedInput);

                if (usuario == null)
                {
                    var usuarioFailed = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
                    if (usuarioFailed != null)
                    {
                        usuarioFailed.IntentosFallidos++;
                        if (usuarioFailed.IntentosFallidos >= 5)
                        {
                            usuarioFailed.CuentaBloqueada = true;
                        }
                        await _context.SaveChangesAsync();
                    }
                    return Unauthorized("Credenciales incorrectas.");
                }

                if (usuario.CuentaBloqueada)
                {
                    return Forbid("Cuenta bloqueada.");
                }

                if (!usuario.Activo)
                {
                    return Unauthorized("Cuenta inactiva.");
                }

                usuario.IntentosFallidos = 0;
                await _context.SaveChangesAsync();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        [HttpPost("RecuperarPassword")]
        public async Task<IActionResult> RecuperarPassword([FromBody] RecoverRequest request)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (usuario == null) return Ok(); // Fake ok para evitar enumeración

            usuario.ResetToken = Guid.NewGuid().ToString("N");
            usuario.ResetTokenExpires = DateTime.UtcNow.AddMinutes(5);
            await _context.SaveChangesAsync();

            // MVC port is typically 7142 (HTTPS) for this solution based on standard ASP.NET templates
            // This should ideally come from appsettings
            string resetUrl = $"https://localhost:7142/Account/ResetPassword?token={usuario.ResetToken}&email={usuario.Email}";
            
            try 
            {
                await _emailService.EnviarCorreoRecuperacion(usuario.Email, resetUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error enviando correo de recuperación: " + ex.Message);
            }

            return Ok(new { message = "Si el correo existe, se enviará un enlace de recuperación." });
        }

        [HttpPost("ResetearPassword")]
        public async Task<IActionResult> ResetearPassword([FromBody] ResetRequest request)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email && u.ResetToken == request.Token);
            if (usuario == null) return BadRequest("Token inválido.");

            if (usuario.ResetTokenExpires < DateTime.UtcNow) return BadRequest("El token ha expirado.");

            // Validate complexity: 8 chars, 1 uppercase, 0 special
            if (request.NewPassword.Length < 8 || 
                !request.NewPassword.Any(char.IsUpper) || 
                request.NewPassword.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return BadRequest("La contraseña debe tener mínimo 8 caracteres, al menos 1 mayúscula y NO debe contener caracteres especiales.");
            }

            usuario.ContrasenaHash = ComputeMD5Hash(request.NewPassword);
            usuario.ResetToken = null;
            usuario.ResetTokenExpires = null;
            usuario.CuentaBloqueada = false;
            usuario.IntentosFallidos = 0;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Contraseña actualizada exitosamente." });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RecoverRequest
    {
        public string Email { get; set; } = string.Empty;
    }

    public class ResetRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
