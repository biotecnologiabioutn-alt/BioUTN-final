using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BioUTN.Modelos
{
    public class CategoriaDocumento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(255)]
        public string? Descripcion { get; set; }

        public virtual ICollection<Documento>? Documentos { get; set; }
    }
}
