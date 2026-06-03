using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    

    public class Carrito
    {
        public int IdCarrito { get; set; }

        public Usuario Cliente { get; set; }

        public List<CarritoItem> ItemCarrito { get; set; } = new List<CarritoItem>();
    }
}
