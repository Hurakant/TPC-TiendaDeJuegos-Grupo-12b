using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pedido
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        public int EstadoPedidoId { get; set; }
        public EstadoPedido EstadoPedido { get; set; }

        public int FormaPagoId { get; set; }
        public FormaPago FormaPago { get; set; }

        public int FormaEntregaId { get; set; }
        public FormaEntrega FormaEntrega { get; set; }

        public int? DireccionId { get; set; }
        public Direccion Direccion { get; set; }

        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }
}
}
