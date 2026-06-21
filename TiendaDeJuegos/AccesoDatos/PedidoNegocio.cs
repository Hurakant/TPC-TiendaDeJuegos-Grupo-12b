using AccesoBD;
using dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class PedidoNegocio
    {
        // CREAR PEDIDO
        public int CrearPedido(Pedido pedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    INSERT INTO Pedido
                    (
                        IDUsuario,
                        MontoTotal,
                        IDFormaDePago,
                        IDFormaDeEntrega,
                        IDEstadoPedido
                    )
                    OUTPUT INSERTED.IDPedido
                    VALUES
                    (
                        @IDUsuario,
                        @MontoTotal,
                        @IDFormaDePago,
                        @IDFormaDeEntrega,
                        @IDEstadoPedido
                    )
                ");

                datos.setParametro("@IDUsuario", pedido.Cliente.IdUsuario);
                datos.setParametro("@MontoTotal", pedido.Total);
                datos.setParametro("@IDFormaDePago", pedido.FormaDePago.IdFormaDePago);
                datos.setParametro("@IDFormaDeEntrega", (int)pedido.FormaDeEntrega);
                datos.setParametro("@IDEstadoPedido", 1);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return Convert.ToInt32(datos.Lector["IDPedido"]);

                return 0;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // DETALLE
        public void AgregarDetalle(int idPedido, CarritoItem item)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    INSERT INTO DetallePedido
                    (
                        IDPedido,
                        IDProducto,
                        Cantidad,
                        PrecioUnitario,
                        Subtotal
                    )
                    VALUES
                    (
                        @IDPedido,
                        @IDProducto,
                        @Cantidad,
                        @PrecioUnitario,
                        @Subtotal
                    )
                ");

                datos.setParametro("@IDPedido", idPedido);
                datos.setParametro("@IDProducto", item.IdProducto);
                datos.setParametro("@Cantidad", item.Cantidad);
                datos.setParametro("@PrecioUnitario", item.Precio);
                datos.setParametro("@Subtotal", item.Subtotal);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public List<Pedido> ListarPorUsuario(int idUsuario)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
            SELECT 
                p.IDPedido,
                p.FechaCreacion,
                p.MontoTotal,
                p.IDEstadoPedido,
                p.IDFormaDePago,
                p.IDFormaDeEntrega
            FROM Pedido p
            WHERE p.IDUsuario = @IDUsuario
            ORDER BY p.FechaCreacion DESC
        ");

                datos.setParametro("@IDUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido p = new Pedido();

                    p.IdPedido = (int)datos.Lector["IDPedido"];
                    p.Fecha = (DateTime)datos.Lector["FechaCreacion"];
                    p.Estado = (EstadoPedido)Convert.ToInt32(datos.Lector["IDEstadoPedido"]);
                    p.MontoTotal = (decimal)datos.Lector["MontoTotal"];

                    //  FORMA DE PAGO
                    int idPago = (int)datos.Lector["IDFormaDePago"];
                    p.FormaDePago = new FormaDePago
                    {
                        IdFormaDePago = idPago,
                        Nombre = ObtenerNombrePago(idPago)
                    };

                    //  FORMA DE ENTREGA
                    p.FormaDeEntrega = (FormaDeEntrega)Convert.ToInt32(datos.Lector["IDFormaDeEntrega"]);

                    lista.Add(p);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private string ObtenerNombrePago(int id)
        {
            switch (id)
            {
                case 1: return "Efectivo";
                case 2: return "Transferencia";
                case 3: return "Tarjeta";
                case 4: return "MercadoPago";
                default: return "Desconocido";
            }
        }
    }
}