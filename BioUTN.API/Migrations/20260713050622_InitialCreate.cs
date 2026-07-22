using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoInventario = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Marca = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Modelo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FechaProximoMantenimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Familias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreFamilia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Familias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FasesCultivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreFase = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FasesCultivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreGenero = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediosCultivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Siglas = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediosCultivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreProyecto = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    TipoProyecto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reactivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StockActual = table.Column<decimal>(type: "numeric", nullable: false),
                    UnidadMedida = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    StockMinimo = table.Column<decimal>(type: "numeric", nullable: false),
                    FechaCaducidad = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProveedorComprador = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreRol = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposCultivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreTipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposCultivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposIdentificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreTipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposIdentificacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposPlanta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPlanta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UbicacionesFisicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreCuerpo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NumeroPiso = table.Column<int>(type: "integer", nullable: false),
                    EstadoUbicacion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionesFisicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombres = table.Column<string>(type: "text", nullable: false),
                    Apellidos = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ContrasenaHash = table.Column<string>(type: "text", nullable: false),
                    IntentosFallidos = table.Column<int>(type: "integer", nullable: false),
                    CuentaBloqueada = table.Column<bool>(type: "boolean", nullable: true),
                    IdRol = table.Column<int>(type: "integer", nullable: false),
                    IdTipoIdentificacion = table.Column<int>(type: "integer", nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "text", nullable: false),
                    IdGenero = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Generos_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_TiposIdentificacion_IdTipoIdentificacion",
                        column: x => x.IdTipoIdentificacion,
                        principalTable: "TiposIdentificacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Especies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoEstricto = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    NombreCientifico = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NombreComun = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PeligroExtincion = table.Column<bool>(type: "boolean", nullable: false),
                    IdTipoPlanta = table.Column<int>(type: "integer", nullable: false),
                    IdFamilia = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Especies_Familias_IdFamilia",
                        column: x => x.IdFamilia,
                        principalTable: "Familias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Especies_TiposPlanta_IdTipoPlanta",
                        column: x => x.IdTipoPlanta,
                        principalTable: "TiposPlanta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProyectosUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    RolEnProyecto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectosUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyectosUsuarios_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProyectosUsuarios_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservasEquipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEquipo = table.Column<int>(type: "integer", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservasEquipo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservasEquipo_Equipos_IdEquipo",
                        column: x => x.IdEquipo,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservasEquipo_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TareasOperativas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    TipoTarea = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaCompletada = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasOperativas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TareasOperativas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantasMadre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEspecie = table.Column<int>(type: "integer", nullable: false),
                    CodigoAsignado = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    FechaIntroduccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UrlFotografia = table.Column<string>(type: "text", nullable: false),
                    EstadoFitosanitario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantasMadre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantasMadre_Especies_IdEspecie",
                        column: x => x.IdEspecie,
                        principalTable: "Especies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantasMadre_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantasMadre_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LotesCultivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPlantaMadre = table.Column<int>(type: "integer", nullable: false),
                    IdLotePadre = table.Column<int>(type: "integer", nullable: true),
                    CodigoTrazabilidad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NumeroRepique = table.Column<int>(type: "integer", nullable: false),
                    FechaSiembra = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdMedioCultivo = table.Column<int>(type: "integer", nullable: false),
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    IdUbicacionFisica = table.Column<int>(type: "integer", nullable: false),
                    IdFaseCultivo = table.Column<int>(type: "integer", nullable: false),
                    IdTipoCultivo = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotesCultivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotesCultivo_FasesCultivo_IdFaseCultivo",
                        column: x => x.IdFaseCultivo,
                        principalTable: "FasesCultivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LotesCultivo_LotesCultivo_IdLotePadre",
                        column: x => x.IdLotePadre,
                        principalTable: "LotesCultivo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LotesCultivo_MediosCultivo_IdMedioCultivo",
                        column: x => x.IdMedioCultivo,
                        principalTable: "MediosCultivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LotesCultivo_PlantasMadre_IdPlantaMadre",
                        column: x => x.IdPlantaMadre,
                        principalTable: "PlantasMadre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LotesCultivo_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LotesCultivo_TiposCultivo_IdTipoCultivo",
                        column: x => x.IdTipoCultivo,
                        principalTable: "TiposCultivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LotesCultivo_UbicacionesFisicas_IdUbicacionFisica",
                        column: x => x.IdUbicacionFisica,
                        principalTable: "UbicacionesFisicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LotesCultivo_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PermisosAmbientales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPlantaMadre = table.Column<int>(type: "integer", nullable: false),
                    NumeroResolucion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UrlArchivoPdf = table.Column<string>(type: "text", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermisosAmbientales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermisosAmbientales_PlantasMadre_IdPlantaMadre",
                        column: x => x.IdPlantaMadre,
                        principalTable: "PlantasMadre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Categoria = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UrlArchivo = table.Column<string>(type: "text", nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdLoteCultivo = table.Column<int>(type: "integer", nullable: true),
                    IdProyecto = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentos_LotesCultivo_IdLoteCultivo",
                        column: x => x.IdLoteCultivo,
                        principalTable: "LotesCultivo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documentos_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documentos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MonitoreosFitosanitarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdLoteCultivo = table.Column<int>(type: "integer", nullable: false),
                    FechaEvaluacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NivelFenolizacion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NivelContaminacion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoreosFitosanitarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitoreosFitosanitarios_LotesCultivo_IdLoteCultivo",
                        column: x => x.IdLoteCultivo,
                        principalTable: "LotesCultivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonitoreosFitosanitarios_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_IdLoteCultivo",
                table: "Documentos",
                column: "IdLoteCultivo");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_IdProyecto",
                table: "Documentos",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_IdUsuario",
                table: "Documentos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Especies_IdFamilia",
                table: "Especies",
                column: "IdFamilia");

            migrationBuilder.CreateIndex(
                name: "IX_Especies_IdTipoPlanta",
                table: "Especies",
                column: "IdTipoPlanta");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_IdFaseCultivo",
                table: "LotesCultivo",
                column: "IdFaseCultivo");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_IdLotePadre",
                table: "LotesCultivo",
                column: "IdLotePadre");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_IdMedioCultivo",
                table: "LotesCultivo",
                column: "IdMedioCultivo");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_IdPlantaMadre",
                table: "LotesCultivo",
                column: "IdPlantaMadre");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_IdProyecto",
                table: "LotesCultivo",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_IdTipoCultivo",
                table: "LotesCultivo",
                column: "IdTipoCultivo");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_IdUbicacionFisica",
                table: "LotesCultivo",
                column: "IdUbicacionFisica");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_UsuarioId",
                table: "LotesCultivo",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoreosFitosanitarios_IdLoteCultivo",
                table: "MonitoreosFitosanitarios",
                column: "IdLoteCultivo");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoreosFitosanitarios_IdUsuario",
                table: "MonitoreosFitosanitarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosAmbientales_IdPlantaMadre",
                table: "PermisosAmbientales",
                column: "IdPlantaMadre");

            migrationBuilder.CreateIndex(
                name: "IX_PlantasMadre_IdEspecie",
                table: "PlantasMadre",
                column: "IdEspecie");

            migrationBuilder.CreateIndex(
                name: "IX_PlantasMadre_IdProyecto",
                table: "PlantasMadre",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_PlantasMadre_UsuarioId",
                table: "PlantasMadre",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectosUsuarios_IdProyecto",
                table: "ProyectosUsuarios",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectosUsuarios_IdUsuario",
                table: "ProyectosUsuarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasEquipo_IdEquipo",
                table: "ReservasEquipo",
                column: "IdEquipo");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasEquipo_IdUsuario",
                table: "ReservasEquipo",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_TareasOperativas_IdUsuario",
                table: "TareasOperativas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdGenero",
                table: "Usuarios",
                column: "IdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdTipoIdentificacion",
                table: "Usuarios",
                column: "IdTipoIdentificacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "MonitoreosFitosanitarios");

            migrationBuilder.DropTable(
                name: "PermisosAmbientales");

            migrationBuilder.DropTable(
                name: "ProyectosUsuarios");

            migrationBuilder.DropTable(
                name: "Reactivos");

            migrationBuilder.DropTable(
                name: "ReservasEquipo");

            migrationBuilder.DropTable(
                name: "TareasOperativas");

            migrationBuilder.DropTable(
                name: "LotesCultivo");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "FasesCultivo");

            migrationBuilder.DropTable(
                name: "MediosCultivo");

            migrationBuilder.DropTable(
                name: "PlantasMadre");

            migrationBuilder.DropTable(
                name: "TiposCultivo");

            migrationBuilder.DropTable(
                name: "UbicacionesFisicas");

            migrationBuilder.DropTable(
                name: "Especies");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Familias");

            migrationBuilder.DropTable(
                name: "TiposPlanta");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TiposIdentificacion");
        }
    }
}
