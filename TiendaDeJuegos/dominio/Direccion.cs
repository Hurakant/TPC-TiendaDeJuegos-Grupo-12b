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
                string direccion = Calle + " " + Numero;

                if (!string.IsNullOrWhiteSpace(Piso))
                    direccion += ", Piso " + Piso;

                if (!string.IsNullOrWhiteSpace(Depto))
                    direccion += ", Depto " + Depto;

                direccion += ", " + Localidad;
                direccion += ", " + Provincia;
                direccion += " (" + CodigoPostal + ")";

                return direccion;
            }
        }
    }
}
