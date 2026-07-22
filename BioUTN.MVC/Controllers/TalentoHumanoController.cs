using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico")]
    public class TalentoHumanoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

