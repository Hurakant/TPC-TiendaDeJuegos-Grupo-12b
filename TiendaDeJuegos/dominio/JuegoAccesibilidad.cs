using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class JuegoAccesibilidad
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int AccesibilidadId { get; set; }
        public Accesibilidad Accesibilidad { get; set; }

        public string Detalle { get; set; }
    }
}
