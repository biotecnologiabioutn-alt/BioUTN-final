using Microsoft.AspNetCore.Mvc;

namespace BioUTN.MVC.Controllers
{
    public class PwaController : Controller
    {
        public IActionResult Scanner()
        {
            // Ocultamos el header y footer para dar sensación de cámara nativa (pantalla completa)
            ViewData["HideNav"] = true;
            return View();
        }

        public IActionResult TarjetaCultivo()
        {
            ViewData["HeaderTitle"] = "Decodificación QR";
            ViewData["ShowBackButton"] = true;
            return View();
        }
    }
}
