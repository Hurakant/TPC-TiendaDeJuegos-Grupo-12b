using System.Collections.Generic;
using AccesoBD;
using dominio;

namespace Negocio
{
    public class FormaDePagoNegocio
    {
        public List<FormaDePago> Listar()
        {
            List<FormaDePago> lista = new List<FormaDePago>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IDFormaDePago, Nombre FROM FormaDePago");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaDePago fp = new FormaDePago();

                    fp.IdFormaDePago = (int)datos.Lector["IDFormaDePago"];
                    fp.Nombre = datos.Lector["Nombre"].ToString();

                    lista.Add(fp);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}