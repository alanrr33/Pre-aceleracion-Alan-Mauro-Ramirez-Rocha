using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.Entities
{
    public class PeliculaSerie
    {

        [Key]
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.MinValue;
        [Range(1, 5)]
        public decimal Calificacion { get; set; }

        // genero asociado
        public Genero Genero { get; set; }

        //personajes asociados

        public ICollection<Personaje> Personajes { get; set; }
    }
}
