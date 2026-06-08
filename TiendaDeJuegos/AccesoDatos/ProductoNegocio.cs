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
    internal class ProductoNegocio
    {
        public List<Producto> listar()
        {
            List<Producto> Lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //datos.setConsulta("SELECT P.Id,P.Codigo,P.Nombre,P.Descripcion,P.Precio,P.IdMarca,P.IdCategoria, C.Descripcion as Categoria,E.Descripcion as Marca FROM dbo.ARTICULOS P LEFT JOIN dbo.MARCAS E ON P.IdMarca = E.Id LEFT JOIN dbo.CATEGORIAS C ON P.IdCategoria = C.Id");
                //datos.ejecutarLectura();
                datos.SetProcedimiento("");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    //AQUI SE CARGAN LOS DATOS
                    Lista.Add(aux);
                }

                datos.cerrarConexion();
                return Lista;
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
