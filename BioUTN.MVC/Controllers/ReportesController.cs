using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Docente, Tecnico")]
    public class ReportesController : Controller
    {
        public IActionResult Cultivos()
        {
            return View();
        }

        public IActionResult Inventario()
        {
            return View();
        }
    }
}

