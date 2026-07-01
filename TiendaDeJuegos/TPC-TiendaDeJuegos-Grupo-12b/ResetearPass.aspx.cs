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
    public partial class ResetearPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["recuperacionIdUsuario"] == null)
            {
                Response.Redirect("~/RecuperarPass.aspx");
            }
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            if (Session["recuperacionIdUsuario"] == null)
            {
                Response.Redirect("~/RecuperarPass.aspx");
                return;
            }

            int idUsuario = (int)Session["recuperacionIdUsuario"];

            if (string.IsNullOrWhiteSpace(txtCodigo.Text) || txtCodigo.Text.Trim().Length != 6)
            {
                MostrarMensaje("Ingresá los 6 dígitos del código.", false);
                return;
            }

            if (txtNuevaPass.Text.Length < 8)
            {
                MostrarMensaje("La contraseña debe tener al menos 8 caracteres.", false);
                return;
            }

            if (txtNuevaPass.Text != txtConfirmarPass.Text)
            {
                MostrarMensaje("Las contraseñas no coinciden.", false);
                return;
            }

            TokenNegocio tokenNegocio = new TokenNegocio();
            bool valido = tokenNegocio.ValidarToken(idUsuario, txtCodigo.Text.Trim(), TipoToken.RecuperacionContrasena);

            if (!valido)
            {
                MostrarMensaje("El código es inválido o expiró. Pedí uno nuevo desde 'Recuperar contraseña'.", false);
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.CambiarContrasena(idUsuario, txtNuevaPass.Text);

            Session.Remove("recuperacionIdUsuario");

            btnRestablecer.Enabled = false;
            lnkReenviar.Enabled = false;
            txtCodigo.Enabled = false;
            txtNuevaPass.Enabled = false;
            txtConfirmarPass.Enabled = false;

            MostrarMensaje("Contraseña actualizada. Ya podés iniciar sesión.", true);
        }

        protected void lnkReenviar_Click(object sender, EventArgs e)
        {
            if (Session["recuperacionIdUsuario"] == null)
            {
                Response.Redirect("~/RecuperarPass.aspx");
                return;
            }

            int idUsuario = (int)Session["recuperacionIdUsuario"];

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.obtenerPorId(idUsuario);

            TokenNegocio tokenNegocio = new TokenNegocio();
            string codigo = tokenNegocio.GenerarToken(idUsuario, TipoToken.RecuperacionContrasena);

            bool ok = EmailService.EnviarCodigoVerificacion(usuario.Email, codigo, out string error);

            MostrarMensaje(ok ? "Te enviamos un nuevo código." : "No pudimos enviar el código: " + error, ok);
        }

        private void MostrarMensaje(string texto, bool ok)
        {
            lblMsj.Text = texto;
            lblMsj.ForeColor = ok ? System.Drawing.Color.LightGreen : System.Drawing.Color.Red;
            lblMsj.Visible = true;
        }
    }
}