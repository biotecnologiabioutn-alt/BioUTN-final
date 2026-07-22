using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("KardexMovimientos")]
    public class KardexMovimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(Reactivo))]
        public int IdReactivo { get; set; }
        public virtual Reactivo? Reactivo { get; set; }

        [Required, ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [Required]
        [MaxLength(20)]
        public string TipoMovimiento { get; set; } = string.Empty;

        public decimal Cantidad { get; set; }

        [MaxLength(250)]
        public string? Motivo { get; set; }

        public DateTime FechaMovimiento { get; set; } = DateTime.UtcNow;
    }
}
