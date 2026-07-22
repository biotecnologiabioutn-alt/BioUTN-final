using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;

namespace BioUTN.MVC.Controllers
{
    [Authorize]
    public class TaxonomiasController : Controller
    {
        // GET: Taxonomias
        public IActionResult Index()
        {
            try
            {
                var list = Crud<Taxonomia>.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<Taxonomia>());
            }
        }

        // GET: Taxonomias/Create
        [Authorize(Roles = "Tecnico")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Taxonomias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tecnico")]
        public IActionResult Create(Taxonomia item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = Crud<Taxonomia>.Create(item);
                    if (isSuccess)
                    {
                        TempData["Success"] = "Elemento creado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Hubo un problema al guardar la Taxonomía en la base de datos (Error 400/500). Verifica los datos.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }
            return View(item);
        }

        // GET: Taxonomias/Edit/5
        [Authorize(Roles = "Tecnico")]
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<Taxonomia>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Taxonomias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tecnico")]
        public IActionResult Edit(int id, Taxonomia item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<Taxonomia>.Update(id, item);
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

        // GET: Taxonomias/Delete/5
        [Authorize(Roles = "Tecnico")]
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<Taxonomia>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Taxonomias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tecnico")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<Taxonomia>.Delete(id);
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
