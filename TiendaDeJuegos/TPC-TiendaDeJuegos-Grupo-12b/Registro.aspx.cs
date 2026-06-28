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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtEmail.Text) || string.IsNullOrWhiteSpace(TxtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(TxtTelefono.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
                {
                    lblMsjError.Text = "Todos los campos son obligatorios.";
                    lblMsjError.Visible = true;
                    return;
                }

                // que el mail tenga arroab
                if (!TxtEmail.Text.Contains("@") || !TxtEmail.Text.Contains("."))
                {
                    lblMsjError.Text = "El formato del correo no es valido.";
                    lblMsjError.Visible = true;
                    return;
                }

                // que el tel tenga solo numeros y qu sean 10 caracteres
                if (TxtTelefono.Text.Length != 10 || !TxtTelefono.Text.All(char.IsDigit))
                {
                    lblMsjError.Text = "El telefono debe contener exactamente 10 dígitos numéricos.";
                    lblMsjError.Visible = true;
                    return;
                }

                // pass con 8 caracteres
                if (txtPass.Text.Length < 8)
                {
                    lblMsjError.Text = "La contraseña debe tener al menos 8 caracteres.";
                    lblMsjError.Visible = true;
                    return;
                }

                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

                usuario.Email = TxtEmail.Text;
                usuario.Nombre = TxtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Telefono = TxtTelefono.Text;
                usuario.Contraseña = txtPass.Text;
                usuario.Activo = true;

                int id = negocio.RegistroUser(usuario);
                usuario.IdUsuario = id;

                EmailService.EnviarBienvenida(usuario.Email, usuario.Nombre, usuario.Apellido, out string errorBienvenida);

                //Prueba de forzar login

                //TokenNegocio tokenNegocio = new TokenNegocio();
                //string codigo = tokenNegocio.GenerarToken(usuario.IdUsuario, TipoToken.VerificacionCorreo);
                //EmailService.EnviarCodigoVerificacion(usuario.Email, codigo, out string errorCodigo);

                //Session.Add("usuarioPendienteVerificacion", usuario);

                Response.Redirect("~/Login.aspx", false);
            }
            catch (Exception ex)
            {
                lblMsjError.Text = "Ocurrio un error al procesar el registro. Intentelo de nuevo." + ex.ToString();
                lblMsjError.Visible = true;
            }
            
        }
    }
}