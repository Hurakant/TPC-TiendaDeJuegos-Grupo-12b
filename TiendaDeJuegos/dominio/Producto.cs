using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Producto
        //Borraria Juegos y dejaria producto, pero ya la hice y me da flojera
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
    }
}
