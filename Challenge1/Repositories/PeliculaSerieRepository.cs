﻿using Challenge1.Data;
using Challenge1.Entities;
using Challenge1.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.Repositories
{
    public class PeliculaSerieRepository : IPeliculaSerieRepository, IDisposable
    {
        private ApplicationDbContext context;

        public PeliculaSerieRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        //probemos async

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




        public async Task<PeliculaSerie[]> GetAllPeliSeriesAsync()
        {
            IQueryable<PeliculaSerie> query = context.PeliculasSeries;
            // Ordenar
            query = query.OrderByDescending(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<PeliculaSerie> GetPeliSerieAsyncId(int peliserieId)
        {

            IQueryable<PeliculaSerie> query = context.PeliculasSeries.Include(c => c.Genero);

            query = query.
                Include(c => c.Personajes);

            // hacer query
            query = query.Where(c => c.Id == peliserieId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<PeliculaSerie> GetPeliSerieAsyncName(string name)
        {

            IQueryable<PeliculaSerie> query = context.PeliculasSeries.Include(c => c.Genero);

            query = query.
                Include(c => c.Personajes);

            // hacer query
            query = query.Where(c => c.Titulo == name);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<PeliculaSerie[]> GetAllPeliSeriesByName(string name, int? generoId, bool ordenAsc = true)
        {


            IQueryable<PeliculaSerie> query = context.PeliculasSeries.Include(c => c.Genero);

            query = query.Include(c => c.Personajes);
            

            if (name!=null && ordenAsc==true)
            {
                query = query.OrderBy(c => c.FechaCreacion).Where(c => c.Titulo.Contains(name));
            }
            
            if (name!=null & ordenAsc == false)
            {
                // Ordenar
                query = query.OrderByDescending(c => c.FechaCreacion)
                  .Where(c => c.Titulo.Contains(name));
            }

            if (generoId!= null & ordenAsc==true)
            {
                query = query.OrderBy(c => c.FechaCreacion)
                    .Where(c => c.Genero.Id == generoId);
            }

            if (generoId != null & ordenAsc == false)
            {
                query = query.OrderByDescending(c => c.FechaCreacion)
                    .Where(c => c.Genero.Id == generoId);
            }







            return await query.ToArrayAsync();
        }

        //sync

        public IEnumerable<PeliculaSerie> GetPeliculaSeries()
        {
            //return context.PeliculasSeries.ToList();

            //traer el objeto con la data relacionada
            IQueryable<PeliculaSerie> query = context.PeliculasSeries
                .Include(c => c.Personajes);
            // Ordenar 

            query = query.OrderByDescending(c => c.Id);

            return query.ToList();


        }

        public PeliculaSerie GetPeliculaSerieById(int id)
        {
            return context.PeliculasSeries.Find(id);
        }

        public void AddPeliculaSerie(PeliculaSerie peliserie)
        {
            context.PeliculasSeries.Add(peliserie);
        }

        public void DeletePeliculaSerie(int id)
        {
            PeliculaSerie peliserie = context.PeliculasSeries.Find(id);
            context.PeliculasSeries.Remove(peliserie);
        }

        public void UpdatePeliculaSerie(PeliculaSerie peliserie)
        {
            context.Entry(peliserie).State = EntityState.Modified;
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
