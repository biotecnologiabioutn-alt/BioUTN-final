using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class ZonaTrabajoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Mapa()
        {
            try
            {
                var anaqueles = BioUTN.ApiConsumer.Crud<BioUTN.Modelos.UbicacionFisica>.GetAll();
                return View(anaqueles);
            }
            catch (System.Exception)
            {
                // Si falla, enviamos lista vacía para no romper la vista
                return View(new System.Collections.Generic.List<BioUTN.Modelos.UbicacionFisica>());
            }
        }

        public IActionResult Checklist()
        {
            return View();
        }
    }
}
