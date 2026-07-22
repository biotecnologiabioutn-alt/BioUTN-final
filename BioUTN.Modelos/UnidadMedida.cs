using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("UnidadesMedida")]
    public class UnidadMedida
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Abreviatura { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string UnidadBase { get; set; } = string.Empty;

        [Required]
        public decimal FactorConversion { get; set; } = 1;
    }
}
