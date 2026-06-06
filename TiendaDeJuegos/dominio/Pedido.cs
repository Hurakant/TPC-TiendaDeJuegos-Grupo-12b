using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public Usuario Usuario { get; set; }
        public Fecha FechaDeCreacionPedido { get; set; }
        public decimal MontoTotal { get; set; }
        public FormaDePago FormaDePago { get; set; }
        public int IDFormaDeEntrega { get; set; }
        public Direccion Direccion { get; set; }
        public EstadoPedido Estado { get; set; }
    }
}
