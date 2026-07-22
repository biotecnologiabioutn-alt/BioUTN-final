using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class MonitoreoFitosanitariosController : Controller
    {
        // GET: MonitoreoFitosanitarios
        public IActionResult Index()
        {
            try
            {
                var list = Crud<MonitoreoFitosanitario>.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<MonitoreoFitosanitario>());
            }
        }

        // GET: MonitoreoFitosanitarios/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<MonitoreoFitosanitario>.GetById(id);
                if (item == null) return NotFound();
                
                if (item.IdUnidadFrasco > 0)
                    item.UnidadFrasco = Crud<UnidadFrasco>.GetById(item.IdUnidadFrasco);
                if (item.IdUsuario > 0)
                    item.Usuario = Crud<Usuario>.GetById(item.IdUsuario);

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los detalles: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: MonitoreoFitosanitarios/Create
        public IActionResult Create()
        {
            ViewBag.UnidadesFrasco = new SelectList(Crud<UnidadFrasco>.GetAll(), "Id", "CodigoUnidad");
            ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll(), "Id", "Nombres");
            return View();
        }

        // POST: MonitoreoFitosanitarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MonitoreoFitosanitario item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Crud<MonitoreoFitosanitario>.Create(item);
                    TempData["Success"] = "Elemento creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }
            ViewBag.UnidadesFrasco = new SelectList(Crud<UnidadFrasco>.GetAll(), "Id", "CodigoUnidad", item.IdUnidadFrasco);
            ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll(), "Id", "Nombres", item.IdUsuario);
            return View(item);
        }

        // GET: MonitoreoFitosanitarios/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<MonitoreoFitosanitario>.GetById(id);
                if (item == null) return NotFound();

                ViewBag.UnidadesFrasco = new SelectList(Crud<UnidadFrasco>.GetAll(), "Id", "CodigoUnidad", item.IdUnidadFrasco);
                ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll(), "Id", "Nombres", item.IdUsuario);
                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: MonitoreoFitosanitarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MonitoreoFitosanitario item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<MonitoreoFitosanitario>.Update(id, item);
                    TempData["Success"] = "Elemento actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                     TempData["Error"] = "Error al actualizar: " + ex.Message;
                }
            }
            ViewBag.UnidadesFrasco = new SelectList(Crud<UnidadFrasco>.GetAll(), "Id", "CodigoUnidad", item.IdUnidadFrasco);
            ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll(), "Id", "Nombres", item.IdUsuario);
            return View(item);
        }

        // GET: MonitoreoFitosanitarios/Delete/5
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<MonitoreoFitosanitario>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: MonitoreoFitosanitarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<MonitoreoFitosanitario>.Delete(id);
                TempData["Success"] = "Elemento eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se puede eliminar (posiblemente en uso): " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
        // GET: MonitoreoFitosanitarios/FichaSeguimiento/5
        public IActionResult FichaSeguimiento(int loteId)
        {
            try
            {
                var lote = Crud<LoteCultivo>.GetById(loteId);
                if (lote == null) return NotFound("Lote no encontrado.");

                var todasUnidades = Crud<UnidadFrasco>.GetAll();
                var unidadesLote = todasUnidades.Where(u => u.IdLoteCultivo == loteId && u.Activo).ToList();

                var vm = new BioUTN.MVC.Models.FichaSeguimientoViewModel
                {
                    IdLoteCultivo = loteId,
                    LoteCodigo = lote.CodigoTrazabilidad,
                    FechaSiembra = lote.FechaSiembra,
                    TotalUnidades = lote.TotalUnidades,
                    Explante = lote.TipoExplante,
                    Responsables = lote.ResponsablesSiembraNombres ?? ""
                };

                foreach (var u in unidadesLote)
                {
                    vm.Filas.Add(new BioUTN.MVC.Models.FichaFilaViewModel
                    {
                        IdUnidadFrasco = u.Id,
                        CodigoUnidad = u.CodigoUnidad,
                        Respuesta = "Crecimiento del explante" // default from PDF
                    });
                }

                return View("Ficha", vm);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar la ficha: " + ex.Message;
                return RedirectToAction("Index", "LoteCultivos");
            }
        }

        // POST: MonitoreoFitosanitarios/FichaSeguimiento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FichaSeguimiento(BioUTN.MVC.Models.FichaSeguimientoViewModel vm)
        {
            try
            {
                var loggedUserId = Crud<Usuario>.GetAll().FirstOrDefault(u => u.Email == User.Identity?.Name)?.Id ?? 1;

                var monitoreos = new List<MonitoreoFitosanitario>();
                foreach (var f in vm.Filas)
                {
                    var m = new MonitoreoFitosanitario
                    {
                        IdUnidadFrasco = f.IdUnidadFrasco,
                        IdUsuario = loggedUserId,
                        FechaEvaluacion = vm.FechaRevision.ToUniversalTime(),
                        UnidadesRevisadas = 1,
                        Bacterias = f.Bacterias,
                        Hongos = f.Hongos,
                        Muerte = f.Muerte,
                        NivelFenolizacion = f.Oxidacion ? "Alto" : "Ninguno",
                        NivelContaminacion = (f.Bacterias || f.Hongos) ? "Alto" : "Ninguno",
                        Respuesta = f.Respuesta ?? "",
                        Observaciones = f.Observacion ?? ""
                    };
                    monitoreos.Add(m);
                }

                if (monitoreos.Any())
                {
                    Crud<MonitoreoFitosanitario>.CreateBatch(monitoreos.ToArray());
                    TempData["Success"] = "Ficha de seguimiento guardada correctamente.";
                }

                return RedirectToAction("Details", "LoteCultivos", new { id = vm.IdLoteCultivo });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al guardar la ficha: " + ex.Message;
                return View("Ficha", vm);
            }
        }
    }
}


