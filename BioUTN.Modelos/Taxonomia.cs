using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BioUTN.Modelos
{
    [Table("Taxonomia")]
    public class Taxonomia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Dominio { get; set; } = "Eukaryota";

        [Required]
        [MaxLength(50)]
        public string Reino { get; set; } = "Plantae";

        [MaxLength(50)]
        public string? SubReino { get; set; }

        [Required]
        [MaxLength(50)]
        public string FiloDivision { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Clase { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Orden { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Familia { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? SubFamilia { get; set; }

        [MaxLength(50)]
        public string? Tribu { get; set; }

        [MaxLength(50)]
        public string? SubTribu { get; set; }

        [Required]
        [MaxLength(50)]
        public string Genero { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Especie { get; set; } = string.Empty;

        // Navegación
        public virtual List<Especie>? Especies { get; set; }
    }
}
