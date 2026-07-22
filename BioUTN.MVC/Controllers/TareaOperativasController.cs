using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class TareaOperativasController : Controller
    {
        // GET: TareaOperativas
        public IActionResult Index()
        {
            try
            {
                var list = Crud<TareaOperativa>.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<TareaOperativa>());
            }
        }

        // GET: TareaOperativas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TareaOperativas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TareaOperativa item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Crud<TareaOperativa>.Create(item);
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

        // GET: TareaOperativas/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<TareaOperativa>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: TareaOperativas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TareaOperativa item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<TareaOperativa>.Update(id, item);
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

        // GET: TareaOperativas/Delete/5
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<TareaOperativa>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: TareaOperativas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<TareaOperativa>.Delete(id);
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


