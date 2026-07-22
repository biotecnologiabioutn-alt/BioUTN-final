using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelosInVitro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodigoUnidad",
                table: "UnidadesFrasco",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaColecta",
                table: "PlantasMadre",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HerbarioReferencia",
                table: "PlantasMadre",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdDocumentoPermiso",
                table: "PlantasMadre",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdResponsableColecta",
                table: "PlantasMadre",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "PlantasMadre",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Procedencia",
                table: "PlantasMadre",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Muerte",
                table: "MonitoreosFitosanitarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiereResiembra",
                table: "MonitoreosFitosanitarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Respuesta",
                table: "MonitoreosFitosanitarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ExplantesPorUnidad",
                table: "LotesCultivo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdDocumentoProtocolo",
                table: "LotesCultivo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoExplante",
                table: "LotesCultivo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SinonimosTaxonomicos",
                table: "Especies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ImportanciaEspecie",
                table: "Especies",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1500)",
                oldMaxLength: 1500);

            migrationBuilder.CreateIndex(
                name: "IX_PlantasMadre_IdDocumentoPermiso",
                table: "PlantasMadre",
                column: "IdDocumentoPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_PlantasMadre_IdResponsableColecta",
                table: "PlantasMadre",
                column: "IdResponsableColecta");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_IdDocumentoProtocolo",
                table: "LotesCultivo",
                column: "IdDocumentoProtocolo");

            migrationBuilder.AddForeignKey(
                name: "FK_LotesCultivo_Documentos_IdDocumentoProtocolo",
                table: "LotesCultivo",
                column: "IdDocumentoProtocolo",
                principalTable: "Documentos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantasMadre_Documentos_IdDocumentoPermiso",
                table: "PlantasMadre",
                column: "IdDocumentoPermiso",
                principalTable: "Documentos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantasMadre_Usuarios_IdResponsableColecta",
                table: "PlantasMadre",
                column: "IdResponsableColecta",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotesCultivo_Documentos_IdDocumentoProtocolo",
                table: "LotesCultivo");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantasMadre_Documentos_IdDocumentoPermiso",
                table: "PlantasMadre");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantasMadre_Usuarios_IdResponsableColecta",
                table: "PlantasMadre");

            migrationBuilder.DropIndex(
                name: "IX_PlantasMadre_IdDocumentoPermiso",
                table: "PlantasMadre");

            migrationBuilder.DropIndex(
                name: "IX_PlantasMadre_IdResponsableColecta",
                table: "PlantasMadre");

            migrationBuilder.DropIndex(
                name: "IX_LotesCultivo_IdDocumentoProtocolo",
                table: "LotesCultivo");

            migrationBuilder.DropColumn(
                name: "FechaColecta",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "HerbarioReferencia",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "IdDocumentoPermiso",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "IdResponsableColecta",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "Procedencia",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "Muerte",
                table: "MonitoreosFitosanitarios");

            migrationBuilder.DropColumn(
                name: "RequiereResiembra",
                table: "MonitoreosFitosanitarios");

            migrationBuilder.DropColumn(
                name: "Respuesta",
                table: "MonitoreosFitosanitarios");

            migrationBuilder.DropColumn(
                name: "ExplantesPorUnidad",
                table: "LotesCultivo");

            migrationBuilder.DropColumn(
                name: "IdDocumentoProtocolo",
                table: "LotesCultivo");

            migrationBuilder.DropColumn(
                name: "TipoExplante",
                table: "LotesCultivo");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoUnidad",
                table: "UnidadesFrasco",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "SinonimosTaxonomicos",
                table: "Especies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImportanciaEspecie",
                table: "Especies",
                type: "character varying(1500)",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);
        }
    }
}
