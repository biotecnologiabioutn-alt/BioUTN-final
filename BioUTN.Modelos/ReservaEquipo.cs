using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("ReservasEquipo")]
    public class ReservaEquipo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(Equipo))]
        public int IdEquipo { get; set; }
        public virtual Equipo? Equipo { get; set; }

        [Required, ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [ForeignKey(nameof(BloqueDisponibilidad))]
        public int? IdBloque { get; set; }
        public virtual BloqueDisponibilidad? BloqueDisponibilidad { get; set; }

        public DateTime FechaReserva { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = string.Empty;

        public string? ObservacionesCheckOut { get; set; }

        // Vínculo opcional a una Solicitud unificada
        [ForeignKey(nameof(SolicitudLab))]
        public int? IdSolicitud { get; set; }
        public virtual SolicitudLab? SolicitudLab { get; set; }
    }
}
