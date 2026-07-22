using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDirectoresNombres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DirectoresNombres",
                table: "Proyectos",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProtocolo",
                table: "PlantasMadre",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoExplante",
                table: "PlantasMadre",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PlantasMadre_IdProtocolo",
                table: "PlantasMadre",
                column: "IdProtocolo");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantasMadre_Protocolos_IdProtocolo",
                table: "PlantasMadre",
                column: "IdProtocolo",
                principalTable: "Protocolos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantasMadre_Protocolos_IdProtocolo",
                table: "PlantasMadre");

            migrationBuilder.DropIndex(
                name: "IX_PlantasMadre_IdProtocolo",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "DirectoresNombres",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "IdProtocolo",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "TipoExplante",
                table: "PlantasMadre");
        }
    }
}
