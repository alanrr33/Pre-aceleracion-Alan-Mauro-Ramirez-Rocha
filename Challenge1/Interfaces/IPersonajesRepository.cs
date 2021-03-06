using Challenge1.Entities;
using System;
using System.Threading.Tasks;

namespace Challenge1.Interfaces
{

    public interface IPersonajesRepository : IDisposable
    {
        //async

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Personaje[]> GetAllPersonajesAsync();

        Task<Personaje> GetPersonajeNameAsync(string name);
        Task<Personaje> GetPersonajeIdAsync(int id);
        Task<Personaje[]> GetAllPersonajesByName(string name);

        Task<Personaje[]> GetAllPersonajesByEdad(int? edad);
        Task<Personaje[]> GetAllPersonajesByPeso(float? peso);
        Task<Personaje[]> GetAllPersonajesByPeliSerieId(int? peliSerieId);

    }
}
