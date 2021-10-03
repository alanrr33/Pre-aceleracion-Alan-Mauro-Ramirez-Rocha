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
    public class GeneroRepository: IGeneroRepository,IDisposable
    {
        private ApplicationDbContext context;

        public GeneroRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        //async

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

        public async Task<Genero[]> GetAllGenerosAsync()
        {
            IQueryable<Genero> query = context.Generos;
            // Ordenar
            query = query.OrderByDescending(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Genero[]> GetAllGenerosNameAsync(string name,bool ordenAsc = true)
        {
            // hacer query
            IQueryable<Genero> query = context.Generos;
            query = query.Where(c => c.Nombre.Contains(name));

            if (ordenAsc != true)
            {
                query = query.OrderByDescending(c => c.Nombre);
            }

            return await query.ToArrayAsync();
        }

        public async Task<Genero> GetGeneroIdAsync(int generoId)
        {
            // hacer query
            IQueryable<Genero> query = context.Generos;
            query = query.Where(c => c.Id == generoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Genero> GetGeneroNameAsync(string name)
        {
            // hacer query
            IQueryable<Genero> query = context.Generos;
            query = query.Where(c => c.Nombre == name);

            return await query.FirstOrDefaultAsync();
        }


        //sync
        public IEnumerable<Genero> GetGeneros()
        {
            return context.Generos.ToList();
        }

        public Genero GetGeneroById(int id)
        {
            return context.Generos.Find(id);
        }

        public void AddGenero(Genero genero)
        {
            context.Generos.Add(genero);
        }

        public void DeleteGenero(int id)
        {
            Genero genero = context.Generos.Find(id);
            context.Generos.Remove(genero);
        }

        public void UpdateGenero(Genero genero)
        {
            context.Entry(genero).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
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
