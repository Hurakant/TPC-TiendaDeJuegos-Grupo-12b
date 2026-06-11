using dominio;

using System.Linq;

namespace Negocio
{
    public class CarritoNegocio
    {
        private Carrito carrito;

        public CarritoNegocio(Carrito carrito)
        {
            this.carrito = carrito;
        }

        // AGREGAR PRODUCTO
        public void AgregarProducto(Producto producto, int cantidad)
        {
            if (producto == null || cantidad <= 0)
                return;

            var item = carrito.ItemCarrito
                .FirstOrDefault(x => x.IdProducto == producto.IdProducto);

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
                    Precio = producto.Precio,
                    Cantidad = cantidad
                });
            }
        }

        // ELIMINAR PRODUCTO
        public void EliminarProducto(int idProducto)
        {
            var item = carrito.ItemCarrito
                .FirstOrDefault(x => x.IdProducto == idProducto);

            if (item != null)
                carrito.ItemCarrito.Remove(item);
        }

        // MODIFICAR CANTIDAD
        public void ModificarCantidad(int idProducto, int cantidad)
        {
            var item = carrito.ItemCarrito
                .FirstOrDefault(x => x.IdProducto == idProducto);

            if (item != null)
                item.Cantidad = cantidad;
        }

        // VACIAR CARRITO
        public void VaciarCarrito()
        {
            carrito.ItemCarrito.Clear();
        }

        // CALCULAR TOTAL
        public decimal CalcularTotal()
        {
            return carrito.ItemCarrito.Sum(x => x.Subtotal);
        }

        // CANTIDAD ITEMS
        public int CantidadTotalItems()
        {
            return carrito.ItemCarrito.Sum(x => x.Cantidad);
        }
    }
}