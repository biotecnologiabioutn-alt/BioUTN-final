using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposInVitroEspecie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PeligroExtincion",
                table: "Especies",
                newName: "EsNativa");

            migrationBuilder.AddColumn<string>(
                name: "CategoriaUso",
                table: "Especies",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CicloVida",
                table: "Especies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DistribucionNatural",
                table: "Especies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EsEndemica",
                table: "Especies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EstadoConservacion",
                table: "Especies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImportanciaEspecie",
                table: "Especies",
                type: "character varying(1500)",
                maxLength: 1500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SinonimosTaxonomicos",
                table: "Especies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriaUso",
                table: "Especies");

            migrationBuilder.DropColumn(
                name: "CicloVida",
                table: "Especies");

            migrationBuilder.DropColumn(
                name: "DistribucionNatural",
                table: "Especies");

            migrationBuilder.DropColumn(
                name: "EsEndemica",
                table: "Especies");

            migrationBuilder.DropColumn(
                name: "EstadoConservacion",
                table: "Especies");

            migrationBuilder.DropColumn(
                name: "ImportanciaEspecie",
                table: "Especies");

            migrationBuilder.DropColumn(
                name: "SinonimosTaxonomicos",
                table: "Especies");

            migrationBuilder.RenameColumn(
                name: "EsNativa",
                table: "Especies",
                newName: "PeligroExtincion");
        }
    }
}
