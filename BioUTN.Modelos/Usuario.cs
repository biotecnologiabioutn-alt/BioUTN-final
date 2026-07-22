using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioUTN.Modelos
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(Rol))]
        public int IdRol { get; set; }
        public virtual Rol? Rol { get; set; }

        [Required, ForeignKey(nameof(TipoIdentificacion))]
        public int IdTipoIdentificacion { get; set; }
        public virtual TipoIdentificacion? TipoIdentificacion { get; set; }

        [Required]
        [MaxLength(20)]
        public string NumeroIdentificacion { get; set; } = string.Empty;

        [Required, ForeignKey(nameof(Genero))]
        public int IdGenero { get; set; }
        public virtual Genero? Genero { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? Telefono { get; set; }

        [Required]
        public string ContrasenaHash { get; set; } = string.Empty;

        public int IntentosFallidos { get; set; } = 0;

        public bool CuentaBloqueada { get; set; } = false;

        public bool Activo { get; set; } = true;

        [MaxLength(255)]
        public string? ResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        [NotMapped]
        public string NombreCompletoConRol 
        { 
            get 
            {
                return $"{Nombres} {Apellidos} ({(Rol?.NombreRol ?? "Sin Rol")})";
            }
        }
    }
}
