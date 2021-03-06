using Challenge1.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge1.Interfaces
{
    public interface IGeneroRepository : IDisposable
    {
        //async

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Genero[]> GetAllGenerosAsync();
        Task<Genero> GetGeneroIdAsync(int generoId);
        Task<Genero> GetGeneroNameAsync(string name);
        Task<Genero[]> GetAllGenerosNameAsync(string name, bool ordenAsc = true);
    }
}
