using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class CarritoItem
    {
        public int IdCarritoItem { get; set; }
        public int IdCarrito { get; set; }
        public int IdJuego { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }
}
