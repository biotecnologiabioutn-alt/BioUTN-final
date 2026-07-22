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
    public class ImplementosController : Controller
    {
        // GET: Implementos
        public IActionResult Index()
        {
            try
            {
                var list = Crud<Reactivo>.GetAll()
                    .Where(r => r.Categoria != null && r.Categoria.ToLower() == "implementos")
                    .ToList();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<Reactivo>());
            }
        }

        // GET: Implementos/Create
        public IActionResult Create()
        {
            try
            {
                ViewBag.UnidadesMedida = Crud<UnidadMedida>.GetAll();
            }
            catch { ViewBag.UnidadesMedida = new List<UnidadMedida>(); }
            return View();
        }

        // POST: Implementos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reactivo item)
        {
            item.Categoria = "Implementos"; // Forzar la categoría

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<Reactivo>.Create(item);
                    TempData["Success"] = "Implemento creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }
            try
            {
                ViewBag.UnidadesMedida = Crud<UnidadMedida>.GetAll();
            }
            catch { ViewBag.UnidadesMedida = new List<UnidadMedida>(); }
            return View(item);
        }

        // GET: Implementos/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<Reactivo>.GetById(id);
                if (item == null || item.Categoria?.ToLower() != "implementos") return NotFound();

                try
                {
                    ViewBag.UnidadesMedida = Crud<UnidadMedida>.GetAll();
                }
                catch { ViewBag.UnidadesMedida = new List<UnidadMedida>(); }

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Implementos/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<Reactivo>.GetById(id);
                if (item == null || item.Categoria?.ToLower() != "implementos") return NotFound();

                try
                {
                    ViewBag.UnidadesMedida = Crud<UnidadMedida>.GetAll();
                }
                catch { ViewBag.UnidadesMedida = new List<UnidadMedida>(); }

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Implementos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Reactivo item)
        {
            if (id != item.Id) return BadRequest();
            
            item.Categoria = "Implementos"; // Forzar categoría por si acaso

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<Reactivo>.Update(id, item);
                    TempData["Success"] = "Implemento actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                     TempData["Error"] = "Error al actualizar: " + ex.Message;
                }
            }
            try
            {
                ViewBag.UnidadesMedida = Crud<UnidadMedida>.GetAll();
            }
            catch { ViewBag.UnidadesMedida = new List<UnidadMedida>(); }
            return View(item);
        }

        // GET: Implementos/Delete/5
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<Reactivo>.GetById(id);
                if (item == null || item.Categoria?.ToLower() != "implementos") return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Implementos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var item = Crud<Reactivo>.GetById(id);
                if(item != null && item.Categoria?.ToLower() == "implementos") {
                    Crud<Reactivo>.Delete(id);
                    TempData["Success"] = "Implemento eliminado correctamente.";
                }
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
