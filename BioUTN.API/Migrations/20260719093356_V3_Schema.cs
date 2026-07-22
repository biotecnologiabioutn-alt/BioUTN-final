using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class V3_Schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Especies_Familias_IdFamilia",
                table: "Especies");

            migrationBuilder.DropForeignKey(
                name: "FK_LotesCultivo_TiposCultivo_IdTipoCultivo",
                table: "LotesCultivo");

            migrationBuilder.DropForeignKey(
                name: "FK_LotesCultivo_Usuarios_UsuarioId",
                table: "LotesCultivo");

            migrationBuilder.DropForeignKey(
                name: "FK_MonitoreosFitosanitarios_LotesCultivo_IdLoteCultivo",
                table: "MonitoreosFitosanitarios");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantasMadre_Usuarios_UsuarioId",
                table: "PlantasMadre");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Familias");

            migrationBuilder.DropTable(
                name: "ProyectosUsuarios");

            migrationBuilder.DropTable(
                name: "TiposCultivo");

            migrationBuilder.DropIndex(
                name: "IX_PlantasMadre_UsuarioId",
                table: "PlantasMadre");

            migrationBuilder.DropIndex(
                name: "IX_LotesCultivo_IdTipoCultivo",
                table: "LotesCultivo");

            migrationBuilder.DropIndex(
                name: "IX_LotesCultivo_UsuarioId",
                table: "LotesCultivo");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "ReservasEquipo");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "LotesCultivo");

            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "ReservasEquipo",
                newName: "FechaReserva");

            migrationBuilder.RenameColumn(
                name: "IdLoteCultivo",
                table: "MonitoreosFitosanitarios",
                newName: "IdUnidadFrasco");

            migrationBuilder.RenameIndex(
                name: "IX_MonitoreosFitosanitarios_IdLoteCultivo",
                table: "MonitoreosFitosanitarios",
                newName: "IX_MonitoreosFitosanitarios_IdUnidadFrasco");

            migrationBuilder.RenameColumn(
                name: "IdTipoCultivo",
                table: "LotesCultivo",
                newName: "TotalUnidades");

            migrationBuilder.RenameColumn(
                name: "IdFamilia",
                table: "Especies",
                newName: "IdTaxonomia");

            migrationBuilder.RenameIndex(
                name: "IX_Especies_IdFamilia",
                table: "Especies",
                newName: "IX_Especies_IdTaxonomia");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroIdentificacion",
                table: "Usuarios",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Nombres",
                table: "Usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "CuentaBloqueada",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Apellidos",
                table: "Usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Usuarios",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NombreCuerpo",
                table: "UbicacionesFisicas",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "CodigoAnaquel",
                table: "UbicacionesFisicas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraFin",
                table: "ReservasEquipo",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraInicio",
                table: "ReservasEquipo",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "IdBloque",
                table: "ReservasEquipo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservacionesCheckOut",
                table: "ReservasEquipo",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProveedorComprador",
                table: "Reactivos",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCaducidad",
                table: "Reactivos",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Reactivos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioResponsable",
                table: "Proyectos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UrlFotografia",
                table: "PlantasMadre",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoAsignado",
                table: "PlantasMadre",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "PlantasMadre",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "PlantasMadre",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UrlQr",
                table: "PlantasMadre",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Bacterias",
                table: "MonitoreosFitosanitarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Hongos",
                table: "MonitoreosFitosanitarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Siglas",
                table: "MediosCultivo",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "MediosCultivo",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Componentes",
                table: "MediosCultivo",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioPropietario",
                table: "MediosCultivo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "LotesCultivo",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ChecklistSiembra",
                table: "LotesCultivo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "LotesCultivo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UrlQr",
                table: "LotesCultivo",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoEstricto",
                table: "Especies",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldMaxLength: 5);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Especies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<short>(
                name: "DiasResiembra",
                table: "Especies",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "MaxResiembras",
                table: "Especies",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "BloquesDisponibilidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEquipo = table.Column<int>(type: "integer", nullable: false),
                    DiaSemana = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    HoraFin = table.Column<TimeSpan>(type: "interval", nullable: false),
                    TipoReserva = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloquesDisponibilidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloquesDisponibilidad_Equipos_IdEquipo",
                        column: x => x.IdEquipo,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntradasDiario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdLoteCultivo = table.Column<int>(type: "integer", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Contenido = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradasDiario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntradasDiario_LotesCultivo_IdLoteCultivo",
                        column: x => x.IdLoteCultivo,
                        principalTable: "LotesCultivo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntradasDiario_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntradasDiario_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KardexMovimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdReactivo = table.Column<int>(type: "integer", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    TipoMovimiento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Cantidad = table.Column<decimal>(type: "numeric", nullable: false),
                    Motivo = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    FechaMovimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KardexMovimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KardexMovimientos_Reactivos_IdReactivo",
                        column: x => x.IdReactivo,
                        principalTable: "Reactivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KardexMovimientos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Protocolos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEspecie = table.Column<int>(type: "integer", nullable: true),
                    IdUsuarioAutor = table.Column<int>(type: "integer", nullable: false),
                    FaseProtocolo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    UrlArchivoPdf = table.Column<string>(type: "text", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protocolos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Protocolos_Especies_IdEspecie",
                        column: x => x.IdEspecie,
                        principalTable: "Especies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Protocolos_Usuarios_IdUsuarioAutor",
                        column: x => x.IdUsuarioAutor,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Taxonomia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Dominio = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Reino = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubReino = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FiloDivision = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Clase = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Orden = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Familia = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubFamilia = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Tribu = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SubTribu = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Genero = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Especie = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesFrasco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdLoteCultivo = table.Column<int>(type: "integer", nullable: false),
                    CodigoUnidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NumeroResiembra = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    UrlQr = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesFrasco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadesFrasco_LotesCultivo_IdLoteCultivo",
                        column: x => x.IdLoteCultivo,
                        principalTable: "LotesCultivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservasEquipo_IdBloque",
                table: "ReservasEquipo",
                column: "IdBloque");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_IdUsuarioResponsable",
                table: "Proyectos",
                column: "IdUsuarioResponsable");

            migrationBuilder.CreateIndex(
                name: "IX_PlantasMadre_IdUsuario",
                table: "PlantasMadre",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_MediosCultivo_IdUsuarioPropietario",
                table: "MediosCultivo",
                column: "IdUsuarioPropietario");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_IdUsuario",
                table: "LotesCultivo",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_BloquesDisponibilidad_IdEquipo",
                table: "BloquesDisponibilidad",
                column: "IdEquipo");

            migrationBuilder.CreateIndex(
                name: "IX_EntradasDiario_IdLoteCultivo",
                table: "EntradasDiario",
                column: "IdLoteCultivo");

            migrationBuilder.CreateIndex(
                name: "IX_EntradasDiario_IdProyecto",
                table: "EntradasDiario",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_EntradasDiario_IdUsuario",
                table: "EntradasDiario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_KardexMovimientos_IdReactivo",
                table: "KardexMovimientos",
                column: "IdReactivo");

            migrationBuilder.CreateIndex(
                name: "IX_KardexMovimientos_IdUsuario",
                table: "KardexMovimientos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Protocolos_IdEspecie",
                table: "Protocolos",
                column: "IdEspecie");

            migrationBuilder.CreateIndex(
                name: "IX_Protocolos_IdUsuarioAutor",
                table: "Protocolos",
                column: "IdUsuarioAutor");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesFrasco_IdLoteCultivo",
                table: "UnidadesFrasco",
                column: "IdLoteCultivo");

            migrationBuilder.AddForeignKey(
                name: "FK_Especies_Taxonomia_IdTaxonomia",
                table: "Especies",
                column: "IdTaxonomia",
                principalTable: "Taxonomia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LotesCultivo_Usuarios_IdUsuario",
                table: "LotesCultivo",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MediosCultivo_Usuarios_IdUsuarioPropietario",
                table: "MediosCultivo",
                column: "IdUsuarioPropietario",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitoreosFitosanitarios_UnidadesFrasco_IdUnidadFrasco",
                table: "MonitoreosFitosanitarios",
                column: "IdUnidadFrasco",
                principalTable: "UnidadesFrasco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantasMadre_Usuarios_IdUsuario",
                table: "PlantasMadre",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Usuarios_IdUsuarioResponsable",
                table: "Proyectos",
                column: "IdUsuarioResponsable",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservasEquipo_BloquesDisponibilidad_IdBloque",
                table: "ReservasEquipo",
                column: "IdBloque",
                principalTable: "BloquesDisponibilidad",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Especies_Taxonomia_IdTaxonomia",
                table: "Especies");

            migrationBuilder.DropForeignKey(
                name: "FK_LotesCultivo_Usuarios_IdUsuario",
                table: "LotesCultivo");

            migrationBuilder.DropForeignKey(
                name: "FK_MediosCultivo_Usuarios_IdUsuarioPropietario",
                table: "MediosCultivo");

            migrationBuilder.DropForeignKey(
                name: "FK_MonitoreosFitosanitarios_UnidadesFrasco_IdUnidadFrasco",
                table: "MonitoreosFitosanitarios");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantasMadre_Usuarios_IdUsuario",
                table: "PlantasMadre");

            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Usuarios_IdUsuarioResponsable",
                table: "Proyectos");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservasEquipo_BloquesDisponibilidad_IdBloque",
                table: "ReservasEquipo");

            migrationBuilder.DropTable(
                name: "BloquesDisponibilidad");

            migrationBuilder.DropTable(
                name: "EntradasDiario");

            migrationBuilder.DropTable(
                name: "KardexMovimientos");

            migrationBuilder.DropTable(
                name: "Protocolos");

            migrationBuilder.DropTable(
                name: "Taxonomia");

            migrationBuilder.DropTable(
                name: "UnidadesFrasco");

            migrationBuilder.DropIndex(
                name: "IX_ReservasEquipo_IdBloque",
                table: "ReservasEquipo");

            migrationBuilder.DropIndex(
                name: "IX_Proyectos_IdUsuarioResponsable",
                table: "Proyectos");

            migrationBuilder.DropIndex(
                name: "IX_PlantasMadre_IdUsuario",
                table: "PlantasMadre");

            migrationBuilder.DropIndex(
                name: "IX_MediosCultivo_IdUsuarioPropietario",
                table: "MediosCultivo");

            migrationBuilder.DropIndex(
                name: "IX_LotesCultivo_IdUsuario",
                table: "LotesCultivo");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "CodigoAnaquel",
                table: "UbicacionesFisicas");

            migrationBuilder.DropColumn(
                name: "HoraFin",
                table: "ReservasEquipo");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "ReservasEquipo");

            migrationBuilder.DropColumn(
                name: "IdBloque",
                table: "ReservasEquipo");

            migrationBuilder.DropColumn(
                name: "ObservacionesCheckOut",
                table: "ReservasEquipo");

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Reactivos");

            migrationBuilder.DropColumn(
                name: "IdUsuarioResponsable",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "UrlQr",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "Bacterias",
                table: "MonitoreosFitosanitarios");

            migrationBuilder.DropColumn(
                name: "Hongos",
                table: "MonitoreosFitosanitarios");

            migrationBuilder.DropColumn(
                name: "Componentes",
                table: "MediosCultivo");

            migrationBuilder.DropColumn(
                name: "IdUsuarioPropietario",
                table: "MediosCultivo");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "LotesCultivo");

            migrationBuilder.DropColumn(
                name: "ChecklistSiembra",
                table: "LotesCultivo");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "LotesCultivo");

            migrationBuilder.DropColumn(
                name: "UrlQr",
                table: "LotesCultivo");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Especies");

            migrationBuilder.DropColumn(
                name: "DiasResiembra",
                table: "Especies");

            migrationBuilder.DropColumn(
                name: "MaxResiembras",
                table: "Especies");

            migrationBuilder.RenameColumn(
                name: "FechaReserva",
                table: "ReservasEquipo",
                newName: "FechaInicio");

            migrationBuilder.RenameColumn(
                name: "IdUnidadFrasco",
                table: "MonitoreosFitosanitarios",
                newName: "IdLoteCultivo");

            migrationBuilder.RenameIndex(
                name: "IX_MonitoreosFitosanitarios_IdUnidadFrasco",
                table: "MonitoreosFitosanitarios",
                newName: "IX_MonitoreosFitosanitarios_IdLoteCultivo");

            migrationBuilder.RenameColumn(
                name: "TotalUnidades",
                table: "LotesCultivo",
                newName: "IdTipoCultivo");

            migrationBuilder.RenameColumn(
                name: "IdTaxonomia",
                table: "Especies",
                newName: "IdFamilia");

            migrationBuilder.RenameIndex(
                name: "IX_Especies_IdTaxonomia",
                table: "Especies",
                newName: "IX_Especies_IdFamilia");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroIdentificacion",
                table: "Usuarios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Nombres",
                table: "Usuarios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "CuentaBloqueada",
                table: "Usuarios",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "Apellidos",
                table: "Usuarios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "NombreCuerpo",
                table: "UbicacionesFisicas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "ReservasEquipo",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "ProveedorComprador",
                table: "Reactivos",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCaducidad",
                table: "Reactivos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UrlFotografia",
                table: "PlantasMadre",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoAsignado",
                table: "PlantasMadre",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "PlantasMadre",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Siglas",
                table: "MediosCultivo",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "MediosCultivo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "LotesCultivo",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoEstricto",
                table: "Especies",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdLoteCultivo = table.Column<int>(type: "integer", nullable: true),
                    IdProyecto = table.Column<int>(type: "integer", nullable: true),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    Categoria = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    UrlArchivo = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_PlantasMadre_UsuarioId",
                table: "PlantasMadre",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_IdTipoCultivo",
                table: "LotesCultivo",
                column: "IdTipoCultivo");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_UsuarioId",
                table: "LotesCultivo",
                column: "UsuarioId");

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
                name: "IX_ProyectosUsuarios_IdProyecto",
                table: "ProyectosUsuarios",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectosUsuarios_IdUsuario",
                table: "ProyectosUsuarios",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Especies_Familias_IdFamilia",
                table: "Especies",
                column: "IdFamilia",
                principalTable: "Familias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LotesCultivo_TiposCultivo_IdTipoCultivo",
                table: "LotesCultivo",
                column: "IdTipoCultivo",
                principalTable: "TiposCultivo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LotesCultivo_Usuarios_UsuarioId",
                table: "LotesCultivo",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitoreosFitosanitarios_LotesCultivo_IdLoteCultivo",
                table: "MonitoreosFitosanitarios",
                column: "IdLoteCultivo",
                principalTable: "LotesCultivo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantasMadre_Usuarios_UsuarioId",
                table: "PlantasMadre",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
