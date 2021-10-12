using Challenge1.ViewModels.Personaje.CrearPersonaje;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Challenge1.ViewModels.PeliculaSerie.DetallePeliculaSerie
{
    public class DetallePeliculaSerieResponseViewModel
    {
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.MinValue;

        [Range(1, 5)]
        public decimal Calificacion { get; set; }

        //genero asociado
        public string Genero { get; set; }

        //personajes asociados

        public ICollection<CrearPersonajeResponseViewModel> Personajes { get; set; }

    }
}
