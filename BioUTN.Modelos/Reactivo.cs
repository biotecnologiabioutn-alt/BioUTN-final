using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("Reactivos")]
    public class Reactivo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public decimal StockActual { get; set; }

        public decimal StockMinimo { get; set; }

        [Required]
        public int IdUnidadMedida { get; set; }

        [ForeignKey("IdUnidadMedida")]
        public virtual UnidadMedida? UnidadMedidaObj { get; set; }

        [Required]
        [MaxLength(50)]
        public string Categoria { get; set; } = string.Empty;

        public DateTime? FechaCaducidad { get; set; }

        [MaxLength(150)]
        public string? Marca { get; set; }

        [MaxLength(150)]
        public string? ProveedorNombre { get; set; }

        [MaxLength(20)]
        public string? ProveedorTelefono { get; set; }

        [MaxLength(150)]
        public string? ProveedorSucursal { get; set; }

        [MaxLength(150)]
        public string? ProveedorEmail { get; set; }

        [MaxLength(150)]
        public string? ProveedorContacto { get; set; }

        [MaxLength(20)]
        public string? ProveedorRUC { get; set; }
    }
}
