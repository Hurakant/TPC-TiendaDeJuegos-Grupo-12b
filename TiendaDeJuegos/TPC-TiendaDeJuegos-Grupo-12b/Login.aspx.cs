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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuarioLogueado"] != null)
            {
                Response.Redirect("~/Home.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario;
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                if (string.IsNullOrWhiteSpace(TxtUser.Text) ||  string.IsNullOrWhiteSpace(TxtPass.Text))
                {
                    lblError.Text = "Todos los campos son obligatorios.";
                    lblError.Visible = true;
                    return;
                }

                if (!TxtUser.Text.Contains("@") || !TxtUser.Text.Contains("."))
                {
                    lblError.Text = "El formato del correo no es valido.";
                    lblError.Visible = true;
                    return;
                }

                if (TxtPass.Text.Length < 8)
                {
                    lblError.Text = "La contraseña debe tener al menos 8 caracteres.";
                    lblError.Visible = true;
                    return;
                }

                usuario = new Usuario();

                usuario.Email = TxtUser.Text;
                usuario.Contraseña = TxtPass.Text;

                if (negocio.Loguear(usuario))
                {
                    //Si todavía no verificó el correo, lo mandamos a verificar su correo
                    if (!usuario.CorreoVerificado)
                    {
                        TokenNegocio tokenNegocio = new TokenNegocio();
                        string codigo = tokenNegocio.GenerarToken(usuario.IdUsuario, TipoToken.VerificacionCorreo);
                        EmailService.EnviarCodigoVerificacion(usuario.Email, codigo, out string errorEnvio);

                        Session.Add("usuarioPendienteVerificacion", usuario);
                        Response.Redirect("~/VerificacionCorreo.aspx", false);
                        return;
                    }
                    Session.Add("usuarioLogueado", usuario);
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Email o contraseña incorrectos, vuelva a intentar!";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}