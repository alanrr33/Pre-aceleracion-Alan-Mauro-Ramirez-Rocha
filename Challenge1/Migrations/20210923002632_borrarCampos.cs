using Microsoft.EntityFrameworkCore.Migrations;

namespace Challenge1.Migrations
{
    public partial class borrarCampos : Migration
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
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Personajes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "PeliculasSeries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Generos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
