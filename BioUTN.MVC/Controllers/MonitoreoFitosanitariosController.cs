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
    }
}


