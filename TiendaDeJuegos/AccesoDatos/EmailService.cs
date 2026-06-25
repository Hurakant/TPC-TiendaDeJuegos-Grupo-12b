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
    }
}
