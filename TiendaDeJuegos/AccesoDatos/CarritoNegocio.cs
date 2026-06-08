using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    public class CarritoNegocio

    {
        private Carrito carrito;

        public CarritoNegocio(Carrito carrito)
        {
            this.carrito = carrito;
        }
        public void AgregarProducto(Producto producto, int cantidad)
        {
            var item = carrito.ItemCarrito
        .FirstOrDefault(x => x.IdProducto == producto.IdProducto);

            decimal precioFinal = producto.Precio;

            if (item != null)
            {
                item.Cantidad += cantidad;
            }
            else
            {
                carrito.ItemCarrito.Add(new CarritoItem
                {
                    IdProducto = producto.IdProducto,
                    Nombre = producto.Nombre,
                    Precio = precioFinal,
                    Cantidad = cantidad
                });
            }

        }

        public void EliminarProducto(int idProducto)
        {
            var item = carrito.ItemCarrito
        .FirstOrDefault(x => x.IdProducto == idProducto);

            if (item != null)
                carrito.ItemCarrito.Remove(item);

        }

        public void ModificarCantidad()
        {

        }

        public decimal CalcularTotal()
        {
            return 0;
        }
    }
}
