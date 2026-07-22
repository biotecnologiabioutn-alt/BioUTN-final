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
    public class EnraizamientosController : Controller
    {
        // GET: Enraizamientos
        public IActionResult Index()
        {
            try
            {
                var list = Crud<Enraizamiento>.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<Enraizamiento>());
            }
        }

        // GET: Enraizamientos/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<Enraizamiento>.GetById(id);
                if (item == null) return NotFound();

                if (item.IdLoteOrigen > 0)
                    item.LoteOrigen = Crud<LoteCultivo>.GetById(item.IdLoteOrigen);
                if (item.IdUsuarioResponsable > 0)
                    item.UsuarioResponsable = Crud<Usuario>.GetById(item.IdUsuarioResponsable);
                if (item.IdProyecto > 0)
                    item.Proyecto = Crud<Proyecto>.GetById(item.IdProyecto);
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

        // GET: Enraizamientos/Create
        public IActionResult Create(int? idLoteOrigen)
        {
            try
            {
                var loteOrigen = idLoteOrigen.HasValue ? Crud<LoteCultivo>.GetById(idLoteOrigen.Value) : null;
                
                var enraizamiento = new Enraizamiento
                {
                    IdLoteOrigen = idLoteOrigen ?? 0,
                    FechaInicio = DateTime.Now,
                    IdProyecto = loteOrigen?.IdProyecto ?? 0,
                    NumeroPlantas = loteOrigen?.TotalUnidades ?? 0
                };

                ViewBag.LotesOrigen = new SelectList(Crud<LoteCultivo>.GetAll().Where(l => l.Activo), "Id", "CodigoTrazabilidad", enraizamiento.IdLoteOrigen);
                ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll().Where(u => u.Activo), "Id", "Nombres");
                ViewBag.Proyectos = new SelectList(Crud<Proyecto>.GetAll().Where(p => p.Activo), "Id", "NombreProyecto", enraizamiento.IdProyecto);
                
                var todosDocumentos = Crud<Documento>.GetAll();
                foreach(var d in todosDocumentos) { if(d.CategoriaId > 0 && d.Categoria == null) d.Categoria = Crud<CategoriaDocumento>.GetById(d.CategoriaId); }
                var protocolos = todosDocumentos.Where(d => d.Categoria != null && d.Categoria.Nombre.Contains("Protocolo", StringComparison.OrdinalIgnoreCase)).ToList();
                ViewBag.Protocolos = new SelectList(protocolos, "Id", "Titulo");

                return View(enraizamiento);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al preparar formulario: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Enraizamientos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Enraizamiento item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    item.FechaInicio = item.FechaInicio.ToUniversalTime();
                    Crud<Enraizamiento>.Create(item);
                    TempData["Success"] = "Enraizamiento registrado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }

            ViewBag.LotesOrigen = new SelectList(Crud<LoteCultivo>.GetAll().Where(l => l.Activo), "Id", "CodigoTrazabilidad", item.IdLoteOrigen);
            ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll().Where(u => u.Activo), "Id", "Nombres", item.IdUsuarioResponsable);
            ViewBag.Proyectos = new SelectList(Crud<Proyecto>.GetAll().Where(p => p.Activo), "Id", "NombreProyecto", item.IdProyecto);
            var todosDocumentos = Crud<Documento>.GetAll();
            var protocolos = todosDocumentos.Where(d => d.CategoriaId > 0).ToList();
            ViewBag.Protocolos = new SelectList(protocolos, "Id", "Titulo", item.IdDocumentoProtocolo);

            return View(item);
        }

        // POST: Enraizamientos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                Crud<Enraizamiento>.Delete(id);
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
