using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("SolicitudesDetalleMaterial")]
    public class SolicitudDetalleMaterial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(Solicitud))]
        public int IdSolicitud { get; set; }
        public virtual SolicitudLab? Solicitud { get; set; }

        // Reactivo o Implemento solicitado
        [Required, ForeignKey(nameof(Reactivo))]
        public int IdReactivo { get; set; }
        public virtual Reactivo? Reactivo { get; set; }

        [Required]
        public decimal CantidadSolicitada { get; set; }

        [MaxLength(200)]
        public string? Observacion { get; set; }
    }
}
