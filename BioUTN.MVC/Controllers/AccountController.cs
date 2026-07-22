using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BioUTN.Modelos;
using System.Text.Json;
using System.Text;

namespace BioUTN.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BioUTNAPI");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Debe ingresar sus credenciales.";
                return View();
            }

            var request = new { Email = email, Password = password };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync("Usuarios/Login", content);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error de conexión con la API: {ex.Message}";
                return View();
            }

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var usuario = JsonSerializer.Deserialize<Usuario>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, $"{usuario.Nombres} {usuario.Apellidos}"),
                        new Claim(ClaimTypes.Email, usuario.Email),
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
                    };

                    if (usuario.Rol != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, usuario.Rol.NombreRol));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
            }
            
            var errorContent = await response.Content.ReadAsStringAsync();
            ViewBag.Error = string.IsNullOrEmpty(errorContent) ? "Credenciales incorrectas o cuenta bloqueada." : errorContent;
            return View();
        }

        [HttpGet]
        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Debe ingresar su correo institucional.";
                return View();
            }

            var request = new { Email = email };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Usuarios/RecuperarPassword", content);

            ViewBag.Success = "Si el correo está registrado, recibirás un enlace de recuperación válido por 5 minutos.";
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Token = token;
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string token, string newPassword, string confirmPassword)
        {
            ViewBag.Token = token;
            ViewBag.Email = email;

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View();
            }

            var request = new { Email = email, Token = token, NewPassword = newPassword };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Usuarios/ResetearPassword", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Contraseña restablecida exitosamente. Ahora puedes iniciar sesión.";
                return RedirectToAction("Login");
            }
            
            var errorMsg = await response.Content.ReadAsStringAsync();
            ViewBag.Error = string.IsNullOrEmpty(errorMsg) ? "Error al restablecer la contraseña." : errorMsg;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            try 
            {
                ViewBag.Roles = BioUTN.ApiConsumer.Crud<Rol>.GetAll();
                ViewBag.TiposIdentificacion = BioUTN.ApiConsumer.Crud<TipoIdentificacion>.GetAll();
                ViewBag.Generos = BioUTN.ApiConsumer.Crud<Genero>.GetAll();
            }
            catch (Exception ex) 
            {
                ViewBag.Error = "Error al cargar datos del formulario: " + ex.Message;
            }
            return View(new Usuario());
        }

        [HttpPost]
        public IActionResult Register(Usuario usuario)
        {
            // Remove navigation properties from validation if necessary
            ModelState.Remove("Rol");
            ModelState.Remove("TipoIdentificacion");
            ModelState.Remove("Genero");
            ModelState.Remove("PlantasIntroducidas");
            ModelState.Remove("LotesAsignados");
            ModelState.Remove("Reservas");
            ModelState.Remove("DocumentosSubidos");
            ModelState.Remove("ProyectosAsignados");

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.CuentaBloqueada = false;
                    usuario.IntentosFallidos = 0;
                    BioUTN.ApiConsumer.Crud<Usuario>.Create(usuario);
                    TempData["Success"] = "Usuario registrado correctamente. Puede iniciar sesión.";
                    return RedirectToAction("Login", "Account");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error al registrar: " + ex.Message;
                }
            }

            try 
            {
                ViewBag.Roles = BioUTN.ApiConsumer.Crud<Rol>.GetAll();
                ViewBag.TiposIdentificacion = BioUTN.ApiConsumer.Crud<TipoIdentificacion>.GetAll();
                ViewBag.Generos = BioUTN.ApiConsumer.Crud<Genero>.GetAll();
            }
            catch (Exception) { /* Ignorar errores de carga al re-renderizar */ }

            return View(usuario);
        }
    }
}
