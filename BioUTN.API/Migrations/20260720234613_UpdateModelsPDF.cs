using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsPDF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdResponsableIntroduccion",
                table: "PlantasMadre",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalExplantesIntroducidos",
                table: "PlantasMadre",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnidadesRevisadas",
                table: "MonitoreosFitosanitarios",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUnidadFrascoOrigen",
                table: "LotesCultivo",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlantasMadre_IdResponsableIntroduccion",
                table: "PlantasMadre",
                column: "IdResponsableIntroduccion");

            migrationBuilder.CreateIndex(
                name: "IX_LotesCultivo_IdUnidadFrascoOrigen",
                table: "LotesCultivo",
                column: "IdUnidadFrascoOrigen");

            migrationBuilder.AddForeignKey(
                name: "FK_LotesCultivo_UnidadesFrasco_IdUnidadFrascoOrigen",
                table: "LotesCultivo",
                column: "IdUnidadFrascoOrigen",
                principalTable: "UnidadesFrasco",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantasMadre_Usuarios_IdResponsableIntroduccion",
                table: "PlantasMadre",
                column: "IdResponsableIntroduccion",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotesCultivo_UnidadesFrasco_IdUnidadFrascoOrigen",
                table: "LotesCultivo");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantasMadre_Usuarios_IdResponsableIntroduccion",
                table: "PlantasMadre");

            migrationBuilder.DropIndex(
                name: "IX_PlantasMadre_IdResponsableIntroduccion",
                table: "PlantasMadre");

            migrationBuilder.DropIndex(
                name: "IX_LotesCultivo_IdUnidadFrascoOrigen",
                table: "LotesCultivo");

            migrationBuilder.DropColumn(
                name: "IdResponsableIntroduccion",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "TotalExplantesIntroducidos",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "UnidadesRevisadas",
                table: "MonitoreosFitosanitarios");

            migrationBuilder.DropColumn(
                name: "IdUnidadFrascoOrigen",
                table: "LotesCultivo");
        }
    }
}
