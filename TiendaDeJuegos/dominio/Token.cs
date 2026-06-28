using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    //Tipo de token con enum
    public enum TipoToken
    {
        VerificacionCorreo = 1,
        RecuperacionContrasena = 2
    }
    public class Token
    {
        public int IdToken { get; set; }
        public int IdUsuario { get; set; }
        public string Codigo { get; set; }
        public TipoToken Tipo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
