
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoBD
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        //Arreglen esto porfa
        public SqlDataReader Lector
        {
            get { return lector; }
        }
        public AccesoDatos()
        {
            //Conexion a la base de datos, aqui use la mia pero cambienla cuando la vayan a usar, o usamos env path
            //server=.\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true;
            //server=localhost; database=NovaHub; integrated security=false; User Id=sa; Password=;
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=NovaHub; integrated security=true;");
            comando = new SqlCommand();
        }
        //Esto es para colocar la consulta
        public void SetProcedimiento(string sp)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }
        public void setConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void setParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Cerrar conexion
        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }


    }
}
