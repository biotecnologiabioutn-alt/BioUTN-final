using Microsoft.AspNetCore.Mvc;

namespace BioUTN.MVC.Controllers
{
    public class PlantaMadreController : Controller
    {
        public IActionResult Crear()
        {
            ViewData["HeaderTitle"] = "Ingreso Planta Madre";
            ViewData["ShowBackButton"] = true;
            return View();
        }
    }
}
