using AutoMapper;
using Challenge1.Entities;
using Challenge1.ViewModels.PeliculaSerie.CrearPeliculaSerie;
using Challenge1.ViewModels.PeliculaSerie.DetallePeliculaSerie;
using Challenge1.ViewModels.PeliculaSerie.ListarPeliculaSerie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.Data
{
    public class PeliculaSerieProfile:Profile
    {
        public PeliculaSerieProfile()
        {

            //listar peliculas 
            this.CreateMap<PeliculaSerie, ListarPeliculaSerieResponseViewModel>();
            this.CreateMap<ListarPeliculaSerieResponseViewModel, PeliculaSerie>();

            //crear peliculas
            this.CreateMap<PeliculaSerie, CrearPeliculaSerieRequestViewModel>();
            this.CreateMap<CrearPeliculaSerieRequestViewModel, PeliculaSerie>();

            this.CreateMap<PeliculaSerie, CrearPeliculaSerieResponseViewModel>();
            this.CreateMap<CrearPeliculaSerieResponseViewModel, PeliculaSerie>();


            //detalle pelicula
            this.CreateMap<PeliculaSerie, DetallePeliculaSerieResponseViewModel>()
                .ForMember(c => c.Genero, o => o.MapFrom(m => m.Genero.Nombre));
            this.CreateMap<DetallePeliculaSerieResponseViewModel, PeliculaSerie>();



        }

    }
}
