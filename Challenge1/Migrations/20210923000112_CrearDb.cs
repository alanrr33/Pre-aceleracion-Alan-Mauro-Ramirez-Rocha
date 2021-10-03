using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Challenge1.Migrations
{
    public partial class CrearDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Historia = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personajes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeliculasSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Calificacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GeneroModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculasSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeliculasSeries_Generos_GeneroModelId",
                        column: x => x.GeneroModelId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeliculaSerieModelPersonajeModel",
                columns: table => new
                {
                    PeliculasSeriesId = table.Column<int>(type: "int", nullable: false),
                    PersonajesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaSerieModelPersonajeModel", x => new { x.PeliculasSeriesId, x.PersonajesId });
                    table.ForeignKey(
                        name: "FK_PeliculaSerieModelPersonajeModel_PeliculasSeries_PeliculasSeriesId",
                        column: x => x.PeliculasSeriesId,
                        principalTable: "PeliculasSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaSerieModelPersonajeModel_Personajes_PersonajesId",
                        column: x => x.PersonajesId,
                        principalTable: "Personajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaSerieModelPersonajeModel_PersonajesId",
                table: "PeliculaSerieModelPersonajeModel",
                column: "PersonajesId");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSeries_GeneroModelId",
                table: "PeliculasSeries",
                column: "GeneroModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculaSerieModelPersonajeModel");

            migrationBuilder.DropTable(
                name: "PeliculasSeries");

            migrationBuilder.DropTable(
                name: "Personajes");

            migrationBuilder.DropTable(
                name: "Generos");
        }
    }
}
