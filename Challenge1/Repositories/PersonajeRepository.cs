using Challenge1.Data;
using Challenge1.Entities;
using Challenge1.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.Repositories
{
    
    public class PersonajeRepository : IPersonajesRepository, IDisposable
    {
        private ApplicationDbContext context;

        public PersonajeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        // async

        public void Add<T>(T entity) where T : class
        {
            
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
           context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            
            //devuelve verdadero si al menos una fila fue cambiada
            return (await context.SaveChangesAsync()) > 0;
        }




        public async Task<Personaje[]> GetAllPersonajesAsync()
        {
            IQueryable<Personaje> query = context.Personajes;
            // Ordenar
            query = query.OrderByDescending(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Personaje> GetPersonajeNameAsync(string name)
        {
            IQueryable<Personaje> query = context.Personajes;

            query = query.Include(c => c.PeliculasSeries)
                    .ThenInclude((c => c.Genero));
            // hacer query
            query = query.Where(c => c.Nombre == name);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Personaje> GetPersonajeIdAsync(int id)
        {
            IQueryable<Personaje> query = context.Personajes;

            /*query = query.Include(c => c.PeliculasSeries)
                    .ThenInclude((c => c.Genero));*/
            // hacer query
            query = query.Where(c => c.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Personaje[]> GetAllPersonajesByName(string name)
        {


            IQueryable<Personaje> query = context.Personajes;

            //query = query.Include(c => c.PeliculasSeries);

           /* if (!string.IsNullOrEmpty(name) & edad == null & peso == null & peliSerieId == null)
            {*/
            query = query.OrderBy(c => c.Nombre).Include(c => c.PeliculasSeries).Where(c => c.Nombre.Contains(name));
            //}

            /*if (edad != null & edad>0 & string.IsNullOrEmpty(name) & peso == null & peliSerieId ==null)
            {
                query = query.Include(c => c.PeliculasSeries).Where(c => c.Edad == edad);
            }

            if (peso != null)
            {
                  query = query.Include(c => c.PeliculasSeries).Where(c => c.Peso == peso);
            }


            if (peliSerieId != null | peliSerieId>0)
            {
                //buscar en las relaciones externas
                
                query = query.Where(c => c.PeliculasSeries.Any(c =>c.Id==peliSerieId));
                

            }*/


            // Ordenar
            query = query.OrderByDescending(c => c.Id);
              //.Where(c => c.Nombre == name);


            return await query.ToArrayAsync();
        }


        public async Task<Personaje[]> GetAllPersonajesByEdad(int? edad)
        {


            IQueryable<Personaje> query = context.Personajes;
       
            query = query.Include(c => c.PeliculasSeries).Where(c => c.Edad == edad);
            

            // Ordenar
            query = query.OrderByDescending(c => c.Id);
  
            return await query.ToArrayAsync();
        }

        public async Task<Personaje[]> GetAllPersonajesByPeso(float? peso)
        {

            IQueryable<Personaje> query = context.Personajes;
          
            query = query.Include(c => c.PeliculasSeries).Where(c => c.Peso == peso);
            

            // Ordenar
            query = query.OrderByDescending(c => c.Id);

            return await query.ToArrayAsync();
        }


        public async Task<Personaje[]> GetAllPersonajesByPeliSerieId(int? peliSerieId)
        {

            IQueryable<Personaje> query = context.Personajes;

            //buscar en las relaciones externas
                
            query = query.Where(c => c.PeliculasSeries.Any(c =>c.Id==peliSerieId));
                
            // Ordenar
            query = query.OrderByDescending(c => c.Id);


            return await query.ToArrayAsync();
        }



        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
