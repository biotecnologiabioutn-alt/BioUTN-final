using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico")]
    public class UbicacionFisicasController : Controller
    {
        // GET: UbicacionFisicas
        public IActionResult Index()
        {
            try
            {
                var list = Crud<UbicacionFisica>.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<UbicacionFisica>());
            }
        }

        // GET: UbicacionFisicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UbicacionFisicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UbicacionFisica item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Crud<UbicacionFisica>.Create(item);
                    TempData["Success"] = "Elemento creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }
            return View(item);
        }

        // GET: UbicacionFisicas/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<UbicacionFisica>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: UbicacionFisicas/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<UbicacionFisica>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: UbicacionFisicas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UbicacionFisica item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<UbicacionFisica>.Update(id, item);
                    TempData["Success"] = "Elemento actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                     TempData["Error"] = "Error al actualizar: " + ex.Message;
                }
            }
            return View(item);
        }

        // GET: UbicacionFisicas/Delete/5
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<UbicacionFisica>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: UbicacionFisicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<UbicacionFisica>.Delete(id);
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

