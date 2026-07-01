using dominio;
using Dominio;
using Negocio;
using System;
using System.Web.UI;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class RecuperarPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            {
                MostrarMensaje("Ingresá un correo válido.", false);
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.ObtenerPorEmail(email);

            //verificamos que el usuario no sea nulo y enviamos
            if (usuario != null && usuario.Activo)
            {
                TokenNegocio tokenNegocio = new TokenNegocio();
                string codigo = tokenNegocio.GenerarToken(usuario.IdUsuario, TipoToken.RecuperacionContrasena);
                EmailService.EnviarCodigoVerificacion(usuario.Email, codigo, out string error);

                Session["recuperacionIdUsuario"] = usuario.IdUsuario;

                Response.Redirect("~/ResetearPass.aspx", false);
                return;
            }

            MostrarMensaje("Si el correo está registrado, te enviamos un código para restablecer tu contraseña.", true);
        }

        private void MostrarMensaje(string texto, bool ok)
        {
            lblMsj.Text = texto;
            lblMsj.ForeColor = ok ? System.Drawing.Color.LightGreen : System.Drawing.Color.Red;
            lblMsj.Visible = true;
        }

    }
}