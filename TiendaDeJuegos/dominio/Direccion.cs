using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Direccion
    {
        public int IDDireccion { get; set; }
        public int IDUsuario { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string CodigoPostal { get; set; }
        public bool Activo { get; set; }
        public string DireccionCompleta
        {
            get
            {
                return Calle + " " + Numero + ", " + Localidad;
            }
        }
    }
}
