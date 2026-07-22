using Microsoft.AspNetCore.Mvc;

namespace BioUTN.MVC.Controllers
{
    public class EquipoController : Controller
    {
        public IActionResult Index()
        {
            ViewData["HeaderTitle"] = "Gestión de Equipos de Laboratorio";
            ViewData["ShowBackButton"] = true;
            return View();
        }

        public IActionResult Reservas()
        {
            ViewData["HeaderTitle"] = "Gestión de Recursos Críticos";
            ViewData["ShowBackButton"] = true;
            return View();
        }

        public IActionResult Checklist()
        {
            ViewData["HeaderTitle"] = "Cierre Operativo y Trazabilidad";
            ViewData["ShowBackButton"] = true;
            return View();
        }
    }
}
