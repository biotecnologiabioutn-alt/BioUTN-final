using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSolicitudesLab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservasEquipo_Proyectos_IdProyecto",
                table: "ReservasEquipo");

            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.RenameColumn(
                name: "IdProyecto",
                table: "ReservasEquipo",
                newName: "IdSolicitud");

            migrationBuilder.RenameIndex(
                name: "IX_ReservasEquipo_IdProyecto",
                table: "ReservasEquipo",
                newName: "IX_ReservasEquipo_IdSolicitud");

            migrationBuilder.CreateTable(
                name: "SolicitudesLab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    IdSolicitante = table.Column<int>(type: "integer", nullable: false),
                    IdAprobador = table.Column<int>(type: "integer", nullable: true),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TipoSolicitud = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Prioridad = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MotivoRechazo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaGestion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IdEquipo = table.Column<int>(type: "integer", nullable: true),
                    FechaReservaEquipo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    HoraInicioEquipo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    HoraFinEquipo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesLab", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesLab_Equipos_IdEquipo",
                        column: x => x.IdEquipo,
                        principalTable: "Equipos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SolicitudesLab_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudesLab_Usuarios_IdAprobador",
                        column: x => x.IdAprobador,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SolicitudesLab_Usuarios_IdSolicitante",
                        column: x => x.IdSolicitante,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesDetalleMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdSolicitud = table.Column<int>(type: "integer", nullable: false),
                    IdReactivo = table.Column<int>(type: "integer", nullable: false),
                    CantidadSolicitada = table.Column<decimal>(type: "numeric", nullable: false),
                    Observacion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesDetalleMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesDetalleMaterial_Reactivos_IdReactivo",
                        column: x => x.IdReactivo,
                        principalTable: "Reactivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudesDetalleMaterial_SolicitudesLab_IdSolicitud",
                        column: x => x.IdSolicitud,
                        principalTable: "SolicitudesLab",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesDetalleMaterial_IdReactivo",
                table: "SolicitudesDetalleMaterial",
                column: "IdReactivo");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesDetalleMaterial_IdSolicitud",
                table: "SolicitudesDetalleMaterial",
                column: "IdSolicitud");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesLab_IdAprobador",
                table: "SolicitudesLab",
                column: "IdAprobador");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesLab_IdEquipo",
                table: "SolicitudesLab",
                column: "IdEquipo");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesLab_IdProyecto",
                table: "SolicitudesLab",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesLab_IdSolicitante",
                table: "SolicitudesLab",
                column: "IdSolicitante");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservasEquipo_SolicitudesLab_IdSolicitud",
                table: "ReservasEquipo",
                column: "IdSolicitud",
                principalTable: "SolicitudesLab",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservasEquipo_SolicitudesLab_IdSolicitud",
                table: "ReservasEquipo");

            migrationBuilder.DropTable(
                name: "SolicitudesDetalleMaterial");

            migrationBuilder.DropTable(
                name: "SolicitudesLab");

            migrationBuilder.RenameColumn(
                name: "IdSolicitud",
                table: "ReservasEquipo",
                newName: "IdProyecto");

            migrationBuilder.RenameIndex(
                name: "IX_ReservasEquipo_IdSolicitud",
                table: "ReservasEquipo",
                newName: "IX_ReservasEquipo_IdProyecto");

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProyecto = table.Column<int>(type: "integer", nullable: true),
                    IdReactivo = table.Column<int>(type: "integer", nullable: false),
                    IdSolicitante = table.Column<int>(type: "integer", nullable: false),
                    Cantidad = table.Column<decimal>(type: "numeric", nullable: false),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MotivoRechazo = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TipoSolicitud = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Solicitudes_Reactivos_IdReactivo",
                        column: x => x.IdReactivo,
                        principalTable: "Reactivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Usuarios_IdSolicitante",
                        column: x => x.IdSolicitante,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_IdProyecto",
                table: "Solicitudes",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_IdReactivo",
                table: "Solicitudes",
                column: "IdReactivo");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_IdSolicitante",
                table: "Solicitudes",
                column: "IdSolicitante");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservasEquipo_Proyectos_IdProyecto",
                table: "ReservasEquipo",
                column: "IdProyecto",
                principalTable: "Proyectos",
                principalColumn: "Id");
        }
    }
}
