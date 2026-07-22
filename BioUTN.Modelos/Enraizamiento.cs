using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("Enraizamientos")]
    public class Enraizamiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(LoteOrigen))]
        public int IdLoteOrigen { get; set; }
        public virtual LoteCultivo? LoteOrigen { get; set; }

        [Required]
        [ForeignKey(nameof(UsuarioResponsable))]
        public int IdUsuarioResponsable { get; set; }
        public virtual Usuario? UsuarioResponsable { get; set; }

        [Required]
        [ForeignKey(nameof(Proyecto))]
        public int IdProyecto { get; set; }
        public virtual Proyecto? Proyecto { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        [Required]
        public int NumeroPlantas { get; set; }

        public int SemanasEstimadas { get; set; }

        [ForeignKey(nameof(Protocolo))]
        public int? IdDocumentoProtocolo { get; set; }
        public virtual Documento? Protocolo { get; set; }
        
        public bool Activo { get; set; } = true;
    }
}
