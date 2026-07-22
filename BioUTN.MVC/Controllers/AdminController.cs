using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BioUTN.MVC.Models;

namespace BioUTN.MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Usuarios()
        {
            ViewData["HeaderTitle"] = "Gestión de Cuentas";
            ViewData["ShowBackButton"] = true;

            var usuarios = new List<UsuarioViewModel>
            {
                new UsuarioViewModel { Id = "1720000001", Nombres = "Cristina", Apellidos = "Echeverría", Correo = "cecheverria@utn.edu.ec", Rol = "Docente", Estado = "Activo", Semestre = "-" },
                new UsuarioViewModel { Id = "1001234567", Nombres = "Fernanda", Apellidos = "Revelo", Correo = "frevelo@utn.edu.ec", Rol = "Tesista", Estado = "Activo", Semestre = "8vo Nivel" },
                new UsuarioViewModel { Id = "0401234567", Nombres = "Juan", Apellidos = "Pérez", Correo = "jperez@utn.edu.ec", Rol = "Estudiante", Estado = "Inactivo", Semestre = "6to Nivel" }
            };

            return View(usuarios);
        }
    }
}
