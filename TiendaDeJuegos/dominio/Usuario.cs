using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int IdUsuario;
        public int FechaAlta { get; set; }
        public string Nombre {  get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Telefono { get; set; }
        public int IdRol {  get; set; }
        public Rol Rol {  get; set; }
        public bool Activo { get; set; }
        public int IdDireccion { get; set; }
        public Direccion Direccion { get; set; }
    }
}
