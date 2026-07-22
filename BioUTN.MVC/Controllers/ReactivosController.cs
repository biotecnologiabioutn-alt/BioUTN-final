using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class ReactivosController : Controller
    {
        // GET: Reactivos
        public IActionResult Index()
        {
            try
            {
                var list = Crud<Reactivo>.GetAll()
                    .Where(r => r.Categoria != null && r.Categoria.ToLower() != "implementos")
                    .ToList();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<Reactivo>());
            }
        }

        // GET: Reactivos/Create
        public IActionResult Create()
        {
            try
            {
                ViewBag.UnidadesMedida = Crud<UnidadMedida>.GetAll();
            }
            catch { ViewBag.UnidadesMedida = new List<UnidadMedida>(); }
            return View();
        }

        // POST: Reactivos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reactivo item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Crud<Reactivo>.Create(item);
                    TempData["Success"] = "Elemento creado correctamente.";
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

        // GET: Reactivos/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<Reactivo>.GetById(id);
                if (item == null) return NotFound();

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

        // GET: Reactivos/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<Reactivo>.GetById(id);
                if (item == null) return NotFound();

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

        // POST: Reactivos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Reactivo item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<Reactivo>.Update(id, item);
                    TempData["Success"] = "Elemento actualizado correctamente.";
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

        // GET: Reactivos/Delete/5
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<Reactivo>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Reactivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<Reactivo>.Delete(id);
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


