using Challenge1.Entities;
using Microsoft.EntityFrameworkCore;

namespace Challenge1.Data
{
    public class ApplicationDbContext: DbContext
    {
        public const string Schema = "Personajes";
        //config
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(Schema);

        }

        public DbSet<Personaje> Personajes { get; set; }
        public DbSet<PeliculaSerie> PeliculasSeries { get; set; }

        public DbSet<Genero> Generos { get; set; }



    }
}
