using Microsoft.EntityFrameworkCore.Migrations;

namespace Challenge1.Migrations
{
    public partial class Correciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasSeries_Generos_GeneroModelId",
                table: "PeliculasSeries");

            migrationBuilder.DropTable(
                name: "PeliculaSerieModelPersonajeModel");

            migrationBuilder.EnsureSchema(
                name: "Personajes");

            migrationBuilder.RenameTable(
                name: "Personajes",
                newName: "Personajes",
                newSchema: "Personajes");

            migrationBuilder.RenameTable(
                name: "PeliculasSeries",
                newName: "PeliculasSeries",
                newSchema: "Personajes");

            migrationBuilder.RenameTable(
                name: "Generos",
                newName: "Generos",
                newSchema: "Personajes");

            migrationBuilder.RenameColumn(
                name: "GeneroModelId",
                schema: "Personajes",
                table: "PeliculasSeries",
                newName: "GeneroId");

            migrationBuilder.RenameIndex(
                name: "IX_PeliculasSeries_GeneroModelId",
                schema: "Personajes",
                table: "PeliculasSeries",
                newName: "IX_PeliculasSeries_GeneroId");

            migrationBuilder.AlterColumn<float>(
                name: "Peso",
                schema: "Personajes",
                table: "Personajes",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "PeliculaSeriePersonaje",
                schema: "Personajes",
                columns: table => new
                {
                    PeliculasSeriesId = table.Column<int>(type: "int", nullable: false),
                    PersonajesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaSeriePersonaje", x => new { x.PeliculasSeriesId, x.PersonajesId });
                    table.ForeignKey(
                        name: "FK_PeliculaSeriePersonaje_PeliculasSeries_PeliculasSeriesId",
                        column: x => x.PeliculasSeriesId,
                        principalSchema: "Personajes",
                        principalTable: "PeliculasSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaSeriePersonaje_Personajes_PersonajesId",
                        column: x => x.PersonajesId,
                        principalSchema: "Personajes",
                        principalTable: "Personajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaSeriePersonaje_PersonajesId",
                schema: "Personajes",
                table: "PeliculaSeriePersonaje",
                column: "PersonajesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasSeries_Generos_GeneroId",
                schema: "Personajes",
                table: "PeliculasSeries",
                column: "GeneroId",
                principalSchema: "Personajes",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasSeries_Generos_GeneroId",
                schema: "Personajes",
                table: "PeliculasSeries");

            migrationBuilder.DropTable(
                name: "PeliculaSeriePersonaje",
                schema: "Personajes");

            migrationBuilder.RenameTable(
                name: "Personajes",
                schema: "Personajes",
                newName: "Personajes");

            migrationBuilder.RenameTable(
                name: "PeliculasSeries",
                schema: "Personajes",
                newName: "PeliculasSeries");

            migrationBuilder.RenameTable(
                name: "Generos",
                schema: "Personajes",
                newName: "Generos");

            migrationBuilder.RenameColumn(
                name: "GeneroId",
                table: "PeliculasSeries",
                newName: "GeneroModelId");

            migrationBuilder.RenameIndex(
                name: "IX_PeliculasSeries_GeneroId",
                table: "PeliculasSeries",
                newName: "IX_PeliculasSeries_GeneroModelId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Peso",
                table: "Personajes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasSeries_Generos_GeneroModelId",
                table: "PeliculasSeries",
                column: "GeneroModelId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
