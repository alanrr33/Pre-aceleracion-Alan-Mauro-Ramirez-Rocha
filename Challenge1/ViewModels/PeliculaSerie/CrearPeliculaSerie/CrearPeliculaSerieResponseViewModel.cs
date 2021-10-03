using Challenge1.ViewModels.Personaje.DetallePersonaje;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.ViewModels.PeliculaSerie.CrearPeliculaSerie
{
    public class CrearPeliculaSerieResponseViewModel
    {
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.MinValue;

        [Range(1, 5)]
        public decimal Calificacion { get; set; }

    }
}
