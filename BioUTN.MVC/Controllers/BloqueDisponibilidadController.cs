using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Linq;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico")] // Solo el técnico puede administrar horarios
    public class BloqueDisponibilidadController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var list = Crud<BloqueDisponibilidad>.GetAll();
                var equipos = Crud<Equipo>.GetAll();
                
                // Asociar objetos para la vista
                foreach(var b in list) {
                    b.Equipo = equipos.FirstOrDefault(e => e.Id == b.IdEquipo);
                }

                return View(list);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "ZonaTrabajo");
            }
        }

        public IActionResult Create()
        {
            ViewBag.IdEquipo = new SelectList(Crud<Equipo>.GetAll(), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public IActionResult Create(BloqueDisponibilidad modelo)
        {
            try
            {
                Crud<BloqueDisponibilidad>.Create(modelo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.IdEquipo = new SelectList(Crud<Equipo>.GetAll(), "Id", "Nombre", modelo.IdEquipo);
                return View(modelo);
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var obj = Crud<BloqueDisponibilidad>.GetById(id);
                if (obj == null) return NotFound();
                ViewBag.IdEquipo = new SelectList(Crud<Equipo>.GetAll(), "Id", "Nombre", obj.IdEquipo);
                return View(obj);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Edit(BloqueDisponibilidad modelo)
        {
            try
            {
                Crud<BloqueDisponibilidad>.Update(modelo.Id, modelo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {   
                ModelState.AddModelError("", ex.Message);
                ViewBag.IdEquipo = new SelectList(Crud<Equipo>.GetAll(), "Id", "Nombre", modelo.IdEquipo);
                return View(modelo);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                Crud<BloqueDisponibilidad>.Delete(id);
            }
            catch (Exception)
            {
                // Manejar error si es necesario
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
