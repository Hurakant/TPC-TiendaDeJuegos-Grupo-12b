using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Negocio
{
    public static class EmailService
    {
        public static bool Enviar(string destino, string asunto, string cuerpo, out string error)
        {
            error = null;

            try
            {
                int puerto;
                string host = ConfigurationManager.AppSettings["Mailtrap.Host"];
                string usuario = ConfigurationManager.AppSettings["Mailtrap.Usuario"];
                string from = ConfigurationManager.AppSettings["Mailtrap.From"];
                if (!int.TryParse(ConfigurationManager.AppSettings["Mailtrap.Puerto"], out puerto))
                {
                    puerto = 587;
                }

                //string token = Environment.GetEnvironmentVariable("MAILTRAP_API_TOKEN");
                //Puse el token aqui porque no habia otra forma D:
                string token = "a48b223e95a80c5ea3f4739a6fba24e5";
                if (string.IsNullOrWhiteSpace(token))
                {
                    error = "Falta la variable de entorno MAILTRAP_API_TOKEN.";
                    return false;
                }


                using (var client = new SmtpClient(host, puerto)
                {
                    Credentials = new NetworkCredential(usuario, token),
                    EnableSsl = true
                })
                {
                    client.Send(from, destino, asunto, cuerpo);
                }

                return true;
            }
            catch (Exception ex)
            {

                error = ex.Message;
                return false;
            }
        }
        //Mensaje de bienvenida
        public static bool EnviarBienvenida(string destino, string nombre, string apellido, out string error)
        {
            string asunto = "¡Bienvenido a NovaHub!";
            string cuerpo = $"Hola {nombre} {apellido},\n\n" +
                            "¡Gracias por registrarte en NovaHub! Ya casi estás listo para empezar, " +
                            "solo falta que verifiques tu correo electrónico con el código que te enviamos " +
                            "en otro mensaje.\n\nSaludos,\nEl equipo de NovaHub";

            return Enviar(destino, asunto, cuerpo, out error);
        }

        //Mail con el código token
        public static bool EnviarCodigoVerificacion(string destino, string codigo, out string error)
        {
            string asunto = "Tu código de verificación - NovaHub";
            string cuerpo = $"Tu código de verificación es: {codigo}\n\n" +
                            "Este código vence en 15 minutos. Si no solicitaste este código, " +
                            "podés ignorar este mensaje.";

            return Enviar(destino, asunto, cuerpo, out error);
        }
    }
}
