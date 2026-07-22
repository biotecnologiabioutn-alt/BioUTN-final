using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriasDocumento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE \"Documentos\" CASCADE;");

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Documentos");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Documentos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoriasDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SoloParaTesistas = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasDocumento", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_CategoriaId",
                table: "Documentos",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_CategoriasDocumento_CategoriaId",
                table: "Documentos",
                column: "CategoriaId",
                principalTable: "CategoriasDocumento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_CategoriasDocumento_CategoriaId",
                table: "Documentos");

            migrationBuilder.DropTable(
                name: "CategoriasDocumento");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_CategoriaId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Documentos");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Documentos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
