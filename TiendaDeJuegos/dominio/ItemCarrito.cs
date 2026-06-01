using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class ItemCarrito
    {
        public int Id { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal Subtotal => Producto.Precio * Cantidad;
    }
}
