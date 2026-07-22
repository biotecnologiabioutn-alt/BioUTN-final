using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUnidadesMedida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnidadMedida",
                table: "Reactivos");

            migrationBuilder.AddColumn<int>(
                name: "IdUnidadMedida",
                table: "Reactivos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProveedorContacto",
                table: "Reactivos",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProveedorEmail",
                table: "Reactivos",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProveedorRUC",
                table: "Reactivos",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnidadesMedida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Abreviatura = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    UnidadBase = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FactorConversion = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesMedida", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reactivos_IdUnidadMedida",
                table: "Reactivos",
                column: "IdUnidadMedida");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactivos_UnidadesMedida_IdUnidadMedida",
                table: "Reactivos",
                column: "IdUnidadMedida",
                principalTable: "UnidadesMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactivos_UnidadesMedida_IdUnidadMedida",
                table: "Reactivos");

            migrationBuilder.DropTable(
                name: "UnidadesMedida");

            migrationBuilder.DropIndex(
                name: "IX_Reactivos_IdUnidadMedida",
                table: "Reactivos");

            migrationBuilder.DropColumn(
                name: "IdUnidadMedida",
                table: "Reactivos");

            migrationBuilder.DropColumn(
                name: "ProveedorContacto",
                table: "Reactivos");

            migrationBuilder.DropColumn(
                name: "ProveedorEmail",
                table: "Reactivos");

            migrationBuilder.DropColumn(
                name: "ProveedorRUC",
                table: "Reactivos");

            migrationBuilder.AddColumn<string>(
                name: "UnidadMedida",
                table: "Reactivos",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
