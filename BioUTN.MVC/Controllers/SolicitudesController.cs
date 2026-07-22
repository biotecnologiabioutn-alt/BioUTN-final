using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BioUTN.MVC.Controllers
{
    [Authorize]
    public class SolicitudesController : Controller
    {
        // ─────────────────────────────────────────────────────────────
        // GET: Solicitudes/Index
        // El técnico ve todas. El resto solo las suyas.
        // ─────────────────────────────────────────────────────────────
        public IActionResult Index()
        {
            try
            {
                var todasSolicitudes = Crud<SolicitudLab>.GetAll();
                bool esTecnico = User.IsInRole("Coordinador") || User.IsInRole("Tecnico");

                List<SolicitudLab> solicitudes;

                if (esTecnico)
                {
                    solicitudes = todasSolicitudes.OrderByDescending(s => s.FechaSolicitud).ToList();
                }
                else
                {
                    // Solo ve sus propias solicitudes
                    var usuarioIdStr = User.FindFirst("IdUsuario")?.Value;
                    if (int.TryParse(usuarioIdStr, out int uid))
                    {
                        solicitudes = todasSolicitudes
                            .Where(s => s.IdSolicitante == uid)
                            .OrderByDescending(s => s.FechaSolicitud)
                            .ToList();
                    }
                    else
                    {
                        solicitudes = new List<SolicitudLab>();
                    }
                }

                ViewBag.EsTecnico = esTecnico;

                // Cargar todos los reactivos/equipos para resolver nombres
                try { ViewBag.Reactivos = Crud<Reactivo>.GetAll(); } 
                catch { ViewBag.Reactivos = new List<Reactivo>(); }
                try { ViewBag.Equipos = Crud<Equipo>.GetAll(); } 
                catch { ViewBag.Equipos = new List<Equipo>(); }
                try { ViewBag.Proyectos = Crud<Proyecto>.GetAll(); } 
                catch { ViewBag.Proyectos = new List<Proyecto>(); }
                try { ViewBag.Usuarios = Crud<Usuario>.GetAll(); } 
                catch { ViewBag.Usuarios = new List<Usuario>(); }

                return View(solicitudes);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar solicitudes: " + ex.Message;
                return View(new List<SolicitudLab>());
            }
        }

        // ─────────────────────────────────────────────────────────────
        // GET: Solicitudes/Create
        // ─────────────────────────────────────────────────────────────
        public IActionResult Create()
        {
            CargarViewBags();
            return View(new SolicitudLab());
        }

        // ─────────────────────────────────────────────────────────────
        // POST: Solicitudes/Create
        // ─────────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SolicitudLab solicitud,
            List<int>? reactivoIds,
            List<decimal>? cantidades,
            List<string>? observacionesLinea)
        {
            // Inyectar solicitante desde sesión
            var usuarioIdStr = User.FindFirst("IdUsuario")?.Value;
            if (int.TryParse(usuarioIdStr, out int uid))
            {
                solicitud.IdSolicitante = uid;
            }

            solicitud.FechaSolicitud = DateTime.UtcNow;
            solicitud.Estado = "Pendiente";

            // Determinar TipoSolicitud
            bool tieneMateriales = reactivoIds != null && reactivoIds.Any(id => id > 0);
            bool tieneEquipo = solicitud.IdEquipo.HasValue && solicitud.IdEquipo > 0;

            if (tieneMateriales && tieneEquipo) solicitud.TipoSolicitud = "Mixta";
            else if (tieneEquipo) solicitud.TipoSolicitud = "Equipo";
            else solicitud.TipoSolicitud = "Materiales";

            // Remover validación de campos no necesarios en este contexto
            ModelState.Remove("Solicitante");
            ModelState.Remove("Proyecto");
            ModelState.Remove("Equipo");
            ModelState.Remove("Aprobador");
            ModelState.Remove("Detalles");
            ModelState.Remove("Prioridad"); // Se ignora pues ahora es por categoría
            
            solicitud.Prioridad = "Mixta"; // Valor legacy por defecto

            if (ModelState.IsValid)
            {
                if (tieneEquipo)
                {
                    var horaInicio = TimeSpan.Parse(solicitud.HoraInicioEquipo ?? "00:00");
                    var horaFin = TimeSpan.Parse(solicitud.HoraFinEquipo ?? "23:59");

                    // Validar que la hora inicio sea menor a hora fin
                    if (horaInicio >= horaFin)
                    {
                        ModelState.AddModelError("HoraInicioEquipo", "La hora de inicio debe ser anterior a la hora de fin.");
                    }
                    else
                    {
                        // Validar cruce con reservas ya confirmadas
                        var reservasConf = Crud<ReservaEquipo>.GetAll()
                            .Where(r => r.IdEquipo == solicitud.IdEquipo && r.FechaReserva.Date == solicitud.FechaReservaEquipo.Value.Date && r.Estado != "Cancelada")
                            .ToList();
                        bool cruceRes = reservasConf.Any(r => r.HoraInicio < horaFin && r.HoraFin > horaInicio);

                        // Validar cruce con otras solicitudes pendientes
                        var solicitudesPend = Crud<SolicitudLab>.GetAll()
                            .Where(s => s.Estado == "Pendiente" && s.IdEquipo == solicitud.IdEquipo && s.FechaReservaEquipo.HasValue && s.FechaReservaEquipo.Value.Date == solicitud.FechaReservaEquipo.Value.Date)
                            .ToList();
                        bool cruceSol = solicitudesPend.Any(s =>
                            TimeSpan.Parse(s.HoraInicioEquipo ?? "00:00") < horaFin &&
                            TimeSpan.Parse(s.HoraFinEquipo ?? "23:59") > horaInicio);

                        if (cruceRes || cruceSol)
                        {
                            ModelState.AddModelError("HoraInicioEquipo", "El equipo no está disponible en este horario (ya está reservado o hay una solicitud en curso).");
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        // 1. Guardar cabecera
                        Crud<SolicitudLab>.Create(solicitud);
                        // Recuperar el Id de la solicitud recién creada
                        var todasParaBuscar = Crud<SolicitudLab>.GetAll()
                            .Where(s => s.IdSolicitante == solicitud.IdSolicitante)
                            .OrderByDescending(s => s.FechaSolicitud)
                            .ToList();
                        var solicitudCreada = todasParaBuscar.FirstOrDefault();
                        int idNuevaSolicitud = solicitudCreada?.Id ?? 0;

                        // 2. Guardar líneas de materiales
                        if (tieneMateriales && reactivoIds != null && idNuevaSolicitud > 0)
                        {
                            for (int i = 0; i < reactivoIds.Count; i++)
                            {
                                if (reactivoIds[i] <= 0) continue;
                                var detalle = new SolicitudDetalleMaterial
                                {
                                    IdSolicitud = idNuevaSolicitud,
                                    IdReactivo = reactivoIds[i],
                                    CantidadSolicitada = cantidades != null && i < cantidades.Count ? cantidades[i] : 1,
                                    Observacion = observacionesLinea != null && i < observacionesLinea.Count ? observacionesLinea[i] : null
                                };
                                Crud<SolicitudDetalleMaterial>.Create(detalle);
                            }
                        }

                        TempData["Success"] = "Solicitud enviada correctamente. El técnico será notificado.";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = "Error al guardar: " + ex.Message;
                    }
                }
            }

            CargarViewBags();
            return View(solicitud);
        }

        // ─────────────────────────────────────────────────────────────
        // GET: Solicitudes/Details/5
        // ─────────────────────────────────────────────────────────────
        public IActionResult Details(int id)
        {
            try
            {
                var solicitud = Crud<SolicitudLab>.GetById(id);
                if (solicitud == null) return NotFound();

                // Cargar los detalles de materiales
                var detalles = Crud<SolicitudDetalleMaterial>.GetAll()
                    .Where(d => d.IdSolicitud == id)
                    .ToList();

                ViewBag.Detalles = detalles;
                CargarViewBags();
                ViewBag.EsTecnico = User.IsInRole("Coordinador") || User.IsInRole("Tecnico");

                return View(solicitud);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ─────────────────────────────────────────────────────────────
        // POST: Solicitudes/Aprobar/5
        // Solo Técnico o Coordinador
        // ─────────────────────────────────────────────────────────────
        [HttpPost]
        [Authorize(Roles = "Coordinador,Tecnico")]
        [ValidateAntiForgeryToken]
        public IActionResult Aprobar(int id)
        {
            try
            {
                var solicitud = Crud<SolicitudLab>.GetById(id);
                if (solicitud == null) return NotFound();
                if (solicitud.Estado != "Pendiente")
                {
                    TempData["Error"] = "Esta solicitud ya fue gestionada.";
                    return RedirectToAction(nameof(Index));
                }

                // Descontar el stock de materiales
                var detalles = Crud<SolicitudDetalleMaterial>.GetAll()
                    .Where(d => d.IdSolicitud == id)
                    .ToList();

                foreach (var detalle in detalles)
                {
                    var reactivo = Crud<Reactivo>.GetById(detalle.IdReactivo);
                    if (reactivo != null)
                    {
                        reactivo.StockActual -= detalle.CantidadSolicitada;
                        Crud<Reactivo>.Update(reactivo.Id, reactivo);

                        // Registrar en Kardex
                        var aprobadorIdStr = User.FindFirst("IdUsuario")?.Value;
                        int.TryParse(aprobadorIdStr, out int aprobadorId);

                        var kardex = new KardexMovimiento
                        {
                            IdReactivo = reactivo.Id,
                            IdUsuario = solicitud.IdSolicitante,
                            TipoMovimiento = "Egreso",
                            Cantidad = detalle.CantidadSolicitada,
                            Motivo = $"Solicitud #{solicitud.Id} - Proyecto: {solicitud.IdProyecto}",
                            FechaMovimiento = DateTime.UtcNow
                        };
                        Crud<KardexMovimiento>.Create(kardex);
                    }
                }

                // Si tiene equipo, crear la reserva automáticamente
                if (solicitud.IdEquipo.HasValue && solicitud.FechaReservaEquipo.HasValue)
                {
                    TimeSpan horaInicio = TimeSpan.Parse(solicitud.HoraInicioEquipo ?? "08:00");
                    TimeSpan horaFin = TimeSpan.Parse(solicitud.HoraFinEquipo ?? "09:00");

                    var reserva = new ReservaEquipo
                    {
                        IdEquipo = solicitud.IdEquipo.Value,
                        IdUsuario = solicitud.IdSolicitante,
                        FechaReserva = solicitud.FechaReservaEquipo.Value,
                        HoraInicio = horaInicio,
                        HoraFin = horaFin,
                        Estado = "Confirmada",
                        IdSolicitud = solicitud.Id
                    };
                    Crud<ReservaEquipo>.Create(reserva);
                }

                // Actualizar la solicitud
                var aprobadorStr = User.FindFirst("IdUsuario")?.Value;
                int.TryParse(aprobadorStr, out int aprobId);
                solicitud.Estado = "Aprobada";
                solicitud.IdAprobador = aprobId > 0 ? aprobId : null;
                solicitud.FechaGestion = DateTime.UtcNow;
                Crud<SolicitudLab>.Update(solicitud.Id, solicitud);

                TempData["Success"] = $"Solicitud #{id} aprobada. Stock descontado y reserva creada.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al aprobar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ─────────────────────────────────────────────────────────────
        // POST: Solicitudes/Rechazar/5
        // ─────────────────────────────────────────────────────────────
        [HttpPost]
        [Authorize(Roles = "Coordinador,Tecnico")]
        [ValidateAntiForgeryToken]
        public IActionResult Rechazar(int id, string? motivoRechazo)
        {
            try
            {
                var solicitud = Crud<SolicitudLab>.GetById(id);
                if (solicitud == null) return NotFound();
                if (solicitud.Estado != "Pendiente")
                {
                    TempData["Error"] = "Esta solicitud ya fue gestionada.";
                    return RedirectToAction(nameof(Index));
                }

                var aprobadorStr = User.FindFirst("IdUsuario")?.Value;
                int.TryParse(aprobadorStr, out int aprobId);

                solicitud.Estado = "Rechazada";
                solicitud.MotivoRechazo = motivoRechazo ?? "Sin motivo especificado";
                solicitud.IdAprobador = aprobId > 0 ? aprobId : null;
                solicitud.FechaGestion = DateTime.UtcNow;
                Crud<SolicitudLab>.Update(solicitud.Id, solicitud);

                TempData["Warning"] = $"Solicitud #{id} rechazada.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al rechazar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ─────────────────────────────────────────────────────────────
        // Helper
        // ─────────────────────────────────────────────────────────────
        private void CargarViewBags()
        {
            try { ViewBag.Proyectos = Crud<Proyecto>.GetAll().Where(p => p.Estado == "Activo").ToList(); }
            catch { ViewBag.Proyectos = new List<Proyecto>(); }

            try { ViewBag.Reactivos = Crud<Reactivo>.GetAll(); }
            catch { ViewBag.Reactivos = new List<Reactivo>(); }

            try { ViewBag.Equipos = Crud<Equipo>.GetAll().Where(e => e.Estado == "Disponible").ToList(); }
            catch { ViewBag.Equipos = new List<Equipo>(); }

            try { ViewBag.Usuarios = Crud<Usuario>.GetAll(); }
            catch { ViewBag.Usuarios = new List<Usuario>(); }
        }
    }
}
