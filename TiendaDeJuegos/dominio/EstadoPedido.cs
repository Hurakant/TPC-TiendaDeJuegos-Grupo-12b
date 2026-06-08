using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public enum EstadoPedido
    {
        Pendiente = 1,
        Pagado = 2,
        EnPreparacion = 3,
        Enviado = 4,
        Entregado = 5,
        Cancelado = 6
    }
}
