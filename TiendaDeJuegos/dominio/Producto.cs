using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
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
