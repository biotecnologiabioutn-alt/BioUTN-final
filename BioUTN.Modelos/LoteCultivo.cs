using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BioUTN.Modelos
{
    [Table("LotesCultivo")]
    public class LoteCultivo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(PlantaMadre))]
        public int IdPlantaMadre { get; set; }
        public virtual PlantaMadre? PlantaMadre { get; set; }

        [ForeignKey(nameof(LotePadre))]
        public int? IdLotePadre { get; set; }
        public virtual LoteCultivo? LotePadre { get; set; }

        [ForeignKey(nameof(UnidadFrascoOrigen))]
        public int? IdUnidadFrascoOrigen { get; set; }
        public virtual UnidadFrasco? UnidadFrascoOrigen { get; set; }

        [Required, ForeignKey(nameof(MedioCultivo))]
        public int IdMedioCultivo { get; set; }
        public virtual MedioCultivo? MedioCultivo { get; set; }

        [Required, ForeignKey(nameof(Proyecto))]
        public int IdProyecto { get; set; }
        public virtual Proyecto? Proyecto { get; set; }

        [Required, ForeignKey(nameof(UbicacionFisica))]
        public int IdUbicacionFisica { get; set; }
        public virtual UbicacionFisica? UbicacionFisica { get; set; }

        [Required, ForeignKey(nameof(FaseCultivo))]
        public int IdFaseCultivo { get; set; }
        public virtual FaseCultivo? FaseCultivo { get; set; }

        [Required, ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [Required]
        [MaxLength(50)]
        public string CodigoTrazabilidad { get; set; } = string.Empty;

        public int NumeroRepique { get; set; } = 0;

        public DateTime FechaSiembra { get; set; }

        public int TotalUnidades { get; set; }

        public int TotalExplantesIntroducidos { get; set; } = 0;

        public string? ChecklistSiembra { get; set; }

        public string? UrlQr { get; set; }

        [Required]
        [MaxLength(100)]
        public string TipoExplante { get; set; } = string.Empty;

        public int ExplantesPorUnidad { get; set; } = 1;

        [ForeignKey(nameof(DocumentoProtocolo))]
        public int? IdDocumentoProtocolo { get; set; }
        public virtual Documento? DocumentoProtocolo { get; set; }

        public int FrecuenciaMonitoreoDias { get; set; } = 7;

        [MaxLength(200)]
        public string? ResponsablesSiembraNombres { get; set; }

        public bool Activo { get; set; } = true;

        // Navegación
        [InverseProperty("LotePadre")]
        public virtual List<LoteCultivo>? LotesHijos { get; set; }

        [InverseProperty("LoteCultivo")]
        public virtual List<UnidadFrasco>? UnidadesFrasco { get; set; }

        public virtual List<EntradaDiario>? EntradasDiario { get; set; }
    }
}
