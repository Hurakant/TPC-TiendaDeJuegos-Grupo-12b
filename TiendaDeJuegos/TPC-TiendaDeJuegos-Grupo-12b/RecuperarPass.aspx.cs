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

        // El usuario ingresa su mail
        protected void btnEnviarCodigo_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            {
                MostrarMensajePaso1("Ingresá un correo válido.", false);
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.ObtenerPorEmail(email);

            // No decimos si el email existe o no, para no filtrar esa informacion, si existe generamos el token y mandamos el mail
            if (usuario != null && usuario.Activo)
            {
                TokenNegocio tokenNegocio = new TokenNegocio();
                string codigo = tokenNegocio.GenerarToken(usuario.IdUsuario, TipoToken.RecuperacionContrasena);
                EmailService.EnviarCodigoVerificacion(usuario.Email, codigo, out string error);

                Session["recuperacionIdUsuario"] = usuario.IdUsuario;

                // paso2
                MostrarPaso2();
            }
            else
            {
                MostrarMensajePaso1("Si el correo está registrado, te enviamos un código para restablecer tu contraseña.", true);
            }
        }

        // PASO 2: el usuario ingresa el codigo y la nueva contraseña
        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            if (Session["recuperacionIdUsuario"] == null)
            {
                MostrarMensajePaso2("La sesión de recuperación expiró. Pedí un código nuevo.", false);
                MostrarPaso1();
                return;
            }

            int idUsuario = (int)Session["recuperacionIdUsuario"];

            if (string.IsNullOrWhiteSpace(txtCodigo.Text) || txtCodigo.Text.Trim().Length != 6)
            {
                MostrarMensajePaso2("Ingresá los 6 dígitos del código.", false);
                return;
            }

            if (txtNuevaPass.Text.Length < 8)
            {
                MostrarMensajePaso2("La contraseña debe tener al menos 8 caracteres.", false);
                return;
            }

            if (txtNuevaPass.Text != txtConfirmarPass.Text)
            {
                MostrarMensajePaso2("Las contraseñas no coinciden.", false);
                return;
            }

            TokenNegocio tokenNegocio = new TokenNegocio();
            bool valido = tokenNegocio.ValidarToken(idUsuario, txtCodigo.Text.Trim(), TipoToken.RecuperacionContrasena);

            if (!valido)
            {
                MostrarMensajePaso2("El código es inválido o expiró. Pedí uno nuevo.", false);
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

            MostrarMensajePaso2("Contraseña actualizada. Ya podés iniciar sesión.", true);
        }

        // Esto es para reenviar el codigo
        protected void lnkReenviar_Click(object sender, EventArgs e)
        {
            if (Session["recuperacionIdUsuario"] == null)
            {
                MostrarMensajePaso2("La sesión de recuperación expiró. Pedí un código nuevo.", false);
                MostrarPaso1();
                return;
            }

            int idUsuario = (int)Session["recuperacionIdUsuario"];

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.obtenerPorId(idUsuario);

            TokenNegocio tokenNegocio = new TokenNegocio();
            string codigo = tokenNegocio.GenerarToken(idUsuario, TipoToken.RecuperacionContrasena);

            bool ok = EmailService.EnviarCodigoVerificacion(usuario.Email, codigo, out string error);

            MostrarMensajePaso2(ok ? "Te enviamos un nuevo código." : "No pudimos enviar el código: " + error, ok);
        }

        private void MostrarPaso1()
        {
            pnlPedirEmail.Visible = true;
            pnlResetear.Visible = false;
        }

        private void MostrarPaso2()
        {
            pnlPedirEmail.Visible = false;
            pnlResetear.Visible = true;
        }

        private void MostrarMensajePaso1(string texto, bool ok)
        {
            lblMsjPaso1.Text = texto;
            lblMsjPaso1.ForeColor = ok ? System.Drawing.Color.LightGreen : System.Drawing.Color.Red;
            lblMsjPaso1.Visible = true;
        }

        private void MostrarMensajePaso2(string texto, bool ok)
        {
            lblMsjPaso2.Text = texto;
            lblMsjPaso2.ForeColor = ok ? System.Drawing.Color.LightGreen : System.Drawing.Color.Red;
            lblMsjPaso2.Visible = true;
        }
    }
}