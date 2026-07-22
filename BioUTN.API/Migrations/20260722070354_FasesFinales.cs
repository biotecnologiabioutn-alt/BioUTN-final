using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class FasesFinales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enraizamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdLoteOrigen = table.Column<int>(type: "integer", nullable: false),
                    IdUsuarioResponsable = table.Column<int>(type: "integer", nullable: false),
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumeroPlantas = table.Column<int>(type: "integer", nullable: false),
                    SemanasEstimadas = table.Column<int>(type: "integer", nullable: false),
                    IdDocumentoProtocolo = table.Column<int>(type: "integer", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enraizamientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enraizamientos_Documentos_IdDocumentoProtocolo",
                        column: x => x.IdDocumentoProtocolo,
                        principalTable: "Documentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Enraizamientos_LotesCultivo_IdLoteOrigen",
                        column: x => x.IdLoteOrigen,
                        principalTable: "LotesCultivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enraizamientos_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enraizamientos_Usuarios_IdUsuarioResponsable",
                        column: x => x.IdUsuarioResponsable,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aclimataciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdLoteEnraizado = table.Column<int>(type: "integer", nullable: false),
                    IdUsuarioResponsable = table.Column<int>(type: "integer", nullable: false),
                    FechaTraspaso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SustratoUtilizado = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    LavadoRaices = table.Column<bool>(type: "boolean", nullable: false),
                    CoberturaPlastica = table.Column<bool>(type: "boolean", nullable: false),
                    IdUbicacionFisica = table.Column<int>(type: "integer", nullable: false),
                    IdDocumentoProtocolo = table.Column<int>(type: "integer", nullable: true),
                    HumedadInicial = table.Column<decimal>(type: "numeric", nullable: true),
                    TemperaturaControlada = table.Column<decimal>(type: "numeric", nullable: true),
                    FechaEvaluacionFinal = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PlantasSobrevivientes = table.Column<int>(type: "integer", nullable: true),
                    PlantasMuertas = table.Column<int>(type: "integer", nullable: true),
                    Observaciones = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    FotografiaUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aclimataciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aclimataciones_Documentos_IdDocumentoProtocolo",
                        column: x => x.IdDocumentoProtocolo,
                        principalTable: "Documentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Aclimataciones_Enraizamientos_IdLoteEnraizado",
                        column: x => x.IdLoteEnraizado,
                        principalTable: "Enraizamientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aclimataciones_UbicacionesFisicas_IdUbicacionFisica",
                        column: x => x.IdUbicacionFisica,
                        principalTable: "UbicacionesFisicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aclimataciones_Usuarios_IdUsuarioResponsable",
                        column: x => x.IdUsuarioResponsable,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aclimataciones_IdDocumentoProtocolo",
                table: "Aclimataciones",
                column: "IdDocumentoProtocolo");

            migrationBuilder.CreateIndex(
                name: "IX_Aclimataciones_IdLoteEnraizado",
                table: "Aclimataciones",
                column: "IdLoteEnraizado");

            migrationBuilder.CreateIndex(
                name: "IX_Aclimataciones_IdUbicacionFisica",
                table: "Aclimataciones",
                column: "IdUbicacionFisica");

            migrationBuilder.CreateIndex(
                name: "IX_Aclimataciones_IdUsuarioResponsable",
                table: "Aclimataciones",
                column: "IdUsuarioResponsable");

            migrationBuilder.CreateIndex(
                name: "IX_Enraizamientos_IdDocumentoProtocolo",
                table: "Enraizamientos",
                column: "IdDocumentoProtocolo");

            migrationBuilder.CreateIndex(
                name: "IX_Enraizamientos_IdLoteOrigen",
                table: "Enraizamientos",
                column: "IdLoteOrigen");

            migrationBuilder.CreateIndex(
                name: "IX_Enraizamientos_IdProyecto",
                table: "Enraizamientos",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Enraizamientos_IdUsuarioResponsable",
                table: "Enraizamientos",
                column: "IdUsuarioResponsable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aclimataciones");

            migrationBuilder.DropTable(
                name: "Enraizamientos");
        }
    }
}
