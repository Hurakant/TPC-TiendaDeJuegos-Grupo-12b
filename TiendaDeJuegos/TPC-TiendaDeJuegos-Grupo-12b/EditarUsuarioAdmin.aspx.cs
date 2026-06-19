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
    public partial class EditarUsuarioAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarRoles();
                cargarUsuario();
            }
        }

        private void cargarUsuario()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario user = negocio.obtenerPorId(id);

            txtNombre.Text = user.Nombre;
            txtApellido.Text = user.Apellido;
            txtEmail.Text = user.Email;
            txtTelefono.Text = user.Telefono;

            ddlRol.SelectedValue = user.Rol.ToString();
        }

        private void cargarRoles()
        {
            ddlRol.DataSource = Enum.GetValues(typeof(Rol));
            ddlRol.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
        }

    }
}