using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReactivoProveedorInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProveedorComprador",
                table: "Reactivos",
                newName: "ProveedorSucursal");

            migrationBuilder.RenameColumn(
                name: "FechaIntroduccion",
                table: "PlantasMadre",
                newName: "FechaRecepcion");

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Reactivos",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProveedorNombre",
                table: "Reactivos",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProveedorTelefono",
                table: "Reactivos",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Reactivos");

            migrationBuilder.DropColumn(
                name: "ProveedorNombre",
                table: "Reactivos");

            migrationBuilder.DropColumn(
                name: "ProveedorTelefono",
                table: "Reactivos");

            migrationBuilder.RenameColumn(
                name: "ProveedorSucursal",
                table: "Reactivos",
                newName: "ProveedorComprador");

            migrationBuilder.RenameColumn(
                name: "FechaRecepcion",
                table: "PlantasMadre",
                newName: "FechaIntroduccion");
        }
    }
}
