using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Usuario
    {
        public int IdUsuario { get; set; } 
        public DateTime FechaAlta { get; set; }
        public string Nombre {  get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Telefono { get; set; }
        public Rol Rol {  get; set; }
        public bool Activo { get; set; }
        public bool CorreoVerificado { get; set; }
        public List<Direccion> Direccion { get; set; } = new List<Direccion>();
    }
}
