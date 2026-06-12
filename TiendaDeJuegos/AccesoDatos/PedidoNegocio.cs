using dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class PedidoNegocio
    {
        private List<Pedido> pedidos;

        public PedidoNegocio(List<Pedido> pedidos)
        {
            this.pedidos = pedidos;
        }

        // CREAR PEDIDO
        public void CrearPedido(Pedido pedido)
        {
            if (pedido == null)
                return;

            pedidos.Add(pedido);
        }

        // LISTAR PEDIDOS
        public List<Pedido> ListarPedidos()
        {
            return pedidos;
        }

        // CAMBIAR ESTADO
        public void CambiarEstado(int idPedido, EstadoPedido nuevoEstado)
        {
            var pedido = pedidos.Find(x => x.IdPedido == idPedido);

            if (pedido != null)
            {
                pedido.Estado = nuevoEstado;
            }
        }
    }
}
