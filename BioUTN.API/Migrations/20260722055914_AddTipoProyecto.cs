using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoProyecto",
                table: "Proyectos");

            migrationBuilder.AddColumn<int>(
                name: "IdTipoProyecto",
                table: "Proyectos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TiposProyecto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposProyecto", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TiposProyecto",
                columns: new[] { "Nombre" },
                values: new object[,]
                {
                    { "Tesis" },
                    { "Investigación" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_IdTipoProyecto",
                table: "Proyectos",
                column: "IdTipoProyecto");

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_TiposProyecto_IdTipoProyecto",
                table: "Proyectos",
                column: "IdTipoProyecto",
                principalTable: "TiposProyecto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_TiposProyecto_IdTipoProyecto",
                table: "Proyectos");

            migrationBuilder.DropTable(
                name: "TiposProyecto");

            migrationBuilder.DropIndex(
                name: "IX_Proyectos_IdTipoProyecto",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "IdTipoProyecto",
                table: "Proyectos");

            migrationBuilder.AddColumn<string>(
                name: "TipoProyecto",
                table: "Proyectos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
