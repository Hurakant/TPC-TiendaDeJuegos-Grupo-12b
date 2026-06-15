using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AccesoBD;

namespace Negocio
{
    public class ProductoNegocio
    {
        //LISTAR (No voy a nombrarlos todos porque tienen nombres descriptivos)
        //POR HACER:
        //Añadir procedures en todas las operaciones, expandir Actualizar stock para que sea:
        //Actualizar, Aumentar, Disminuir para no hacerlo de forma arcaica cuando se hagan las compras

        






         public List<Producto> listar()
         {
             List<Producto> lista = new List<Producto>();
             AccesoDatos datos = new AccesoDatos();

             try
             {
                 datos.setConsulta("SELECT P.IDProducto, P.Nombre, P.Descripcion, P.Precio, P.Descuento, P.Stock, P.EsDigital, P.Activo, P.ImagenUrl, P.IDCategoria, C.NombreCategoria FROM Producto P LEFT JOIN Categoria C ON P.IDCategoria = C.IDCategoria WHERE P.Activo = 1 ORDER BY P.Nombre ASC");
                 datos.ejecutarLectura();

                 while (datos.Lector.Read())
                 {
                     Producto aux = new Producto();

                     aux.IdProducto = (int)datos.Lector["IDProducto"];
                     aux.Nombre = (string)datos.Lector["Nombre"];
                     aux.Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"];
                     aux.Precio = (decimal)datos.Lector["Precio"];
                     aux.Descuento = (decimal)datos.Lector["Descuento"];
                     aux.Stock = (int)datos.Lector["Stock"];
                     aux.EsDigital = (bool)datos.Lector["EsDigital"];
                     aux.Activo = (bool)datos.Lector["Activo"];
                     aux.ImagenUrl = datos.Lector["ImagenUrl"] is DBNull ? "" : (string)datos.Lector["ImagenUrl"];

                     Categoria cat = new Categoria();
                     cat.IdCategoria = (int)datos.Lector["IDCategoria"];
                     cat.NombreCategoria = datos.Lector["NombreCategoria"] is DBNull ? "Sin Categoría" : (string)datos.Lector["NombreCategoria"];
                     aux.Categoria.Add(cat);

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
        public Producto listarPorId(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(
                    "SELECT P.IDProducto, P.Nombre, P.Descripcion, P.Precio, P.Descuento, P.Stock, " +
                    "P.EsDigital, P.Activo, P.ImagenUrl, P.IDCategoria, C.NombreCategoria " +
                    "FROM Producto P LEFT JOIN Categoria C ON P.IDCategoria = C.IDCategoria " +
                    "WHERE P.IDProducto = @id");
                datos.setParametro("@id", idProducto);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Producto aux = new Producto();

                    aux.IdProducto = (int)datos.Lector["IDProducto"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Descuento = (decimal)datos.Lector["Descuento"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.EsDigital = (bool)datos.Lector["EsDigital"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    aux.ImagenUrl = datos.Lector["ImagenUrl"] is DBNull ? "" : (string)datos.Lector["ImagenUrl"];

                    Categoria cat = new Categoria();
                    cat.IdCategoria = (int)datos.Lector["IDCategoria"];
                    cat.NombreCategoria = datos.Lector["NombreCategoria"] is DBNull ? "Sin Categoría" : (string)datos.Lector["NombreCategoria"];
                    aux.Categoria.Add(cat);

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
        public List<Producto> listarFiltrado(string texto, List<int> idsCategorias, int orden)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT P.IDProducto, P.Nombre, P.Descripcion, P.Precio, P.Descuento, P.Stock, ");
                query.Append("P.EsDigital, P.Activo, P.ImagenUrl, P.IDCategoria, C.NombreCategoria ");
                query.Append("FROM Producto P LEFT JOIN Categoria C ON P.IDCategoria = C.IDCategoria ");
                query.Append("WHERE P.Activo = 1 ");

                if (!string.IsNullOrWhiteSpace(texto))
                    query.Append("AND P.Nombre LIKE @texto ");

                if (idsCategorias != null && idsCategorias.Count > 0)
                {
                    query.Append("AND P.IDCategoria IN (");
                    for (int i = 0; i < idsCategorias.Count; i++)
                    {
                        query.Append("@cat" + i);
                        if (i < idsCategorias.Count - 1)
                            query.Append(", ");
                    }
                    query.Append(") ");
                }

                switch (orden)
                {
                    case 1:
                        query.Append("ORDER BY P.Precio ASC ");
                        break;
                    case 2:
                        query.Append("ORDER BY P.Precio DESC ");
                        break;
                    default:
                        query.Append("ORDER BY P.Nombre ASC ");
                        break;
                }

                datos.setConsulta(query.ToString());

                if (!string.IsNullOrWhiteSpace(texto))
                    datos.setParametro("@texto", "%" + texto.Trim() + "%");

                if (idsCategorias != null && idsCategorias.Count > 0)
                {
                    for (int i = 0; i < idsCategorias.Count; i++)
                        datos.setParametro("@cat" + i, idsCategorias[i]);
                }

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();

                    aux.IdProducto = (int)datos.Lector["IDProducto"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Descuento = (decimal)datos.Lector["Descuento"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.EsDigital = (bool)datos.Lector["EsDigital"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    aux.ImagenUrl = datos.Lector["ImagenUrl"] is DBNull ? "" : (string)datos.Lector["ImagenUrl"];

                    Categoria cat = new Categoria();
                    cat.IdCategoria = (int)datos.Lector["IDCategoria"];
                    cat.NombreCategoria = datos.Lector["NombreCategoria"] is DBNull ? "Sin Categoría" : (string)datos.Lector["NombreCategoria"];
                    aux.Categoria.Add(cat);

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
        public void agregar(Producto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(
                    "INSERT INTO Producto (Nombre, Descripcion, Precio, Descuento, Stock, EsDigital, Activo, ImagenUrl, IDCategoria) " +
                    "VALUES (@Nombre, @Descripcion, @Precio, @Descuento, @Stock, @EsDigital, 1, @ImagenUrl, @IDCategoria); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT);");
                datos.setParametro("@Nombre", nuevo.Nombre);
                datos.setParametro("@Descripcion", nuevo.Descripcion ?? "");
                datos.setParametro("@Precio", nuevo.Precio);
                datos.setParametro("@Descuento", nuevo.Descuento);
                datos.setParametro("@Stock", nuevo.Stock);
                datos.setParametro("@EsDigital", nuevo.EsDigital);
                datos.setParametro("@ImagenUrl", nuevo.ImagenUrl ?? "");
                datos.setParametro("@IDCategoria", nuevo.Categoria.Count > 0 ? (object)nuevo.Categoria[0].IdCategoria : DBNull.Value);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    datos.Lector.Close();
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
        public void modificar(Producto prod)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(
                    "UPDATE Producto SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, " +
                    "Descuento = @Descuento, Stock = @Stock, EsDigital = @EsDigital, Activo = @Activo, " +
                    "ImagenUrl = @ImagenUrl, IDCategoria = @IDCategoria WHERE IDProducto = @IDProducto");
                datos.setParametro("@IDProducto", prod.IdProducto);
                datos.setParametro("@Nombre", prod.Nombre);
                datos.setParametro("@Descripcion", prod.Descripcion ?? "");
                datos.setParametro("@Precio", prod.Precio);
                datos.setParametro("@Descuento", prod.Descuento);
                datos.setParametro("@Stock", prod.Stock);
                datos.setParametro("@EsDigital", prod.EsDigital);
                datos.setParametro("@Activo", prod.Activo);
                datos.setParametro("@ImagenUrl", prod.ImagenUrl ?? "");
                datos.setParametro("@IDCategoria", prod.Categoria.Count > 0 ? (object)prod.Categoria[0].IdCategoria : DBNull.Value);
                datos.ejecutarAccion();
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
        public void eliminar(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Producto SET Activo = 0 WHERE IDProducto = @IDProducto");
                datos.setParametro("@IDProducto", idProducto);
                datos.ejecutarAccion();
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
        public bool existeNombreProducto(string nombre, int idActual = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT IDProducto FROM Producto WHERE Nombre = @Nombre AND Activo = 1");
                datos.setParametro("@Nombre", nombre);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int idEncontrado = (int)datos.Lector["IDProducto"];
                    if (idEncontrado != idActual)
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
        //Mouseherramienta misteriosa especial
        public void actualizarStock(int idProducto, int cantidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Producto SET Stock = Stock + @Cantidad WHERE IDProducto = @IDProducto");
                datos.setParametro("@IDProducto", idProducto);
                datos.setParametro("@Cantidad", cantidad);
                datos.ejecutarAccion();
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
