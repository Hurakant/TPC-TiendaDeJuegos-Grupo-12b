using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Usuario
    {
        public int IdUsuario;
        public Fecha FechaAlta { get; set; }
        public string Nombre {  get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Telefono { get; set; }
        public Rol Rol {  get; set; }
        public bool Activo { get; set; }
        public List<Direccion> Direccion { get; set; } = new List<Direccion>();
    }
}
