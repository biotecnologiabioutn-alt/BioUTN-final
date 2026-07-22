using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("Aclimataciones")]
    public class Aclimatacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(LoteEnraizado))]
        public int IdLoteEnraizado { get; set; }
        public virtual Enraizamiento? LoteEnraizado { get; set; }

        [Required]
        [ForeignKey(nameof(UsuarioResponsable))]
        public int IdUsuarioResponsable { get; set; }
        public virtual Usuario? UsuarioResponsable { get; set; }

        [Required]
        public DateTime FechaTraspaso { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(200)]
        public string SustratoUtilizado { get; set; } = string.Empty;

        [Required]
        public bool LavadoRaices { get; set; }

        [Required]
        public bool CoberturaPlastica { get; set; }

        [Required]
        [ForeignKey(nameof(UbicacionFisica))]
        public int IdUbicacionFisica { get; set; }
        public virtual UbicacionFisica? UbicacionFisica { get; set; }

        [ForeignKey(nameof(Protocolo))]
        public int? IdDocumentoProtocolo { get; set; }
        public virtual Documento? Protocolo { get; set; }

        public decimal? HumedadInicial { get; set; }
        public decimal? TemperaturaControlada { get; set; }

        public DateTime? FechaEvaluacionFinal { get; set; }
        public int? PlantasSobrevivientes { get; set; }
        public int? PlantasMuertas { get; set; }
        
        [MaxLength(2000)]
        public string? Observaciones { get; set; }
        
        [MaxLength(255)]
        public string? FotografiaUrl { get; set; }

        public bool Activo { get; set; } = true;
    }
}
