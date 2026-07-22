using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("BloquesDisponibilidad")]
    public class BloqueDisponibilidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(Equipo))]
        public int IdEquipo { get; set; }
        public virtual Equipo? Equipo { get; set; }

        [Required]
        [MaxLength(15)]
        public string DiaSemana { get; set; } = string.Empty;

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        [Required]
        [MaxLength(20)]
        public string TipoReserva { get; set; } = string.Empty;
    }
}
