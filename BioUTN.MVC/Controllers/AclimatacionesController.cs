using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class AclimatacionesController : Controller
    {
        // GET: Aclimataciones
        public IActionResult Index()
        {
            try
            {
                var list = Crud<Aclimatacion>.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<Aclimatacion>());
            }
        }

        // GET: Aclimataciones/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<Aclimatacion>.GetById(id);
                if (item == null) return NotFound();

                if (item.IdLoteEnraizado > 0)
                    item.LoteEnraizado = Crud<Enraizamiento>.GetById(item.IdLoteEnraizado);
                if (item.IdUsuarioResponsable > 0)
                    item.UsuarioResponsable = Crud<Usuario>.GetById(item.IdUsuarioResponsable);
                if (item.IdUbicacionFisica > 0)
                    item.UbicacionFisica = Crud<UbicacionFisica>.GetById(item.IdUbicacionFisica);
                if (item.IdDocumentoProtocolo != null)
                    item.Protocolo = Crud<Documento>.GetById(item.IdDocumentoProtocolo.Value);

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los detalles: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Aclimataciones/Create
        public IActionResult Create(int? idLoteOrigen)
        {
            try
            {
                // Find latest Enraizamiento for this LoteCultivo, or if idLoteOrigen is directly an Enraizamiento id (need to differentiate)
                // Wait, LoteCultivosController passes idLoteOrigen which is LoteCultivo ID. We need to find its Enraizamiento.
                // Or maybe we expect them to do Enraizamiento first.
                
                var enraizamientos = Crud<Enraizamiento>.GetAll();
                Enraizamiento? loteEnraizado = null;

                if (idLoteOrigen.HasValue)
                {
                    loteEnraizado = enraizamientos.FirstOrDefault(e => e.IdLoteOrigen == idLoteOrigen.Value);
                }

                var aclimatacion = new Aclimatacion
                {
                    IdLoteEnraizado = loteEnraizado?.Id ?? 0,
                    FechaTraspaso = DateTime.Now,
                    LavadoRaices = true,
                    CoberturaPlastica = true
                };

                ViewBag.LotesEnraizados = new SelectList(enraizamientos.Where(e => e.Activo), "Id", "Id", aclimatacion.IdLoteEnraizado);
                ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll().Where(u => u.Activo), "Id", "Nombres");
                ViewBag.UbicacionesFisicas = new SelectList(Crud<UbicacionFisica>.GetAll(), "Id", "NombreCuerpo");
                
                var todosDocumentos = Crud<Documento>.GetAll();
                foreach(var d in todosDocumentos) { if(d.CategoriaId > 0 && d.Categoria == null) d.Categoria = Crud<CategoriaDocumento>.GetById(d.CategoriaId); }
                var protocolos = todosDocumentos.Where(d => d.Categoria != null && d.Categoria.Nombre.Contains("Protocolo", StringComparison.OrdinalIgnoreCase)).ToList();
                ViewBag.Protocolos = new SelectList(protocolos, "Id", "Titulo");

                return View(aclimatacion);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al preparar formulario: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Aclimataciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Aclimatacion item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    item.FechaTraspaso = item.FechaTraspaso.ToUniversalTime();
                    if (item.FechaEvaluacionFinal.HasValue) item.FechaEvaluacionFinal = item.FechaEvaluacionFinal.Value.ToUniversalTime();

                    Crud<Aclimatacion>.Create(item);
                    TempData["Success"] = "Aclimatación registrada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }

            ViewBag.LotesEnraizados = new SelectList(Crud<Enraizamiento>.GetAll().Where(e => e.Activo), "Id", "Id", item.IdLoteEnraizado);
            ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll().Where(u => u.Activo), "Id", "Nombres", item.IdUsuarioResponsable);
            ViewBag.UbicacionesFisicas = new SelectList(Crud<UbicacionFisica>.GetAll(), "Id", "NombreCuerpo", item.IdUbicacionFisica);
            ViewBag.Protocolos = new SelectList(Crud<Documento>.GetAll().Where(d => d.CategoriaId > 0), "Id", "Titulo", item.IdDocumentoProtocolo);

            return View(item);
        }

        // GET: Aclimataciones/Edit/5
        public IActionResult Edit(int id)
        {
             try
            {
                var item = Crud<Aclimatacion>.GetById(id);
                if (item == null) return NotFound();

                ViewBag.LotesEnraizados = new SelectList(Crud<Enraizamiento>.GetAll().Where(e => e.Activo), "Id", "Id", item.IdLoteEnraizado);
                ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll().Where(u => u.Activo), "Id", "Nombres", item.IdUsuarioResponsable);
                ViewBag.UbicacionesFisicas = new SelectList(Crud<UbicacionFisica>.GetAll(), "Id", "NombreCuerpo", item.IdUbicacionFisica);
                ViewBag.Protocolos = new SelectList(Crud<Documento>.GetAll().Where(d => d.CategoriaId > 0), "Id", "Titulo", item.IdDocumentoProtocolo);

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los detalles: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Aclimataciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Aclimatacion item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    item.FechaTraspaso = item.FechaTraspaso.ToUniversalTime();
                    if (item.FechaEvaluacionFinal.HasValue) item.FechaEvaluacionFinal = item.FechaEvaluacionFinal.Value.ToUniversalTime();

                    Crud<Aclimatacion>.Update(id, item);
                    TempData["Success"] = "Aclimatación actualizada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al actualizar: " + ex.Message;
                }
            }

            ViewBag.LotesEnraizados = new SelectList(Crud<Enraizamiento>.GetAll().Where(e => e.Activo), "Id", "Id", item.IdLoteEnraizado);
            ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll().Where(u => u.Activo), "Id", "Nombres", item.IdUsuarioResponsable);
            ViewBag.UbicacionesFisicas = new SelectList(Crud<UbicacionFisica>.GetAll(), "Id", "NombreCuerpo", item.IdUbicacionFisica);
            ViewBag.Protocolos = new SelectList(Crud<Documento>.GetAll().Where(d => d.CategoriaId > 0), "Id", "Titulo", item.IdDocumentoProtocolo);

            return View(item);
        }

        // POST: Aclimataciones/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                Crud<Aclimatacion>.Delete(id);
                TempData["Success"] = "Elemento eliminado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se puede eliminar: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
