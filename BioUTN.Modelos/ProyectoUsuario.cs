using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    public class ProyectoUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(Proyecto))]
        public int IdProyecto { get; set; }
        public virtual Proyecto? Proyecto { get; set; }

        [Required, ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [Required]
        [MaxLength(50)]
        public string RolEnProyecto { get; set; } = string.Empty; // "Dueño/Investigador", "Asistente/Practicante"
    }
}
