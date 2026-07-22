using Microsoft.AspNetCore.Mvc;

namespace BioUTN.MVC.Controllers
{
    public class LoteController : Controller
    {
        public IActionResult Crear()
        {
            ViewData["HeaderTitle"] = "Crear Nuevo Lote";
            ViewData["ShowBackButton"] = true;
            return View();
        }

        public IActionResult PanelEscritorio()
        {
            ViewData["HeaderTitle"] = "Panel de Escritorio - Gestión QR";
            ViewData["ShowBackButton"] = true;
            return View();
        }
    }
}
