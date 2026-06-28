using dominio;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class VerificacionCorreo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["usuarioPendienteVerificacion"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuarioPendienteVerificacion"];

            if (usuario == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCodigo.Text) || txtCodigo.Text.Trim().Length != 6)
            {
                MostrarMensaje("Ingresá los 6 dígitos del código.", false);
                return;
            }

            TokenNegocio tokenNegocio = new TokenNegocio();
            bool valido = tokenNegocio.ValidarToken(usuario.IdUsuario, txtCodigo.Text.Trim(), TipoToken.VerificacionCorreo);

            if (valido)
            {
                usuario.CorreoVerificado = true;
                Session.Remove("usuarioPendienteVerificacion");
                Session.Add("usuarioLogueado", usuario);
                Response.Redirect("~/Home.aspx", false);
            }
            else
            {
                MostrarMensaje("El código es invlido o expiró. Pedí uno nuevo.", false);
            }
        }
        //Boton re reenviar

        protected void lnkReenviar_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuarioPendienteVerificacion"];

            if (usuario == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            TokenNegocio tokenNegocio = new TokenNegocio();
            string codigo = tokenNegocio.GenerarToken(usuario.IdUsuario, TipoToken.VerificacionCorreo);

            bool seEnvio = EmailService.EnviarCodigoVerificacion(usuario.Email, codigo, out string error);

            MostrarMensaje(seEnvio ? "Te enviamos un nuevo codigo." : "No pudimos enviar el codigo: " + error, seEnvio);
        }
        private void MostrarMensaje(string texto, bool seEnvio)
        {
            lblMsj.Text = texto;
            lblMsj.ForeColor = seEnvio ? System.Drawing.Color.LightGreen : System.Drawing.Color.Red;
            lblMsj.Visible = true;
        }
    }
}