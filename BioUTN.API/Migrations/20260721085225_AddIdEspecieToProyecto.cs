using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AddIdEspecieToProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEspecie",
                table: "Proyectos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_IdEspecie",
                table: "Proyectos",
                column: "IdEspecie");

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Especies_IdEspecie",
                table: "Proyectos",
                column: "IdEspecie",
                principalTable: "Especies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Especies_IdEspecie",
                table: "Proyectos");

            migrationBuilder.DropIndex(
                name: "IX_Proyectos_IdEspecie",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "IdEspecie",
                table: "Proyectos");
        }
    }
}
