using dominio;
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
                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

                usuario.Email = TxtEmail.Text;
                usuario.Nombre = TxtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Telefono = TxtTelefono.Text;
                usuario.Contraseña = txtPass.Text;
                usuario.Activo = true;

                int id = negocio.RegistroUser(usuario);
                Session.Add("usuarioLogueado", usuario);

                Response.Redirect("~/Home.aspx",false);
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }

            
        }
    }
}