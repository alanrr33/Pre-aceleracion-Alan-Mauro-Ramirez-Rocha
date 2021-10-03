using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Challenge1.Migrations
{
    public partial class VolverAtrasImagen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Personajes");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "PeliculasSeries");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Generos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Imagen",
                table: "Personajes",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagen",
                table: "PeliculasSeries",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagen",
                table: "Generos",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
