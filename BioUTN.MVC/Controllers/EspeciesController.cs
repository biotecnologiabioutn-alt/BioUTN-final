using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BioUTN.MVC.Controllers
{
    [Authorize]
    public class EspeciesController : Controller
    {
        // GET: Especies
        public IActionResult Index()
        {
            try
            {
                var list = Crud<Especie>.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<Especie>());
            }
        }

        // GET: Especies/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<Especie>.GetById(id);
                if (item == null) return NotFound();

                if (item.IdTaxonomia > 0)
                    item.Taxonomia = Crud<Taxonomia>.GetById(item.IdTaxonomia);
                if (item.IdTipoPlanta > 0)
                    item.TipoPlanta = Crud<TipoPlanta>.GetById(item.IdTipoPlanta);

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los detalles: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Especies/Create
        [Authorize(Roles = "Tecnico")]
        public IActionResult Create()
        {
            ViewBag.Taxonomias = new SelectList(Crud<Taxonomia>.GetAll(), "Id", "Especie");
            ViewBag.TipoPlantas = new SelectList(Crud<TipoPlanta>.GetAll(), "Id", "Nombre");
            return View();
        }

        // POST: Especies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tecnico")]
        public IActionResult Create(Especie item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = Crud<Especie>.Create(item);
                    if (isSuccess)
                    {
                        TempData["Success"] = "Elemento creado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Hubo un problema al guardar la Especie en la base de datos (Error 400/500). Verifica los datos.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }
            ViewBag.Taxonomias = new SelectList(Crud<Taxonomia>.GetAll(), "Id", "Especie", item.IdTaxonomia);
            ViewBag.TipoPlantas = new SelectList(Crud<TipoPlanta>.GetAll(), "Id", "Nombre", item.IdTipoPlanta);
            return View(item);
        }

        // GET: Especies/Edit/5
        [Authorize(Roles = "Tecnico")]
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<Especie>.GetById(id);
                if (item == null) return NotFound();

                ViewBag.Taxonomias = new SelectList(Crud<Taxonomia>.GetAll(), "Id", "Especie", item.IdTaxonomia);
                ViewBag.TipoPlantas = new SelectList(Crud<TipoPlanta>.GetAll(), "Id", "Nombre", item.IdTipoPlanta);
                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Especies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tecnico")]
        public IActionResult Edit(int id, Especie item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<Especie>.Update(id, item);
                    TempData["Success"] = "Elemento actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                     TempData["Error"] = "Error al actualizar: " + ex.Message;
                }
            }
            ViewBag.Taxonomias = new SelectList(Crud<Taxonomia>.GetAll(), "Id", "Especie", item.IdTaxonomia);
            ViewBag.TipoPlantas = new SelectList(Crud<TipoPlanta>.GetAll(), "Id", "Nombre", item.IdTipoPlanta);
            return View(item);
        }

        // GET: Especies/Delete/5
        [Authorize(Roles = "Tecnico")]
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<Especie>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Especies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tecnico")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<Especie>.Delete(id);
                TempData["Success"] = "Elemento eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se puede eliminar (posiblemente en uso): " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Especies/CrearTipoPlantaAjax
        [HttpPost]
        [Authorize(Roles = "Tecnico")]
        public IActionResult CrearTipoPlantaAjax(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre)) return BadRequest("El nombre es requerido.");
                
                var nuevo = new TipoPlanta { Nombre = nombre.Trim() };
                bool exito = Crud<TipoPlanta>.Create(nuevo);
                
                if (exito)
                {
                    var lista = Crud<TipoPlanta>.GetAll();
                    return Json(new { success = true, data = lista });
                }
                return BadRequest("No se pudo crear en la base de datos.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

