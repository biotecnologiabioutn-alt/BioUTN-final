using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("EntradasDiario")]
    public class EntradaDiario
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

        [ForeignKey(nameof(LoteCultivo))]
        public int? IdLoteCultivo { get; set; }
        public virtual LoteCultivo? LoteCultivo { get; set; }

        public DateTime FechaRegistro { get; set; }

        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Contenido { get; set; } = string.Empty;
    }
}
