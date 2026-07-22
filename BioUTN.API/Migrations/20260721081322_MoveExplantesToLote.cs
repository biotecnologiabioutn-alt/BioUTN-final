using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class MoveExplantesToLote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoExplante",
                table: "PlantasMadre");

            migrationBuilder.DropColumn(
                name: "TotalExplantesIntroducidos",
                table: "PlantasMadre");

            migrationBuilder.AddColumn<int>(
                name: "TotalExplantesIntroducidos",
                table: "LotesCultivo",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalExplantesIntroducidos",
                table: "LotesCultivo");

            migrationBuilder.AddColumn<string>(
                name: "TipoExplante",
                table: "PlantasMadre",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalExplantesIntroducidos",
                table: "PlantasMadre",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
