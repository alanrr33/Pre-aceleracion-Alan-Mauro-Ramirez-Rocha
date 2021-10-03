using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.Entities
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }

        //peliculas series asociadas

        public ICollection<PeliculaSerie> PeliculasSeries { get; set; }

    }
}
