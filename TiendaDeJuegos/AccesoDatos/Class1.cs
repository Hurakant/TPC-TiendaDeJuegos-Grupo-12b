using dominio;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class AccesoDatos
    {
        private SqlConnection conexion;

        public AccesoDatos()
        {
            conexion = new SqlConnection(
                @"server=.\SQLEXPRESS;database=Productos;integrated security=true");
        }

        public SqlConnection ObtenerConexion()
        {
            return conexion;
        }
    }


    public class JuegoNegocio
    {
        public List<Producto> listar()
        {
            List<Producto> lista = new List<Producto>();

            SqlConnection cn = new SqlConnection(
                @"server=.\SQLEXPRESS;database=Productos;integrated security=true");

            cn.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Juegos", cn);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Producto aux = new Producto();

                aux.IdProducto = (int)dr["Id"];
                aux.Nombre = dr["Nombre"].ToString();

                aux.ImagenUrl = dr["Imagen"].ToString();

                lista.Add(aux);
            }

            cn.Close();

            return lista;
        }
    }
}
