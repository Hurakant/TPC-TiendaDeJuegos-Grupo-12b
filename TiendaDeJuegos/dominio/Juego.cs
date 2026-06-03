using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Juego
    {
        public int IdJuego { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public int Stock { get; set; }
        public int FechaLanzamiento { get; set; }
        public bool EsDigital { get; set; }
        public bool Activo { get; set; }

        public List<Categoria> Categoria { get; set; } = new List<Categoria>();
    }
}
