using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("TiposPlanta")]
    public class TipoPlanta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;
    }
}
