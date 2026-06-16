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
                datos.setConsulta("SELECT IDUsuario, Nombre, Apellido, Rol , Telefono FROM USUARIO WHERE Email = @email AND Contrasena = @pass AND Activo = 1");
                datos.setParametro("@email", usuario.Email);
                datos.setParametro("@pass", usuario.Contraseña);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)(datos.Lector["IDUsuario"]);
                    usuario.Nombre = datos.Lector["Nombre"].ToString();
                    usuario.Apellido = datos.Lector["Apellido"].ToString();
                    usuario.Telefono = datos.Lector["Telefono"].ToString();

                    usuario.Rol = (Rol)(datos.Lector["Rol"]);
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


        public int RegistroUser (Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetProcedimiento("SP_InsertarUsuario");
                datos.setParametro("@nombre",nuevo.Nombre);
                datos.setParametro("@Apellido", nuevo.Apellido);
                datos.setParametro("@Email", nuevo.Email);
                datos.setParametro("@Contraseña", nuevo.Contraseña);
                datos.setParametro("@Telefono", nuevo.Telefono);

                return datos.ejecutarAccionScalar();
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

        public void ModificarUser(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono WHERE IdUsuario = @Id");
                datos.setParametro("@Nombre", usuario.Nombre);
                datos.setParametro("@Apellido", usuario.Apellido);
                datos.setParametro("@Email", usuario.Email);
                datos.setParametro("@Telefono", usuario.Telefono);
                datos.setParametro("@Id", usuario.IdUsuario);

                datos.ejecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

    }
}
