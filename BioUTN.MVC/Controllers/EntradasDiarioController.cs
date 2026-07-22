using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class EntradasDiarioController : Controller
    {
        // GET: EntradasDiario
        public IActionResult Index()
        {
            try
            {
                var list = Crud<EntradaDiario>.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<EntradaDiario>());
            }
        }

        // GET: EntradasDiario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EntradasDiario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EntradaDiario item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    item.FechaRegistro = DateTime.Now; // Autogenerar fecha
                    Crud<EntradaDiario>.Create(item);
                    TempData["Success"] = "Ficha de seguimiento creada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }
            return View(item);
        }

        // GET: EntradasDiario/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<EntradaDiario>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: EntradasDiario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EntradaDiario item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<EntradaDiario>.Update(id, item);
                    TempData["Success"] = "Ficha de seguimiento actualizada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                     TempData["Error"] = "Error al actualizar: " + ex.Message;
                }
            }
            return View(item);
        }

        // GET: EntradasDiario/Delete/5
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<EntradaDiario>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: EntradasDiario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<EntradaDiario>.Delete(id);
                TempData["Success"] = "Ficha eliminada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se puede eliminar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
