using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Challenge1.Entities;
using Challenge1.ViewModels.Personaje.CrearPersonaje;
using Challenge1.ViewModels.Personaje.DetallePersonaje;
using Challenge1.ViewModels.Personaje.ListarPersonajes;

namespace Challenge1.Data
{
    public class PersonajeProfile:Profile
    {
        public PersonajeProfile()
        {
            //listar personajes
            this.CreateMap<Personaje, ListarPersonajesResponseViewModel>();

            //crear personaje
            this.CreateMap<Personaje, CrearPersonajeRequestViewModel>();
            this.CreateMap<Personaje, CrearPersonajeResponseViewModel>();
            this.CreateMap<CrearPersonajeRequestViewModel, Personaje>();
            this.CreateMap<CrearPersonajeResponseViewModel, Personaje>();

            //detalle personaje

            this.CreateMap<Personaje, DetallePersonajeResponseViewModel>();
            this.CreateMap<DetallePersonajeResponseViewModel, Personaje>();

        }

    }

}
