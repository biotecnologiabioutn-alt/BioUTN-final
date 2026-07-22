using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class FixPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Proyectos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FrecuenciaMonitoreoDias",
                table: "LotesCultivo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ResponsablesSiembraNombres",
                table: "LotesCultivo",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "FrecuenciaMonitoreoDias",
                table: "LotesCultivo");

            migrationBuilder.DropColumn(
                name: "ResponsablesSiembraNombres",
                table: "LotesCultivo");
        }
    }
}
