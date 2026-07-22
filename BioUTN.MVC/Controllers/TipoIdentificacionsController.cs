using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico")]
    public class TipoIdentificacionsController : Controller
    {
        // GET: TipoIdentificacions
        public IActionResult Index()
        {
            try
            {
                var list = Crud<TipoIdentificacion>.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<TipoIdentificacion>());
            }
        }

        // GET: TipoIdentificacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoIdentificacions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TipoIdentificacion item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = Crud<TipoIdentificacion>.Create(item);
                    if (isSuccess)
                    {
                        TempData["Success"] = "Tipo de Identificación creado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Hubo un problema al crear el Tipo de Identificación.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }
            return View(item);
        }

        // GET: TipoIdentificacions/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<TipoIdentificacion>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: TipoIdentificacions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TipoIdentificacion item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = Crud<TipoIdentificacion>.Update(id, item);
                    if (isSuccess)
                    {
                        TempData["Success"] = "Tipo de Identificación actualizado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Hubo un problema al actualizar el Tipo de Identificación.";
                    }
                }
                catch (Exception ex)
                {
                     TempData["Error"] = "Error al actualizar: " + ex.Message;
                }
            }
            return View(item);
        }

        // GET: TipoIdentificacions/Delete/5
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<TipoIdentificacion>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: TipoIdentificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<TipoIdentificacion>.Delete(id);
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

