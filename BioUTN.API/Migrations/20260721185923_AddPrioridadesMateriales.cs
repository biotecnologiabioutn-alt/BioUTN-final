using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioUTN.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPrioridadesMateriales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaImplementos",
                table: "SolicitudesLab",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaReactivos",
                table: "SolicitudesLab",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoraImplementos",
                table: "SolicitudesLab",
                type: "character varying(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoraReactivos",
                table: "SolicitudesLab",
                type: "character varying(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrioridadImplementos",
                table: "SolicitudesLab",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrioridadReactivos",
                table: "SolicitudesLab",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaImplementos",
                table: "SolicitudesLab");

            migrationBuilder.DropColumn(
                name: "FechaReactivos",
                table: "SolicitudesLab");

            migrationBuilder.DropColumn(
                name: "HoraImplementos",
                table: "SolicitudesLab");

            migrationBuilder.DropColumn(
                name: "HoraReactivos",
                table: "SolicitudesLab");

            migrationBuilder.DropColumn(
                name: "PrioridadImplementos",
                table: "SolicitudesLab");

            migrationBuilder.DropColumn(
                name: "PrioridadReactivos",
                table: "SolicitudesLab");
        }
    }
}
