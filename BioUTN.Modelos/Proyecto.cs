using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BioUTN.Modelos
{
    [Table("Proyectos")]
    public class Proyecto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string NombreProyecto { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string TipoProyecto { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Activo";

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [Required, ForeignKey(nameof(Usuario))]
        public int IdUsuarioResponsable { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [ForeignKey(nameof(Especie))]
        public int? IdEspecie { get; set; }
        public virtual Especie? Especie { get; set; }

        // Campos para Fichas (Carpeta Verde/Azul)
        [ForeignKey(nameof(Director))]
        public int? IdDirector { get; set; }
        public virtual Usuario? Director { get; set; }

        [ForeignKey(nameof(Tesista))]
        public int? IdTesista { get; set; }
        public virtual Usuario? Tesista { get; set; }

        [MaxLength(200)]
        public string? DirectoresNombres { get; set; }

        [MaxLength(200)]
        public string? EstudiantesNombres { get; set; }

        [MaxLength(100)]
        public string? Uso { get; set; }

        public bool PlantasMadresConfirmadas { get; set; } = false;

        public bool Activo { get; set; } = true;

        // Navegación
        public virtual List<PlantaMadre>? PlantasMadres { get; set; }
        public virtual List<LoteCultivo>? LotesCultivos { get; set; }
        public virtual List<EntradaDiario>? EntradasDiario { get; set; }
    }
}
