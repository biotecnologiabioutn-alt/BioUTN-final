using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class UnidadFrascosController : Controller
    {
        // GET: UnidadFrascos
        public IActionResult Index()
        {
            try
            {
                var list = Crud<UnidadFrasco>.GetAll();
                var lotes = Crud<LoteCultivo>.GetAll();
                
                foreach(var item in list)
                {
                    item.LoteCultivo = lotes.FirstOrDefault(l => l.Id == item.IdLoteCultivo);
                }

                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los frascos: " + ex.Message;
                return View(new List<UnidadFrasco>());
            }
        }

        // GET: UnidadFrascos/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<UnidadFrasco>.GetById(id);
                if (item == null) return NotFound();

                if (item.IdLoteCultivo > 0)
                {
                    item.LoteCultivo = Crud<LoteCultivo>.GetById(item.IdLoteCultivo);
                    if (item.LoteCultivo?.IdPlantaMadre > 0)
                    {
                        item.LoteCultivo.PlantaMadre = Crud<PlantaMadre>.GetById(item.LoteCultivo.IdPlantaMadre);
                        if (item.LoteCultivo.PlantaMadre?.IdEspecie > 0)
                        {
                            item.LoteCultivo.PlantaMadre.Especie = Crud<Especie>.GetById(item.LoteCultivo.PlantaMadre.IdEspecie);
                        }
                    }
                    if (item.LoteCultivo?.IdMedioCultivo > 0)
                    {
                        item.LoteCultivo.MedioCultivo = Crud<MedioCultivo>.GetById(item.LoteCultivo.IdMedioCultivo);
                    }
                }

                // Cargar el historial de monitoreos para este frasco específico
                var monitoreos = Crud<MonitoreoFitosanitario>.GetAll()
                                    .Where(m => m.IdUnidadFrasco == id)
                                    .OrderByDescending(m => m.FechaEvaluacion)
                                    .ToList();
                
                // Mapear el usuario de cada monitoreo para la vista
                var usuarios = Crud<Usuario>.GetAll();
                foreach (var m in monitoreos)
                {
                    if (m.IdUsuario > 0)
                    {
                        m.Usuario = usuarios.FirstOrDefault(u => u.Id == m.IdUsuario);
                    }
                }

                item.Monitoreos = monitoreos;

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los detalles del frasco: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
