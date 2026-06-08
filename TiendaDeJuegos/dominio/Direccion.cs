using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        public int IdUsuario { get; set; }
        public string Calle {  get; set; }
        public string NumeroDeCasa { get; set; }
        public string localidad { get; set; }
        public string CodigoPostal { get; set; }
        public string Observaciones { get; set; }
        public bool EsPrincipal { get; set; }
        public bool Activa {  get; set; }
    }
}
