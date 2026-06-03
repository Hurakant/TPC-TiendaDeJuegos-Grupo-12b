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
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public int FechaDeCreacionPedido { get; set; }
        public decimal MontoTotal { get; set; }
        public int IdFormaDePago { get; set; }
        public FormaDePago FormaDePago { get; set; }
        public int FormaDeEntrega { get; set; }
        public int IdDireccion { get; set; }
        public Direccion Direccion { get; set; }
        public EstadoPedido Estado { get; set; }
    }
}
