using AutoMapper;
using Challenge1.Entities;
using Challenge1.ViewModels.Genero.Get;
using Challenge1.ViewModels.Genero.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.Data
{
    public class GeneroProfile:Profile
    {
        public GeneroProfile()
        {
            this.CreateMap<Genero, GetAllGenerosResponseViewModel>();

            //post
            this.CreateMap<Genero, CrearGeneroRequestViewModel>();
            this.CreateMap<Genero, CrearGeneroResponseViewModel>();
            this.CreateMap<CrearGeneroRequestViewModel, Genero>();
            this.CreateMap<CrearGeneroResponseViewModel, Genero>();

        }
    }
}
