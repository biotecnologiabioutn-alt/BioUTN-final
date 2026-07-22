using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentosBiblioteca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermisosAmbientales_PlantasMadre_IdPlantaMadre",
                table: "PermisosAmbientales");

            migrationBuilder.AlterColumn<int>(
                name: "IdPlantaMadre",
                table: "PermisosAmbientales",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PermisosAmbientales_PlantasMadre_IdPlantaMadre",
                table: "PermisosAmbientales",
                column: "IdPlantaMadre",
                principalTable: "PlantasMadre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermisosAmbientales_PlantasMadre_IdPlantaMadre",
                table: "PermisosAmbientales");

            migrationBuilder.AlterColumn<int>(
                name: "IdPlantaMadre",
                table: "PermisosAmbientales",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_PermisosAmbientales_PlantasMadre_IdPlantaMadre",
                table: "PermisosAmbientales",
                column: "IdPlantaMadre",
                principalTable: "PlantasMadre",
                principalColumn: "Id");
        }
    }
}
