using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BioUTN.Modelos
{
    [Table("UnidadesFrasco")]
    public class UnidadFrasco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(LoteCultivo))]
        public int IdLoteCultivo { get; set; }
        public virtual LoteCultivo? LoteCultivo { get; set; }

        [Required]
        [MaxLength(150)]
        public string CodigoUnidad { get; set; } = string.Empty;

        public int NumeroResiembra { get; set; } = 0;

        [Required]
        [MaxLength(30)]
        public string Estado { get; set; } = "Saludable";

        public string? UrlQr { get; set; }

        public bool Activo { get; set; } = true;

        // Navegación
        public virtual List<MonitoreoFitosanitario>? Monitoreos { get; set; }
    }
}
