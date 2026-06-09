using AccesoBD;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {

        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT IDUsuario, Nombre, Apellido, Rol FROM USUARIO WHERE Email = @email AND Contraseña = @pass AND Activo = 1");
                datos.setParametro("@email", usuario.Email);
                datos.setParametro("@pass", usuario.Contraseña);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)(datos.Lector["IDUsuario"]);
                    usuario.Nombre = datos.Lector["Nombre"].ToString();
                    usuario.Apellido = datos.Lector["Apellido"].ToString();

                    usuario.Rol = (Rol)Convert.ToInt32(datos.Lector["Rol"]);
                    //usuario.Rol = (Rol)datos.Lector["Rol"];  si es con int rol, ahora esta como varchar :O si usan la del script
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
    }
}
