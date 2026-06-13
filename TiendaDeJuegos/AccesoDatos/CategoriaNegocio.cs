using AccesoBD;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar(string filtro = "", bool soloActivos = true)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "SELECT IDCategoria, NombreCategoria, Activo FROM Categoria WHERE 1=1 ";

                if (soloActivos)
                    query += "AND Activo = 1 ";

                if (!string.IsNullOrWhiteSpace(filtro))
                    query += "AND NombreCategoria LIKE @filtro ";

                query += "ORDER BY NombreCategoria ASC";

                datos.setConsulta(query);
                if (!string.IsNullOrWhiteSpace(filtro))
                    datos.setParametro("@filtro", "%" + filtro.Trim() + "%");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.IdCategoria = (int)datos.Lector["IDCategoria"];
                    aux.NombreCategoria = (string)datos.Lector["NombreCategoria"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    lista.Add(aux);
                }

                datos.cerrarConexion();
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

        public Categoria obtenerPorId(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT IDCategoria, NombreCategoria, Activo FROM Categoria WHERE IDCategoria = @id");
                datos.setParametro("@id", idCategoria);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.IdCategoria = (int)datos.Lector["IDCategoria"];
                    aux.NombreCategoria = (string)datos.Lector["NombreCategoria"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    datos.cerrarConexion();
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
                datos.setConsulta(
                    "SELECT TOP 1 IDCategoria FROM Categoria " +
                    "WHERE LOWER(LTRIM(RTRIM(NombreCategoria))) = LOWER(LTRIM(RTRIM(@nombre))) " +
                    "AND Activo = 1 AND (@idExcluir = 0 OR IDCategoria <> @idExcluir)");
                datos.setParametro("@nombre", nombre);
                datos.setParametro("@idExcluir", idExcluir);
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
                datos.setConsulta("INSERT INTO Categoria (NombreCategoria, Activo) VALUES (@nombre, 1); SELECT CAST(SCOPE_IDENTITY() AS INT);");
                datos.setParametro("@nombre", nombre.Trim());
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

        public bool modificar(int idCategoria, string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Categoria SET NombreCategoria = @nombre WHERE IDCategoria = @id");
                datos.setParametro("@nombre", nombre.Trim());
                datos.setParametro("@id", idCategoria);
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
        public bool eliminar(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Categoria SET Activo = 0 WHERE IDCategoria = @id");
                datos.setParametro("@id", idCategoria);
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
