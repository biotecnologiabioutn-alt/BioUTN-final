using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFichaFieldsToProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EstudiantesNombres",
                table: "Proyectos",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdDirector",
                table: "Proyectos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTesista",
                table: "Proyectos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uso",
                table: "Proyectos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_IdDirector",
                table: "Proyectos",
                column: "IdDirector");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_IdTesista",
                table: "Proyectos",
                column: "IdTesista");

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Usuarios_IdDirector",
                table: "Proyectos",
                column: "IdDirector",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Usuarios_IdTesista",
                table: "Proyectos",
                column: "IdTesista",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Usuarios_IdDirector",
                table: "Proyectos");

            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Usuarios_IdTesista",
                table: "Proyectos");

            migrationBuilder.DropIndex(
                name: "IX_Proyectos_IdDirector",
                table: "Proyectos");

            migrationBuilder.DropIndex(
                name: "IX_Proyectos_IdTesista",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "EstudiantesNombres",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "IdDirector",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "IdTesista",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "Uso",
                table: "Proyectos");
        }
    }
}
