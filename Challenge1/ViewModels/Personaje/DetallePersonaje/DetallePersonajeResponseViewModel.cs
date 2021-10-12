using Challenge1.ViewModels.PeliculaSerie.CrearPeliculaSerie;
using System.Collections.Generic;


namespace Challenge1.ViewModels.Personaje.DetallePersonaje
{
    public class DetallePersonajeResponseViewModel
    {
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
        public string Historia { get; set; }

        //asociacion con peliculasSeries
        public ICollection<CrearPeliculaSerieResponseViewModel> PeliculasSeries { get; set; }

    }
}
