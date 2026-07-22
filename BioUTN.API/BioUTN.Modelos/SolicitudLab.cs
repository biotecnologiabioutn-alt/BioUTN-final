using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("SolicitudesLab")]
    public class SolicitudLab
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Proyecto al que pertenece la solicitud
        [Required, ForeignKey(nameof(Proyecto))]
        public int IdProyecto { get; set; }
        public virtual Proyecto? Proyecto { get; set; }

        // Quien solicita (involucrado del proyecto)
        [Required, ForeignKey(nameof(Solicitante))]
        public int IdSolicitante { get; set; }
        public virtual Usuario? Solicitante { get; set; }

        // Técnico que gestiona (Aprobar/Rechazar)
        [ForeignKey(nameof(Aprobador))]
        public int? IdAprobador { get; set; }
        public virtual Usuario? Aprobador { get; set; }

        // Estado: Pendiente | Aprobada | Rechazada
        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Pendiente";

        // Tipo: Materiales | Equipo | Mixta
        [Required]
        [MaxLength(20)]
        public string TipoSolicitud { get; set; } = "Materiales";

        // Prioridad general (legacy)
        [Required]
        [MaxLength(20)]
        public string Prioridad { get; set; } = "Planificada";

        [MaxLength(20)]
        public string? PrioridadReactivos { get; set; }
        public DateTime? FechaReactivos { get; set; }
        [MaxLength(5)]
        public string? HoraReactivos { get; set; }

        [MaxLength(20)]
        public string? PrioridadImplementos { get; set; }
        public DateTime? FechaImplementos { get; set; }
        [MaxLength(5)]
        public string? HoraImplementos { get; set; }

        [MaxLength(500)]
        public string? Observaciones { get; set; }

        [MaxLength(500)]
        public string? MotivoRechazo { get; set; }

        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;

        public DateTime? FechaGestion { get; set; }

        // Datos de la reserva de equipo (si aplica)
        [ForeignKey(nameof(Equipo))]
        public int? IdEquipo { get; set; }
        public virtual Equipo? Equipo { get; set; }

        public DateTime? FechaReservaEquipo { get; set; }

        [MaxLength(5)]
        public string? HoraInicioEquipo { get; set; } // "08:00"

        [MaxLength(5)]
        public string? HoraFinEquipo { get; set; } // "10:00"

        // Navegación - Detalle de materiales
        public virtual List<SolicitudDetalleMaterial>? Detalles { get; set; }
    }
}
