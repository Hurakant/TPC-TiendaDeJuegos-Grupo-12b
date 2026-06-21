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
                datos.setParametro("@Contrasena", nuevo.Contraseña);
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

        public void ModificarUserAdmin(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono , Rol = @Rol WHERE IdUsuario = @Id");
                datos.setParametro("@Nombre", usuario.Nombre);
                datos.setParametro("@Apellido", usuario.Apellido);
                datos.setParametro("@Email", usuario.Email);
                datos.setParametro("@Telefono", usuario.Telefono);
                datos.setParametro("@Rol", usuario.Rol);
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


        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IDUsuario,Nombre, Apellido, Email, Telefono, Activo, Rol FROM USUARIO");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.IdUsuario = (int)datos.Lector["IDUsuario"];
                    aux.Nombre = datos.Lector["Nombre"].ToString();
                    aux.Apellido = datos.Lector["Apellido"].ToString();
                    aux.Email = datos.Lector["Email"].ToString();
                    aux.Telefono = datos.Lector["Telefono"].ToString();
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Rol = (Rol)datos.Lector["Rol"];

                    lista.Add(aux);
                }

                return lista;
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

        public void eliminar(int id) // logico
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE Usuario SET Activo = 0 WHERE IdUsuario = @id");
                datos.setParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void activar(int id) // logco
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE Usuario SET Activo = 1 WHERE IdUsuario = @id");
                datos.setParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Usuario obtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario user = new Usuario();

            try
            {
                datos.setConsulta("SELECT IDUsuario, Nombre, Apellido, Email, Telefono,Rol, Activo FROM Usuario WHERE IDUsuario = @id");
                datos.setParametro("@id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    user.IdUsuario = (int)datos.Lector["IdUsuario"];
                    user.Nombre = (string)datos.Lector["Nombre"];
                    user.Apellido = (string)datos.Lector["Apellido"];
                    user.Email = (string)datos.Lector["Email"];
                    user.Telefono = (string)datos.Lector["Telefono"];
                    user.Activo = (bool)(datos.Lector["Activo"]);

                    user.Rol = (Rol)datos.Lector["Rol"]; 
                }

                return user;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
