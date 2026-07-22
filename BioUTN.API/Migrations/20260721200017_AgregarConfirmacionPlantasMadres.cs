using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarConfirmacionPlantasMadres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PlantasMadresConfirmadas",
                table: "Proyectos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlantasMadresConfirmadas",
                table: "Proyectos");
        }
    }
}
