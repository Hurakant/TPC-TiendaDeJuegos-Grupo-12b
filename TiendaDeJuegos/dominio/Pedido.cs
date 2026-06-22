using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pedido
    {
        public int IdPedido { get; set; }

        public Usuario Cliente { get; set; }

        public List<CarritoItem> Detalle { get; set; } = new List<CarritoItem>();

        public DateTime Fecha { get; set; }

        public EstadoPedido Estado { get; set; }

        public FormaDePago FormaDePago { get; set; }

        public FormaDeEntrega FormaDeEntrega { get; set; }

        public Direccion Direccion { get; set; }

        public decimal Total => Detalle.Sum(x => x.Subtotal);

        public string FormaDePagoTexto =>
            FormaDePago != null ? FormaDePago.Nombre : "";
        public decimal MontoTotal { get; set; }
    }
}
