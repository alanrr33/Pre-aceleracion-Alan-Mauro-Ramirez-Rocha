// <auto-generated />
using System;
using Challenge1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Challenge1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210923002632_borrarCampos")]
    partial class borrarCampos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Challenge1.Models.GeneroModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("Challenge1.Models.PeliculaSerieModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Calificacion")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GeneroModelId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GeneroModelId");

                    b.ToTable("PeliculasSeries");
                });

            modelBuilder.Entity("Challenge1.Models.PersonajeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<string>("Historia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Peso")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Personajes");
                });

            modelBuilder.Entity("PeliculaSerieModelPersonajeModel", b =>
                {
                    b.Property<int>("PeliculasSeriesId")
                        .HasColumnType("int");

                    b.Property<int>("PersonajesId")
                        .HasColumnType("int");

                    b.HasKey("PeliculasSeriesId", "PersonajesId");

                    b.HasIndex("PersonajesId");

                    b.ToTable("PeliculaSerieModelPersonajeModel");
                });

            modelBuilder.Entity("Challenge1.Models.PeliculaSerieModel", b =>
                {
                    b.HasOne("Challenge1.Models.GeneroModel", null)
                        .WithMany("PeliculasSeries")
                        .HasForeignKey("GeneroModelId");
                });

            modelBuilder.Entity("PeliculaSerieModelPersonajeModel", b =>
                {
                    b.HasOne("Challenge1.Models.PeliculaSerieModel", null)
                        .WithMany()
                        .HasForeignKey("PeliculasSeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Challenge1.Models.PersonajeModel", null)
                        .WithMany()
                        .HasForeignKey("PersonajesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Challenge1.Models.GeneroModel", b =>
                {
                    b.Navigation("PeliculasSeries");
                });
#pragma warning restore 612, 618
        }
    }
}
