using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.MVC.Models;
using System.Collections.Generic;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System.Security.Claims;
using System.Linq;
using System;
using Newtonsoft.Json;

namespace BioUTN.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["HeaderTitle"] = User.Identity?.Name ?? "Hola";
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            ViewData["HeaderSubtitle"] = roles.Any() ? $"Rol: {string.Join(", ", roles)}" : "Rol: Usuario";
            ViewData["ShowLogout"] = true;

            int currentUserId = 0;
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int uid))
            {
                currentUserId = uid;
            }

            bool isTecnico = User.IsInRole("Tecnico") || User.IsInRole("Coordinador") || User.IsInRole("Tecnico de laboratorio") || User.IsInRole("Administrador");

            var lotes = (Crud<LoteCultivo>.GetAll() ?? Array.Empty<LoteCultivo>()).ToList();
            var misLotes = isTecnico ? lotes.Where(l => l.Activo).ToList() : lotes.Where(l => l.Activo && l.IdUsuario == currentUserId).ToList();

            var reservas = (Crud<ReservaEquipo>.GetAll() ?? Array.Empty<ReservaEquipo>()).ToList();
            var misReservas = isTecnico ? reservas.ToList() : reservas.Where(r => r.IdUsuario == currentUserId).ToList();

            var equipos = (Crud<Equipo>.GetAll() ?? Array.Empty<Equipo>()).ToList();

            var mockTasks = new Dictionary<string, List<object>>();

            // Add reservas
            foreach (var r in misReservas)
            {
                string dateStr = r.FechaReserva.ToString("yyyy-MM-dd");
                if (!mockTasks.ContainsKey(dateStr)) mockTasks[dateStr] = new List<object>();
                
                var equipo = equipos.FirstOrDefault(e => e.Id == r.IdEquipo);
                string eqName = equipo != null ? equipo.Nombre : "Equipo";

                string type = "success";
                string icon = "fa-check";
                string statusText = r.Estado;

                if (r.Estado.Contains("Pendiente", StringComparison.OrdinalIgnoreCase))
                {
                    type = "warning";
                    icon = "fa-clock";
                    statusText = isTecnico ? "Requiere Aprobación" : "Pendiente";
                }
                else if (r.Estado.Contains("Rechazad", StringComparison.OrdinalIgnoreCase) || r.Estado.Contains("Cancelad", StringComparison.OrdinalIgnoreCase))
                {
                    type = "danger";
                    icon = "fa-times";
                }

                mockTasks[dateStr].Add(new {
                    title = (isTecnico && r.IdUsuario != currentUserId ? "[General] " : "") + "Reserva: " + eqName,
                    subtitle1 = "Horario: " + r.HoraInicio.ToString(@"hh\:mm") + " - " + r.HoraFin.ToString(@"hh\:mm"),
                    subtitle2 = "Estado: " + r.Estado,
                    deadline = r.FechaReserva.ToString("dd/MM/yyyy"),
                    type = type,
                    icon = icon,
                    status = statusText
                });
            }

            // Add Lote monitorings (simulated next 30 days)
            DateTime today = DateTime.Today;
            foreach (var l in misLotes)
            {
                int freq = l.FrecuenciaMonitoreoDias > 0 ? l.FrecuenciaMonitoreoDias : 7;
                for (int i = 0; i < 60; i++)
                {
                    DateTime checkDate = l.FechaSiembra.AddDays(i);
                    if (checkDate >= today.AddDays(-5) && checkDate <= today.AddDays(30))
                    {
                        if ((checkDate - l.FechaSiembra).Days % freq == 0)
                        {
                            string dateStr = checkDate.ToString("yyyy-MM-dd");
                            if (!mockTasks.ContainsKey(dateStr)) mockTasks[dateStr] = new List<object>();

                            bool isPast = checkDate < today;
                            bool isToday = checkDate == today;

                            string type = isPast ? "danger" : (isToday ? "warning" : "success");
                            string icon = isPast ? "fa-times-circle" : (isToday ? "fa-exclamation-circle" : "fa-clock");
                            string status = isPast ? "Atrasado" : (isToday ? "Revisar Hoy" : "Próximamente");

                            mockTasks[dateStr].Add(new {
                                title = (isTecnico && l.IdUsuario != currentUserId ? "[General] " : "") + "Monitoreo: " + l.CodigoTrazabilidad,
                                subtitle1 = "Revisión (" + freq + " días) - Fase: " + (l.FaseCultivo?.NombreFase ?? "Crecimiento"),
                                subtitle2 = "Explant. Intro: " + l.TotalExplantesIntroducidos,
                                deadline = checkDate.ToString("dd/MM/yyyy"),
                                type = type,
                                icon = icon,
                                status = status
                            });
                        }
                    }
                }
            }

            ViewBag.MockTasksJson = JsonConvert.SerializeObject(mockTasks);

            var proyectos = new List<ProyectoViewModel>
            {
                new ProyectoViewModel 
                { 
                    IdLote = "Anth-01-R2", 
                    NombreCientifico = "Saintpaulia ionantha", 
                    TipoProyecto = "Tesis", 
                    Etiqueta = "Mi Tesis",
                    Detalles = "Última rev: Ayer"
                }
            };

            return View(proyectos);
        }

        public IActionResult Landing()
        {
            ViewData["HideHeader"] = true;
            return View();
        }
    }
}
