using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BioUTN.Modelos
{
    public class TipoCultivo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string NombreTipo { get; set; } = string.Empty; // Normal, Conservación, Callos

        public virtual List<LoteCultivo>? LotesCultivos { get; set; }
    }
}
