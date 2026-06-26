using AccesoBD;
using dominio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoNegocio
    {
        //LISTAR (No voy a nombrarlos todos porque tienen nombres descriptivos)
        //POR HACER:
        //expandir Actualizar stock para que sea:
        //Actualizar, Aumentar, Disminuir para no hacerlo de forma arcaica cuando se hagan las compras

        //Metodo para armar lista generica
        private List<Producto> ArmarListaDesdeLector(AccesoDatos datos)
        {
            List<Producto> lista = new List<Producto>();
            Dictionary<int, Producto> porId = new Dictionary<int, Producto>();

            while (datos.Lector.Read())
            {
                Producto aux = new Producto();

                aux.IdProducto = (int)datos.Lector["IDProducto"];
                aux.Nombre = (string)datos.Lector["Nombre"];
                aux.Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"];
                aux.ImagenUrl = datos.Lector["ImagenUrl"] is DBNull ? "" : (string)datos.Lector["ImagenUrl"];
                aux.Precio = (decimal)datos.Lector["Precio"];
                aux.Descuento = (decimal)datos.Lector["Descuento"];
                aux.Stock = (int)datos.Lector["Stock"];
                aux.FechaLanzamiento = datos.Lector["FechaLanzamiento"] is DBNull ? DateTime.MinValue : (DateTime)datos.Lector["FechaLanzamiento"];
                aux.EsDigital = (bool)datos.Lector["EsDigital"];
                aux.Activo = (bool)datos.Lector["Activo"];

                lista.Add(aux);
                porId[aux.IdProducto] = aux;
            }

            if (datos.Lector.NextResult())
            {
                while (datos.Lector.Read())
                {
                    int idProd = (int)datos.Lector["IDProducto"];
                    if (!porId.ContainsKey(idProd)) continue;

                    Categoria cat = new Categoria();
                    cat.IdCategoria = (int)datos.Lector["IDCategoria"];
                    cat.NombreCategoria = (string)datos.Lector["NombreCategoria"];
                    cat.Activo = (bool)datos.Lector["Activo"];

                    porId[idProd].Categoria.Add(cat);
                }
            }

            if (datos.Lector.NextResult())
            {
                while (datos.Lector.Read())
                {
                    int idProd = (int)datos.Lector["IDProducto"];
                    if (!porId.ContainsKey(idProd)) continue;

                    Accesibilidad acc = new Accesibilidad();
                    acc.IdAccesibilidad = (int)datos.Lector["IDAccesibilidad"];
                    acc.NombreAccesibilidad = (string)datos.Lector["NombreAccesibilidad"];
                    acc.Activo = (bool)datos.Lector["Activo"];

                    porId[idProd].Accesibilidad.Add(acc);
                }
            }

            return lista;
        }
        public List<Producto> listar()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("EXEC SP_Producto_Listar");
                datos.ejecutarLectura();
                return ArmarListaDesdeLector(datos);

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
                datos.setConsulta("EXEC SP_Producto_ObtenerPorId @IDProducto = @IDProducto");
                datos.setParametro("@IDProducto", idProducto);
                datos.ejecutarLectura();

                Producto aux = null;

                if (datos.Lector.Read())
                {
                    aux = new Producto();
                    aux.IdProducto = (int)datos.Lector["IDProducto"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"];
                    aux.ImagenUrl = datos.Lector["ImagenUrl"] is DBNull ? "" : (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Descuento = (decimal)datos.Lector["Descuento"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.FechaLanzamiento = datos.Lector["FechaLanzamiento"] is DBNull ? DateTime.MinValue : (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.EsDigital = (bool)datos.Lector["EsDigital"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                }

                if (aux == null)
                    return null;

                if (datos.Lector.NextResult())
                {
                    while (datos.Lector.Read())
                    {
                        Categoria cat = new Categoria();
                        cat.IdCategoria = (int)datos.Lector["IDCategoria"];
                        cat.NombreCategoria = (string)datos.Lector["NombreCategoria"];
                        cat.Activo = (bool)datos.Lector["Activo"];
                        aux.Categoria.Add(cat);
                    }
                }

                if (datos.Lector.NextResult())
                {
                    while (datos.Lector.Read())
                    {
                        Accesibilidad acc = new Accesibilidad();
                        acc.IdAccesibilidad = (int)datos.Lector["IDAccesibilidad"];
                        acc.NombreAccesibilidad = (string)datos.Lector["NombreAccesibilidad"];
                        acc.Activo = (bool)datos.Lector["Activo"];
                        aux.Accesibilidad.Add(acc);
                    }
                }

                return aux;
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
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string idsCsv = (idsCategorias != null && idsCategorias.Count > 0)
                    ? string.Join(",", idsCategorias)
                    : null;

                datos.setConsulta("EXEC SP_Producto_ListarFiltrado @Texto = @Texto, @IdsCategorias = @IdsCategorias, @Orden = @Orden");
                datos.setParametro("@Texto", string.IsNullOrWhiteSpace(texto) ? (object)DBNull.Value : texto.Trim());
                datos.setParametro("@IdsCategorias", idsCsv == null ? (object)DBNull.Value : idsCsv);
                datos.setParametro("@Orden", orden);
                datos.ejecutarLectura();

                return ArmarListaDesdeLector(datos);
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
            int idNuevo = 0;
            try
            {
                datos.setConsulta(
                    "EXEC SP_Producto_Agregar @Nombre = @Nombre, @Descripcion = @Descripcion, @Precio = @Precio, " +
                    "@Descuento = @Descuento, @Stock = @Stock, @EsDigital = @EsDigital, @ImagenUrl = @ImagenUrl, " +
                    "@FechaLanzamiento = @FechaLanzamiento");
                datos.setParametro("@Nombre", nuevo.Nombre);
                datos.setParametro("@Descripcion", nuevo.Descripcion ?? "");
                datos.setParametro("@Precio", nuevo.Precio);
                datos.setParametro("@Descuento", nuevo.Descuento);
                datos.setParametro("@Stock", nuevo.Stock);
                datos.setParametro("@EsDigital", nuevo.EsDigital);
                datos.setParametro("@ImagenUrl", nuevo.ImagenUrl ?? "");
                datos.setParametro("@FechaLanzamiento", nuevo.FechaLanzamiento == default(DateTime) ? (object)DBNull.Value : nuevo.FechaLanzamiento);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    idNuevo = (int)datos.Lector[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            //Foreach para categoria y accesibilidad, cada una se inserta en una llamada separada
            foreach (Categoria cat in nuevo.Categoria)
                AgregarCategoriaAProducto(idNuevo, cat.IdCategoria);

            foreach (Accesibilidad acc in nuevo.Accesibilidad)
                AgregarAccesibilidadAProducto(idNuevo, acc.IdAccesibilidad);

            nuevo.IdProducto = idNuevo;
        }

        public void modificar(Producto prod)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(
                    "EXEC SP_Producto_Modificar @IDProducto = @IDProducto, @Nombre = @Nombre, @Descripcion = @Descripcion, " +
                    "@Precio = @Precio, @Descuento = @Descuento, @Stock = @Stock, @EsDigital = @EsDigital, " +
                    "@Activo = @Activo, @ImagenUrl = @ImagenUrl");
                datos.setParametro("@IDProducto", prod.IdProducto);
                datos.setParametro("@Nombre", prod.Nombre);
                datos.setParametro("@Descripcion", prod.Descripcion ?? "");
                datos.setParametro("@Precio", prod.Precio);
                datos.setParametro("@Descuento", prod.Descuento);
                datos.setParametro("@Stock", prod.Stock);
                datos.setParametro("@EsDigital", prod.EsDigital);
                datos.setParametro("@Activo", prod.Activo);
                datos.setParametro("@ImagenUrl", prod.ImagenUrl ?? "");
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

            // Relaciones muchos a muchos: se borran todas las anteriores y se
            // vuelven a insertar las actuales (mas simple que comparar diferencias)
            EliminarCategoriasDeProducto(prod.IdProducto);
            foreach (Categoria cat in prod.Categoria)
                AgregarCategoriaAProducto(prod.IdProducto, cat.IdCategoria);

            EliminarAccesibilidadesDeProducto(prod.IdProducto);
            foreach (Accesibilidad acc in prod.Accesibilidad)
                AgregarAccesibilidadAProducto(prod.IdProducto, acc.IdAccesibilidad);
        }

        public void eliminar(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("EXEC SP_Producto_Eliminar @IDProducto = @IDProducto");
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
                datos.setConsulta("EXEC SP_Producto_ExisteNombre @Nombre = @Nombre, @IdActual = @IdActual");
                datos.setParametro("@Nombre", nombre);
                datos.setParametro("@IdActual", idActual);
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
        //Mouseherramienta misteriosa especial
        public void actualizarStock(int idProducto, int cantidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("EXEC SP_Producto_ActualizarStock @IDProducto = @IDProducto, @Cantidad = @Cantidad");
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
        //Helpers para categoria y accesibilidad

        private void AgregarCategoriaAProducto(int idProducto, int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("EXEC SP_ProductoCategoria_Agregar @IDProducto = @IDProducto, @IDCategoria = @IDCategoria");
                datos.setParametro("@IDProducto", idProducto);
                datos.setParametro("@IDCategoria", idCategoria);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void EliminarCategoriasDeProducto(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("EXEC SP_ProductoCategoria_EliminarPorProducto @IDProducto = @IDProducto");
                datos.setParametro("@IDProducto", idProducto);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void AgregarAccesibilidadAProducto(int idProducto, int idAccesibilidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("EXEC SP_ProductoAccesibilidad_Agregar @IDProducto = @IDProducto, @IDAccesibilidad = @IDAccesibilidad");
                datos.setParametro("@IDProducto", idProducto);
                datos.setParametro("@IDAccesibilidad", idAccesibilidad);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void EliminarAccesibilidadesDeProducto(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("EXEC SP_ProductoAccesibilidad_EliminarPorProducto @IDProducto = @IDProducto");
                datos.setParametro("@IDProducto", idProducto);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
