using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.ViewModels.Genero.Get
{
    public class GetAllGenerosResponseViewModel
    {
        public string Imagen { get; set; }
        public string Nombre { get; set; }

        //peliculas series asociadas
        //public ICollection<PeliculaSerieModel> PeliculasSeries { get; set; }
    }
}
