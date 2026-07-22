using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BioUTN.MVC.Models;

namespace BioUTN.MVC.Controllers
{
    public class EspecieController : Controller
    {
        public IActionResult Index()
        {
            ViewData["HeaderTitle"] = "Catálogo de Especies";
            ViewData["ShowBackButton"] = true;

            var especies = new List<EspecieViewModel>
            {
                new EspecieViewModel { CodigoInterno = "Sion", NombreCientifico = "Saintpaulia ionantha", NombreComun = "Violeta Africana", Familia = "Gesneriaceae", CategoriaBiotecnologica = "Ornamental", EstadoIucn = "NT", LotesVinculados = 5 },
                new EspecieViewModel { CodigoInterno = "Pfis", NombreCientifico = "Phragmipedium fischeri", NombreComun = "Orquídea Zapatito", Familia = "Orchidaceae", CategoriaBiotecnologica = "Amenazada", EstadoIucn = "CR", LotesVinculados = 0 },
                new EspecieViewModel { CodigoInterno = "Musa", NombreCientifico = "Musa acuminata", NombreComun = "Banano", Familia = "Musaceae", CategoriaBiotecnologica = "Alimenticia", EstadoIucn = "LC", LotesVinculados = 12 }
            };

            return View(especies);
        }

        public IActionResult Crear()
        {
            ViewData["HeaderTitle"] = "Nueva Especie";
            ViewData["ShowBackButton"] = true;
            return View();
        }
    }
}
