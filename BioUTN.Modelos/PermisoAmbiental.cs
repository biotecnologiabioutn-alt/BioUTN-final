using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("PermisosAmbientales")]
    public class PermisoAmbiental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(PlantaMadre))]
        public int IdPlantaMadre { get; set; }
        public virtual PlantaMadre? PlantaMadre { get; set; }

        [Required]
        [MaxLength(100)]
        public string NumeroResolucion { get; set; } = string.Empty;

        [Required]
        public string UrlArchivoPdf { get; set; } = string.Empty;

        public DateTime FechaEmision { get; set; }
    }
}
