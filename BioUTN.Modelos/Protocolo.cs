using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("Protocolos")]
    public class Protocolo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Especie))]
        public int? IdEspecie { get; set; }
        public virtual Especie? Especie { get; set; }

        [Required, ForeignKey(nameof(Usuario))]
        public int IdUsuarioAutor { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [Required]
        [MaxLength(50)]
        public string FaseProtocolo { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        public string? UrlArchivoPdf { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public bool Activo { get; set; } = true;
    }
}
