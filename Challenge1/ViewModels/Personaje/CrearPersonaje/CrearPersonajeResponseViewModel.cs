using Challenge1.ViewModels.PeliculaSerie.CrearPeliculaSerie;
using Challenge1.ViewModels.PeliculaSerie.DetallePeliculaSerie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.ViewModels.Personaje.CrearPersonaje
{
    public class CrearPersonajeResponseViewModel
    {
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
        public string Historia { get; set; }

    }
}
