using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.ViewModels.PeliculaSerie.ListarPeliculaSerie
{
    public class ListarPeliculaSerieResponseViewModel
    {
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.MinValue;

    }
}
