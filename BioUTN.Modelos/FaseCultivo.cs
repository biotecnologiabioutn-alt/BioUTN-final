using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("FasesCultivo")]
    public class FaseCultivo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string NombreFase { get; set; } = string.Empty;
    }
}
