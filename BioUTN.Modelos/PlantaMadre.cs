using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BioUTN.Modelos
{
    [Table("PlantasMadre")]
    public class PlantaMadre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(Especie))]
        public int IdEspecie { get; set; }
        public virtual Especie? Especie { get; set; }

        [Required, ForeignKey(nameof(Proyecto))]
        public int IdProyecto { get; set; }
        public virtual Proyecto? Proyecto { get; set; }

        [Required, ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [ForeignKey(nameof(ResponsableIntroduccion))]
        public int? IdResponsableIntroduccion { get; set; }
        public virtual Usuario? ResponsableIntroduccion { get; set; }

        [Required]
        [MaxLength(20)]
        public string CodigoAsignado { get; set; } = string.Empty;

        public DateTime FechaRecepcion { get; set; }

        [Required]
        [MaxLength(50)]
        public string EstadoFitosanitario { get; set; } = string.Empty;

        public string? UrlFotografia { get; set; }

        public string? UrlQr { get; set; }

        [Required]
        [MaxLength(255)]
        public string Procedencia { get; set; } = string.Empty;

        public DateTime? FechaColecta { get; set; }

        [ForeignKey(nameof(ResponsableColecta))]
        public int? IdResponsableColecta { get; set; }
        public virtual Usuario? ResponsableColecta { get; set; }

        [MaxLength(200)]
        public string? ResponsablesColectaNombres { get; set; }

        [ForeignKey(nameof(DocumentoPermiso))]
        public int? IdDocumentoPermiso { get; set; }
        public virtual Documento? DocumentoPermiso { get; set; }

        [ForeignKey(nameof(Protocolo))]
        public int? IdProtocolo { get; set; }
        public virtual Protocolo? Protocolo { get; set; }

        [MaxLength(150)]
        public string? HerbarioReferencia { get; set; }

        public string? Observaciones { get; set; }

        public bool Activo { get; set; } = true;

        // Navegación
        public virtual List<LoteCultivo>? LotesCultivo { get; set; }
        public virtual List<PermisoAmbiental>? PermisosAmbientales { get; set; }
    }
}
