using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("UbicacionesFisicas")]
    public class UbicacionFisica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string CodigoAnaquel { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string NombreCuerpo { get; set; } = string.Empty;

        public int NumeroPiso { get; set; }

        public int LimiteFrascos { get; set; }

        [Required]
        [MaxLength(20)]
        public string EstadoUbicacion { get; set; } = string.Empty;
    }
}
