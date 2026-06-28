using AccesoBD;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    // Este token servirá para verificar y recuperar contraseña más adelante
    public class TokenNegocio
    {
        private const int MinutosValidez = 15;

        //Aqui solo puede haber un mismo tipo de token por usuario para evitar cosas malas
        public string GenerarToken(int idUsuario, TipoToken tipo)
        {
            string codigo = new Random().Next(0, 1000000).ToString("D6");

            EliminarTokenExistente(idUsuario, tipo);

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"INSERT INTO Token (IDUsuario, Codigo, Tipo, FechaCreacion, FechaVencimiento)
                                     VALUES (@idUsuario, @codigo, @tipo, @fechaCreacion, @fechaVencimiento)");
                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@codigo", codigo);
                datos.setParametro("@tipo", (int)tipo);
                datos.setParametro("@fechaCreacion", DateTime.Now);
                datos.setParametro("@fechaVencimiento", DateTime.Now.AddMinutes(MinutosValidez));

                datos.ejecutarAccion();

                return codigo;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //Valida el código contra la BD: si el tipo es VerificacionCorreo, marca al usuario como verificado
        //Los tokens hay que eliminarlos despues de usarlos
        public bool ValidarToken(int idUsuario, string codigo, TipoToken tipo)
        {
            bool esValido = ExisteTokenVigente(idUsuario, codigo, tipo);

            if (!esValido)
            {
                return false;
            }

            if (tipo == TipoToken.VerificacionCorreo)
            {
                MarcarCorreoVerificado(idUsuario);
            }

            EliminarTokenExistente(idUsuario, tipo);

            return true;
        }

        private bool ExisteTokenVigente(int idUsuario, string codigo, TipoToken tipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"SELECT IDToken FROM Token
                                     WHERE IDUsuario = @idUsuario AND Codigo = @codigo
                                     AND Tipo = @tipo AND FechaVencimiento > @ahora");
                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@codigo", codigo);
                datos.setParametro("@tipo", (int)tipo);
                datos.setParametro("@ahora", DateTime.Now);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void MarcarCorreoVerificado(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Usuario SET CorreoVerificado = 1 WHERE IdUsuario = @idUsuario");
                datos.setParametro("@idUsuario", idUsuario);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void EliminarTokenExistente(int idUsuario, TipoToken tipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("DELETE FROM Token WHERE IDUsuario = @idUsuario AND Tipo = @tipo");
                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@tipo", (int)tipo);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
