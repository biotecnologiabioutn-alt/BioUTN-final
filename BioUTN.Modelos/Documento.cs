using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    public class Documento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        [Required, ForeignKey(nameof(Categoria))]
        public int CategoriaId { get; set; }
        public virtual CategoriaDocumento? Categoria { get; set; } 

        [Required]
        public string UrlArchivo { get; set; } = string.Empty; 

        public DateTime FechaSubida { get; set; }

        [Required, ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [ForeignKey(nameof(LoteCultivo))]
        public int? IdLoteCultivo { get; set; }
        public virtual LoteCultivo? LoteCultivo { get; set; }

        // [OPCIONAL] Si el documento es un Protocolo global no lleva proyecto, pero si es de una Tesis, se liga al Proyecto.
        [ForeignKey(nameof(Proyecto))]
        public int? IdProyecto { get; set; }
        public virtual Proyecto? Proyecto { get; set; }
    }
}
