using Challenge1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.Interfaces
{



    public interface IPeliculaSerieRepository : IDisposable
    {
        //async

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<PeliculaSerie[]> GetAllPeliSeriesAsync();
        Task<PeliculaSerie> GetPeliSerieAsyncId(int peliserieId);
        Task<PeliculaSerie> GetPeliSerieAsyncName(string name);
        Task<PeliculaSerie[]> GetAllPeliSeriesByName(string name, string orden);
        Task<PeliculaSerie[]> GetAllPeliSeriesById(int? generoId, string orden);


        //sync
        /*
        IEnumerable<PeliculaSerie> GetPeliculaSeries();
        PeliculaSerie GetPeliculaSerieById(int id);
        void AddPeliculaSerie(PeliculaSerie peliserie);
        void DeletePeliculaSerie(int peliserieId);
        void UpdatePeliculaSerie(PeliculaSerie peliserie);
        void Save();*/
    }
}
