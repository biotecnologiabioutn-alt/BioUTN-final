using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("TareasOperativas")]
    public class TareaOperativa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string TipoTarea { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = string.Empty;

        public DateTime FechaAsignacion { get; set; }

        public DateTime? FechaCompletada { get; set; }
    }
}
