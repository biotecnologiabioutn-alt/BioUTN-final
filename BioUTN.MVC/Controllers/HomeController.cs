using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.MVC.Models;
using System.Collections.Generic;

namespace BioUTN.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["HeaderTitle"] = "Hola, Fernanda";
            ViewData["HeaderSubtitle"] = "Rol: Tesista";
            ViewData["ShowLogout"] = true;

            // Datos Mock Listos para ver
            var proyectos = new List<ProyectoViewModel>
            {
                new ProyectoViewModel 
                { 
                    IdLote = "Anth-01-R2", 
                    NombreCientifico = "Saintpaulia ionantha", 
                    TipoProyecto = "Tesis", 
                    Etiqueta = "Mi Tesis",
                    Detalles = "Ãšltima rev: Ayer"
                },
                new ProyectoViewModel 
                { 
                    IdLote = "Pfis-03-R0", 
                    NombreCientifico = "Phragmipedium fischeri", 
                    TipoProyecto = "Docencia", 
                    Etiqueta = "Asistencia - Dra. Cristina",
                    Detalles = "Anaquel A2, Piso 1"
                }
            };

            return View(proyectos);
        }

        public IActionResult Landing()
        {
            ViewData["HideHeader"] = true; // Ocultamos el Navbar para la Landing Page
            return View();
        }
    }
}


