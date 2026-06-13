
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
            conexion = new SqlConnection("server=localhost; database=NovaHub; integrated security=false; User Id=sa; Password=;");
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

        public int ejecutarAccionScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return int.Parse(comando.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //CREATE PROCEDURE SP_InsertarUsuario
            //    @Nombre VARCHAR(50),
            //    @Apellido VARCHAR(50),
            //    @Email VARCHAR(100),
            //    @Contrasena VARCHAR(50),
            //    @Telefono VARCHAR(20)
            //AS
            //BEGIN
            //    INSERT INTO Usuario(Nombre, Apellido, Email, Contrasena, Telefono, Rol)
            //    VALUES(@Nombre, @Apellido, @Email, @Contrasena, @Telefono, 1)

            //    SELECT scope_identity()
            //END
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
