using AccesoBD;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AccesibilidadNegocio
    {
        public List<Accesibilidad> listar(string filtro = "", bool soloActivos = true)
        {
            List<Accesibilidad> lista = new List<Accesibilidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("EXEC SP_Accesibilidad_Listar @Filtro = @Filtro, @SoloActivos = @SoloActivos");
                datos.setParametro("@Filtro", string.IsNullOrWhiteSpace(filtro) ? (object)DBNull.Value : filtro.Trim());
                datos.setParametro("@SoloActivos", soloActivos);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Accesibilidad aux = new Accesibilidad();
                    aux.IdAccesibilidad = (int)datos.Lector["IDAccesibilidad"];
                    aux.NombreAccesibilidad = (string)datos.Lector["NombreAccesibilidad"];
                    aux.Activo = (bool)datos.Lector["Activo"];
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

        public Accesibilidad obtenerPorId(int idAccesibilidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("EXEC SP_Accesibilidad_ObtenerPorId @IDAccesibilidad = @IDAccesibilidad");
                datos.setParametro("@IDAccesibilidad", idAccesibilidad);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Accesibilidad aux = new Accesibilidad();
                    aux.IdAccesibilidad = (int)datos.Lector["IDAccesibilidad"];
                    aux.NombreAccesibilidad = (string)datos.Lector["NombreAccesibilidad"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    return aux;
                }

                return null;
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

        public bool existeNombre(string nombre, int idExcluir = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("EXEC SP_Accesibilidad_ExisteNombre @Nombre = @Nombre, @IdExcluir = @IdExcluir");
                datos.setParametro("@Nombre", nombre);
                datos.setParametro("@IdExcluir", idExcluir);
                datos.ejecutarLectura();
                return datos.Lector.Read();
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

        public int agregar(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("EXEC SP_Accesibilidad_Agregar @Nombre = @Nombre");
                datos.setParametro("@Nombre", nombre.Trim());
                datos.ejecutarLectura();

                int idNuevo = 0;
                if (datos.Lector.Read())
                    idNuevo = (int)datos.Lector[0];
                return idNuevo;
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

        public bool modificar(int idAccesibilidad, string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("EXEC SP_Accesibilidad_Modificar @IDAccesibilidad = @IDAccesibilidad, @Nombre = @Nombre");
                datos.setParametro("@IDAccesibilidad", idAccesibilidad);
                datos.setParametro("@Nombre", nombre.Trim());
                datos.ejecutarAccion();
                return true;
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

        //Eliminación logica, no fisica
        public bool eliminar(int idAccesibilidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("EXEC SP_Accesibilidad_Eliminar @IDAccesibilidad = @IDAccesibilidad");
                datos.setParametro("@IDAccesibilidad", idAccesibilidad);
                datos.ejecutarAccion();
                return true;
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