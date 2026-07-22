using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("MediosCultivo")]
    public class MedioCultivo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Siglas { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public string Componentes { get; set; } = string.Empty;

        [ForeignKey(nameof(Usuario))]
        public int? IdUsuarioPropietario { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
