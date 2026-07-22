using Microsoft.AspNetCore.Mvc;

namespace BioUTN.MVC.Controllers
{
    public class CultivoController : Controller
    {
        public IActionResult ScannerResult()
        {
            ViewData["HeaderTitle"] = "Lectura QR Exitosa";
            ViewData["ShowBackButton"] = true;
            return View();
        }

        public IActionResult BandejaDigital()
        {
            ViewData["HeaderTitle"] = "Lote Anth-01-R2";
            ViewData["HeaderSubtitle"] = "Seleccione los frascos afectados";
            ViewData["ShowBackButton"] = true;
            return View();
        }

        public IActionResult Resiembra()
        {
            ViewData["HeaderTitle"] = "Registro de Subcultivo";
            ViewData["ShowBackButton"] = true;
            return View();
        }

        public IActionResult Trazabilidad()
        {
            ViewData["HeaderTitle"] = "Trazabilidad Genealógica";
            ViewData["ShowBackButton"] = true;
            return View();
        }

        public IActionResult MisCultivos()
        {
            ViewData["HeaderTitle"] = "Mis Cultivos Activos";
            ViewData["HeaderSubtitle"] = "Gestión y monitoreo de lotes in vitro";
            ViewData["ShowBackButton"] = false; // Como está en el sidebar principal, no necesita back
            return View();
        }
    }
}
