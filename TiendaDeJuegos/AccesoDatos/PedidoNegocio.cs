using AccesoBD;
using dominio;
using System;
using System.Collections.Generic;
using System.Data;

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

        public List<Pedido> ListarTodos()
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
                p.IDFormaDeEntrega,
                u.Nombre
            FROM Pedido p
            INNER JOIN Usuario u ON u.IdUsuario = p.IDUsuario
            ORDER BY p.FechaCreacion DESC
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido p = new Pedido();

                    p.IdPedido = (int)datos.Lector["IDPedido"];
                    p.Fecha = (DateTime)datos.Lector["FechaCreacion"];
                    p.MontoTotal = (decimal)datos.Lector["MontoTotal"];
                    p.Estado = (EstadoPedido)Convert.ToInt32(datos.Lector["IDEstadoPedido"]);

                    p.Cliente = new Usuario
                    {
                        Nombre = datos.Lector["Nombre"].ToString()
                    };

                    lista.Add(p);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public DataTable ListarTodosParaGrid()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable tabla = new DataTable();

            try
            {
                datos.setConsulta(@"
            SELECT 
                p.IDPedido,
                u.Nombre AS Cliente,
                p.FechaCreacion,
                p.MontoTotal,
                p.IDEstadoPedido
            FROM Pedido p
            INNER JOIN Usuario u ON u.IdUsuario = p.IDUsuario
            ORDER BY p.FechaCreacion DESC
        ");

                datos.ejecutarLectura();

                tabla.Load(datos.Lector);

                return tabla;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Pedido ObtenerPedido(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
            SELECT
                IDPedido,
                FechaCreacion,
                MontoTotal,
                IDEstadoPedido,
                IDFormaDePago,
                IDFormaDeEntrega
            FROM Pedido
            WHERE IDPedido = @id
        ");

                datos.setParametro("@id", idPedido);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Pedido p = new Pedido();

                    p.IdPedido = (int)datos.Lector["IDPedido"];
                    p.Fecha = (DateTime)datos.Lector["FechaCreacion"];
                    p.MontoTotal = (decimal)datos.Lector["MontoTotal"];
                    p.Estado = (EstadoPedido)Convert.ToInt32(datos.Lector["IDEstadoPedido"]);

                    int idPago = (int)datos.Lector["IDFormaDePago"];
                    p.FormaDePago = new FormaDePago
                    {
                        IdFormaDePago = idPago,
                        Nombre = ObtenerNombrePago(idPago)
                    };

                    p.FormaDeEntrega =
                        (FormaDeEntrega)Convert.ToInt32(datos.Lector["IDFormaDeEntrega"]);

                    return p;
                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public DataTable ObtenerDetallePedido(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable tabla = new DataTable();

            try
            {
                datos.setConsulta(@"
            SELECT
                p.Nombre AS NombreProducto,
                dp.Cantidad,
                dp.PrecioUnitario,
                dp.Subtotal
            FROM DetallePedido dp
            INNER JOIN Producto p
                ON p.IDProducto = dp.IDProducto
            WHERE dp.IDPedido = @id
        ");

                datos.setParametro("@id", idPedido);

                datos.ejecutarLectura();

                tabla.Load(datos.Lector);

                return tabla;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void ModificarEstado(Pedido pedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
            UPDATE Pedido
            SET IDEstadoPedido = @Estado
            WHERE IDPedido = @IdPedido
        ");

                datos.setParametro("@Estado", (int)pedido.Estado);
                datos.setParametro("@IdPedido", pedido.IdPedido);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void ModificarPedidoCompleto(Pedido pedido)
{
    AccesoDatos datos = new AccesoDatos();

    try
    {
        datos.setConsulta(@"
            UPDATE Pedido
            SET 
                IDEstadoPedido = @Estado,
                IDFormaDePago = @Pago,
                IDFormaDeEntrega = @Entrega
            WHERE IDPedido = @IdPedido
        ");

        datos.setParametro("@IdPedido", pedido.IdPedido);
        datos.setParametro("@Estado", (int)pedido.Estado);
        datos.setParametro("@Pago", pedido.FormaDePago.IdFormaDePago);
        datos.setParametro("@Entrega", (int)pedido.FormaDeEntrega);

        datos.ejecutarAccion();
    }
    finally
    {
        datos.cerrarConexion();
    }
}
    }
}