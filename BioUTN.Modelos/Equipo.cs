using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BioUTN.Modelos
{
    [Table("Equipos")]
    public class Equipo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string CodigoInventario { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Marca { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Disponible";

        public DateTime? FechaProximoMantenimiento { get; set; }

        // Navegación
        public virtual List<BloqueDisponibilidad>? Bloques { get; set; }
        public virtual List<ReservaEquipo>? Reservas { get; set; }
    }
}
