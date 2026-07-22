using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSolicitudesYReservasProyectos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdProyecto",
                table: "ReservasEquipo",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdReactivo = table.Column<int>(type: "integer", nullable: false),
                    IdSolicitante = table.Column<int>(type: "integer", nullable: false),
                    IdProyecto = table.Column<int>(type: "integer", nullable: true),
                    Cantidad = table.Column<decimal>(type: "numeric", nullable: false),
                    TipoSolicitud = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MotivoRechazo = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
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
                name: "IX_ReservasEquipo_IdProyecto",
                table: "ReservasEquipo",
                column: "IdProyecto");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservasEquipo_Proyectos_IdProyecto",
                table: "ReservasEquipo");

            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_ReservasEquipo_IdProyecto",
                table: "ReservasEquipo");

            migrationBuilder.DropColumn(
                name: "IdProyecto",
                table: "ReservasEquipo");
        }
    }
}
