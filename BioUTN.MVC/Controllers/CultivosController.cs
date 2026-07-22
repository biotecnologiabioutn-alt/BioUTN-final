using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class CultivosController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var frascos = BioUTN.ApiConsumer.Crud<BioUTN.Modelos.UnidadFrasco>.GetAll();
                ViewBag.TotalFrascos = frascos?.Count() ?? 0;
                
                var contaminados = frascos?.Count(f => f.Estado != null && f.Estado.Contains("Contaminad")) ?? 0;
                ViewBag.Contaminados = contaminados;
                
                var saludables = (ViewBag.TotalFrascos - contaminados);
                ViewBag.Saludables = saludables;

                if (ViewBag.TotalFrascos > 0)
                {
                    ViewBag.Eficiencia = (int)Math.Round((double)saludables / ViewBag.TotalFrascos * 100);
                }
                else
                {
                    ViewBag.Eficiencia = 100;
                }

                ViewBag.TotalEspecies = BioUTN.ApiConsumer.Crud<BioUTN.Modelos.Especie>.GetAll()?.Count() ?? 0;
                ViewBag.TotalPlantasMadre = BioUTN.ApiConsumer.Crud<BioUTN.Modelos.PlantaMadre>.GetAll()?.Count() ?? 0;
                ViewBag.TotalLotes = BioUTN.ApiConsumer.Crud<BioUTN.Modelos.LoteCultivo>.GetAll()?.Count() ?? 0;
            }
            catch (System.Exception)
            {
                ViewBag.TotalFrascos = 0;
                ViewBag.Contaminados = 0;
                ViewBag.Saludables = 0;
                ViewBag.Eficiencia = 0;
                ViewBag.TotalEspecies = 0;
                ViewBag.TotalPlantasMadre = 0;
                ViewBag.TotalLotes = 0;
            }

            return View();
        }
    }
}


