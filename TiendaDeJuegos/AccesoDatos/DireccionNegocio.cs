using AccesoBD;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DireccionNegocio
    {

        public void AgregarDireccion(Direccion nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO Direccion (IDUsuario, Calle, Numero, Piso, Depto, Localidad, Provincia, CodigoPostal) VALUES (@idUsuario, @calle, @numero, @piso, @depto, @localidad, @provincia, @codigoPostal)");

                datos.setParametro("@idUsuario", nueva.IDUsuario);
                datos.setParametro("@calle", nueva.Calle);
                datos.setParametro("@numero", nueva.Numero);
                datos.setParametro("@piso", nueva.Piso != null ? nueva.Piso : "");
                datos.setParametro("@depto", nueva.Depto != null ? nueva.Depto : "");
                datos.setParametro("@localidad", nueva.Localidad);
                datos.setParametro("@provincia", nueva.Provincia);
                datos.setParametro("@codigoPostal", nueva.CodigoPostal);

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


        public List<Direccion> ListarPorUsuario(int idUsuario)
        {
            List<Direccion> lista = new List<Direccion>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IDDireccion, IDUsuario, Calle, Numero, Piso, Depto, Localidad, Provincia, CodigoPostal, Activo FROM Direccion WHERE IDUsuario = @idUsuario AND Activo = 1");
                datos.setParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Direccion aux = new Direccion();
                    aux.IDDireccion = (int)datos.Lector["IDDireccion"];
                    aux.IDUsuario = (int)datos.Lector["IDUsuario"];
                    aux.Calle = (string)datos.Lector["Calle"];
                    aux.Numero = (string)datos.Lector["Numero"];

                    aux.Piso = datos.Lector["Piso"].ToString();
                    aux.Depto = datos.Lector["Depto"].ToString();

                    aux.Localidad = (string)datos.Lector["Localidad"];
                    aux.Provincia = (string)datos.Lector["Provincia"];
                    aux.CodigoPostal = (string)datos.Lector["CodigoPostal"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(aux);
                }

                return lista;
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

        public void EliminarDireccion(int idDireccion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Direccion SET Activo = 0 WHERE IDDireccion = @idDireccion");
                datos.setParametro("@idDireccion", idDireccion);
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
