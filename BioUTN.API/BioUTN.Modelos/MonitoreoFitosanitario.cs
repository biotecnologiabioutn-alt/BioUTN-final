using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("MonitoreosFitosanitarios")]
    public class MonitoreoFitosanitario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(UnidadFrasco))]
        public int IdUnidadFrasco { get; set; }
        public virtual UnidadFrasco? UnidadFrasco { get; set; }

        [Required, ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }

        public DateTime FechaEvaluacion { get; set; }

        public int UnidadesRevisadas { get; set; } = 1;

        [Required]
        [MaxLength(20)]
        public string NivelFenolizacion { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string NivelContaminacion { get; set; } = string.Empty;

        public bool Bacterias { get; set; } = false;

        public bool Hongos { get; set; } = false;

        public bool Muerte { get; set; } = false;

        [Required]
        [MaxLength(100)]
        public string Respuesta { get; set; } = string.Empty;

        public bool RequiereResiembra { get; set; } = false;

        public string Observaciones { get; set; } = string.Empty;
    }
}
