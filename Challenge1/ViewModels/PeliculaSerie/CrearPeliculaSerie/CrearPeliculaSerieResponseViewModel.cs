using System;
using System.ComponentModel.DataAnnotations;


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
