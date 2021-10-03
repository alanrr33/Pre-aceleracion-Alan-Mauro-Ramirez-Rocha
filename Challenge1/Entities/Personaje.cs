using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.Entities
{
    public class Personaje
    {
        [Key]
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
        public string Historia { get; set; }

        //asociacion con peliculasSeries
        public ICollection<PeliculaSerie> PeliculasSeries { get; set; }
    }
}
