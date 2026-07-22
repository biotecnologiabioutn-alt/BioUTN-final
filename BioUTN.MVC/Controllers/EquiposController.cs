using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class EquiposController : Controller
    {
        // GET: Equipos
        public IActionResult Index()
        {
            try
            {
                var list = Crud<Equipo>.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<Equipo>());
            }
        }

        // GET: Equipos/Create
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Create(Equipo item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Crud<Equipo>.Create(item);
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

        // GET: Equipos/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<Equipo>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Equipos/Edit/5
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<Equipo>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Equipos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Edit(int id, Equipo item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = Crud<Equipo>.Update(id, item);
                    if (isSuccess)
                    {
                        TempData["Success"] = "Elemento actualizado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Hubo un problema al actualizar.";
                    }
                }
                catch (Exception ex)
                {
                     TempData["Error"] = "Error al actualizar: " + ex.Message;
                }
            }
            return View(item);
        }

        // GET: Equipos/Delete/5
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<Equipo>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<Equipo>.Delete(id);
                TempData["Success"] = "Elemento eliminado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
