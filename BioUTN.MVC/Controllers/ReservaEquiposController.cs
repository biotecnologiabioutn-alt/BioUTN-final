using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;

namespace BioUTN.MVC.Controllers
{
    [Authorize]
    public class ReservaEquiposController : Controller
    {
        // GET: ReservaEquipos
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Index()
        {
            try
            {
                var list = (Crud<ReservaEquipo>.GetAll() ?? Array.Empty<ReservaEquipo>()).ToList();
                var equipos = (Crud<Equipo>.GetAll() ?? Array.Empty<Equipo>()).ToList();
                var usuarios = (Crud<Usuario>.GetAll() ?? Array.Empty<Usuario>()).ToList();

                foreach (var item in list)
                {
                    item.Equipo = equipos.Find(e => e.Id == item.IdEquipo);
                    item.Usuario = usuarios.Find(u => u.Id == item.IdUsuario);
                }

                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<ReservaEquipo>());
            }
        }

        // GET: ReservaEquipos/Calendario
        public IActionResult Calendario(int? equipoId, DateTime? fechaBase)
        {
            try
            {
                var equipos = (Crud<Equipo>.GetAll() ?? Array.Empty<Equipo>()).ToList();
                ViewBag.Equipos = equipos;

                var fecha = fechaBase ?? DateTime.Today;
                // Find monday of the week
                int diff = (7 + (fecha.DayOfWeek - DayOfWeek.Monday)) % 7;
                var lunes = fecha.AddDays(-1 * diff).Date;
                var viernes = lunes.AddDays(4).Date;

                ViewBag.FechaBase = fecha;
                ViewBag.Lunes = lunes;
                ViewBag.Viernes = viernes;
                ViewBag.EquipoSeleccionado = equipoId;

                var reservas = new List<ReservaEquipo>();
                var bloques = new List<BloqueDisponibilidad>();

                if (equipoId.HasValue)
                {
                    var todasReservas = (Crud<ReservaEquipo>.GetAll() ?? Array.Empty<ReservaEquipo>()).ToList();
                    reservas = todasReservas.Where(r => r.IdEquipo == equipoId.Value && r.FechaReserva.Date >= lunes && r.FechaReserva.Date <= viernes).ToList();
                    
                    var usuarios = (Crud<Usuario>.GetAll() ?? Array.Empty<Usuario>()).ToList();
                    foreach (var r in reservas)
                    {
                        r.Usuario = usuarios.Find(u => u.Id == r.IdUsuario);
                        r.Equipo = equipos.Find(e => e.Id == r.IdEquipo);
                    }

                    var todosBloques = (Crud<BloqueDisponibilidad>.GetAll() ?? Array.Empty<BloqueDisponibilidad>()).ToList();
                    bloques = todosBloques.Where(b => b.IdEquipo == equipoId.Value).ToList();
                    
                    ViewBag.EquipoObj = equipos.FirstOrDefault(e => e.Id == equipoId.Value);
                }

                ViewBag.Reservas = reservas;
                ViewBag.Bloques = bloques;

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar calendario: " + ex.Message;
                return View();
            }
        }


        // GET: ReservaEquipos/Create
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Create()
        {
            ViewBag.Equipos = (Crud<Equipo>.GetAll() ?? Array.Empty<Equipo>())
                .Where(e => e.Estado.Equals("Disponible", StringComparison.OrdinalIgnoreCase))
                .ToList();
            ViewBag.Usuarios = (Crud<Usuario>.GetAll() ?? Array.Empty<Usuario>()).ToList();
            return View();
        }

        // POST: ReservaEquipos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReservaEquipo item)
        {
            // Set initial status to Pendiente or Aprobada
            item.Estado = "Aprobada"; 

            // Validar que no haya cruces
            var list = (Crud<ReservaEquipo>.GetAll() ?? Array.Empty<ReservaEquipo>()).ToList();
            bool hasConflict = list.Exists(r => 
                r.IdEquipo == item.IdEquipo && 
                r.FechaReserva.Date == item.FechaReserva.Date &&
                r.Estado != "Cancelada" && r.Estado != "Rechazada" &&
                ((item.HoraInicio >= r.HoraInicio && item.HoraInicio < r.HoraFin) || 
                 (item.HoraFin > r.HoraInicio && item.HoraFin <= r.HoraFin) ||
                 (item.HoraInicio <= r.HoraInicio && item.HoraFin >= r.HoraFin))
            );

            if(hasConflict)
            {
                ModelState.AddModelError("", "El equipo ya se encuentra reservado en el horario seleccionado.");
            }
            else
            {
                var equipoObj = (Crud<Equipo>.GetById(item.IdEquipo));
                if(equipoObj != null && !equipoObj.Estado.Equals("Disponible", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("", $"No se puede reservar el equipo porque su estado actual es: {equipoObj.Estado}");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<ReservaEquipo>.Create(item);
                    TempData["Success"] = "Reserva creada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }

            ViewBag.Equipos = (Crud<Equipo>.GetAll() ?? Array.Empty<Equipo>())
                .Where(e => e.Estado.Equals("Disponible", StringComparison.OrdinalIgnoreCase))
                .ToList();
            ViewBag.Usuarios = (Crud<Usuario>.GetAll() ?? Array.Empty<Usuario>()).ToList();
            return View(item);
        }

        // GET: ReservaEquipos/Details/5
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<ReservaEquipo>.GetById(id);
                if (item == null) return NotFound();

                var equipos = (Crud<Equipo>.GetAll() ?? Array.Empty<Equipo>()).ToList();
                var usuarios = (Crud<Usuario>.GetAll() ?? Array.Empty<Usuario>()).ToList();

                item.Equipo = equipos.Find(e => e.Id == item.IdEquipo);
                item.Usuario = usuarios.Find(u => u.Id == item.IdUsuario);

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ReservaEquipos/Edit/5
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<ReservaEquipo>.GetById(id);
                if (item == null) return NotFound();

                ViewBag.Equipos = (Crud<Equipo>.GetAll() ?? Array.Empty<Equipo>()).ToList();
                ViewBag.Usuarios = (Crud<Usuario>.GetAll() ?? Array.Empty<Usuario>()).ToList();

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ReservaEquipos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ReservaEquipo item)
        {
            if (id != item.Id) return BadRequest();

            // Check conflicts again if they changed times, excluding self
            var list = (Crud<ReservaEquipo>.GetAll() ?? Array.Empty<ReservaEquipo>()).ToList();
            bool hasConflict = list.Exists(r => 
                r.Id != item.Id &&
                r.IdEquipo == item.IdEquipo && 
                r.FechaReserva.Date == item.FechaReserva.Date &&
                r.Estado != "Cancelada" && r.Estado != "Rechazada" &&
                ((item.HoraInicio >= r.HoraInicio && item.HoraInicio < r.HoraFin) || 
                 (item.HoraFin > r.HoraInicio && item.HoraFin <= r.HoraFin) ||
                 (item.HoraInicio <= r.HoraInicio && item.HoraFin >= r.HoraFin))
            );

            if(hasConflict)
            {
                ModelState.AddModelError("", "El equipo ya se encuentra reservado en el horario seleccionado.");
            }
            else
            {
                var equipoObj = (Crud<Equipo>.GetById(item.IdEquipo));
                if(equipoObj != null && !equipoObj.Estado.Equals("Disponible", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("", $"No se puede reservar el equipo porque su estado actual es: {equipoObj.Estado}");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = Crud<ReservaEquipo>.Update(id, item);
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

            ViewBag.Equipos = (Crud<Equipo>.GetAll() ?? Array.Empty<Equipo>()).ToList();
            ViewBag.Usuarios = (Crud<Usuario>.GetAll() ?? Array.Empty<Usuario>()).ToList();
            return View(item);
        }

        // GET: ReservaEquipos/Delete/5
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Delete(int id)
        {
            try
            {
                var item = Crud<ReservaEquipo>.GetById(id);
                if (item == null) return NotFound();

                var equipos = (Crud<Equipo>.GetAll() ?? Array.Empty<Equipo>()).ToList();
                var usuarios = (Crud<Usuario>.GetAll() ?? Array.Empty<Usuario>()).ToList();

                item.Equipo = equipos.Find(e => e.Id == item.IdEquipo);
                item.Usuario = usuarios.Find(u => u.Id == item.IdUsuario);

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ReservaEquipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<ReservaEquipo>.Delete(id);
                TempData["Success"] = "Elemento eliminado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: ReservaEquipos/Bloques
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Bloques()
        {
            try
            {
                var list = (Crud<BloqueDisponibilidad>.GetAll() ?? Array.Empty<BloqueDisponibilidad>()).ToList();
                var equipos = (Crud<Equipo>.GetAll() ?? Array.Empty<Equipo>()).ToList();
                
                foreach (var item in list)
                {
                    item.Equipo = equipos.Find(e => e.Id == item.IdEquipo);
                }

                ViewBag.Equipos = equipos;
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar bloques: " + ex.Message;
                return View(new List<BloqueDisponibilidad>());
            }
        }

        // POST: ReservaEquipos/CreateBloque
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBloque(BloqueDisponibilidad item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Crud<BloqueDisponibilidad>.Create(item);
                    TempData["Success"] = "Horario bloqueado correctamente.";
                }
                else
                {
                    TempData["Error"] = "Error al crear el bloqueo. Verifique los datos.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error interno: " + ex.Message;
            }
            return RedirectToAction(nameof(Bloques));
        }
        
        // POST: ReservaEquipos/DeleteBloque/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBloque(int id)
        {
            try
            {
                Crud<BloqueDisponibilidad>.Delete(id);
                TempData["Success"] = "Bloqueo eliminado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar: " + ex.Message;
            }
            return RedirectToAction(nameof(Bloques));
        }
    }
}
