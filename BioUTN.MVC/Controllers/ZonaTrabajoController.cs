using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class ZonaTrabajoController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                ViewBag.TotalEquipos = BioUTN.ApiConsumer.Crud<BioUTN.Modelos.Equipo>.GetAll()?.Count() ?? 0;
                
                var reservas = BioUTN.ApiConsumer.Crud<BioUTN.Modelos.ReservaEquipo>.GetAll();
                ViewBag.ReservasActivas = reservas?.Count(r => r.FechaFin >= System.DateTime.Now) ?? 0;

                ViewBag.TotalAnaqueles = BioUTN.ApiConsumer.Crud<BioUTN.Modelos.UbicacionFisica>.GetAll()?.Count() ?? 0;
                ViewBag.TotalDocumentos = BioUTN.ApiConsumer.Crud<BioUTN.Modelos.Documento>.GetAll()?.Count() ?? 0;
            }
            catch (System.Exception)
            {
                ViewBag.TotalEquipos = 0;
                ViewBag.ReservasActivas = 0;
                ViewBag.TotalAnaqueles = 0;
                ViewBag.TotalDocumentos = 0;
            }

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
