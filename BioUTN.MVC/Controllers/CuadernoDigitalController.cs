using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class CuadernoDigitalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
