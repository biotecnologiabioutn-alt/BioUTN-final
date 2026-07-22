using Microsoft.EntityFrameworkCore;
using BioUTN.Modelos;
using System.Linq;

namespace BioUTN.API.Data
{
    public class BioUTNContext : DbContext
    {
        public BioUTNContext(DbContextOptions<BioUTNContext> options) : base(options)
        {
        }

        // 1. ADMINISTRACIÓN Y SEGURIDAD
        public DbSet<Rol> Roles { get; set; }
        public DbSet<TipoIdentificacion> TiposIdentificacion { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        // 2. BIOLOGÍA: TAXONOMÍA Y ESPECIES
        public DbSet<TipoPlanta> TiposPlanta { get; set; }
        public DbSet<Taxonomia> Taxonomia { get; set; }
        public DbSet<Especie> Especies { get; set; }

        // 3. PROYECTOS Y GESTIÓN OPERATIVA
        public DbSet<TipoProyecto> TiposProyecto { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<TareaOperativa> TareasOperativas { get; set; }

        // 4. TRAZABILIDAD: PLANTAS, LOTES Y FRASCOS
        public DbSet<PlantaMadre> PlantasMadre { get; set; }
        public DbSet<PermisoAmbiental> PermisosAmbientales { get; set; }
        public DbSet<MedioCultivo> MediosCultivo { get; set; }
        public DbSet<FaseCultivo> FasesCultivo { get; set; }
        public DbSet<UbicacionFisica> UbicacionesFisicas { get; set; }
        public DbSet<LoteCultivo> LotesCultivo { get; set; }
        public DbSet<UnidadFrasco> UnidadesFrasco { get; set; }
        public DbSet<Enraizamiento> Enraizamientos { get; set; }
        public DbSet<Aclimatacion> Aclimataciones { get; set; }

        // 5. MONITOREO Y EVIDENCIA CIENTÍFICA
        public DbSet<MonitoreoFitosanitario> MonitoreosFitosanitarios { get; set; }
        public DbSet<EntradaDiario> EntradasDiario { get; set; }
        public DbSet<Protocolo> Protocolos { get; set; }
        public DbSet<CategoriaDocumento> CategoriasDocumento { get; set; }
        public DbSet<Documento> Documentos { get; set; }

        // 6. EQUIPOS, RESERVAS E INVENTARIO
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<BloqueDisponibilidad> BloquesDisponibilidad { get; set; }
        public DbSet<ReservaEquipo> ReservasEquipo { get; set; }
        public DbSet<UnidadMedida> UnidadesMedida { get; set; }
        public DbSet<Reactivo> Reactivos { get; set; }
        public DbSet<KardexMovimiento> KardexMovimientos { get; set; }
        public DbSet<SolicitudLab> SolicitudesLab { get; set; }
        public DbSet<SolicitudDetalleMaterial> SolicitudesDetalleMaterial { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Desactivar Cascade Delete en todas las relaciones para evitar "Multiple Cascade Paths"
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
