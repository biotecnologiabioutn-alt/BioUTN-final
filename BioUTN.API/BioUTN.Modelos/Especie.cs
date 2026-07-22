using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BioUTN.Modelos
{
    [Table("Especies")]
    public class Especie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(Taxonomia))]
        public int IdTaxonomia { get; set; }
        public virtual Taxonomia? Taxonomia { get; set; }

        [Required, ForeignKey(nameof(TipoPlanta))]
        public int IdTipoPlanta { get; set; }
        public virtual TipoPlanta? TipoPlanta { get; set; }

        [Required]
        [MaxLength(10)]
        public string CodigoEstricto { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NombreCientifico { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NombreComun { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? SinonimosTaxonomicos { get; set; }

        [Required]
        [MaxLength(50)]
        public string CicloVida { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string CategoriaUso { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string EstadoConservacion { get; set; } = "Desconocido";

        public bool EsEndemica { get; set; } = false;

        public bool EsNativa { get; set; } = false;

        [Required]
        [MaxLength(255)]
        public string DistribucionNatural { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string ImportanciaEspecie { get; set; } = string.Empty;

        public short DiasResiembra { get; set; } = 90;

        public short MaxResiembras { get; set; } = 10;

        public bool Activo { get; set; } = true;

        // Navegación
        public virtual List<PlantaMadre>? PlantasMadre { get; set; }
        public virtual List<Protocolo>? Protocolos { get; set; }
    }
}
