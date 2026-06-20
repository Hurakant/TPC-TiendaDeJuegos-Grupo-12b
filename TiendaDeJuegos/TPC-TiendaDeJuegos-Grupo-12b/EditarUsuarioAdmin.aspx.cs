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

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                lblMsjError.Text = "Todos los campos son obligatorios.";
                lblMsjError.Visible = true;
                return;
            }

            // que el mail tenga arroab
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                lblMsjError.Text = "El formato del correo no es valido.";
                lblMsjError.Visible = true;
                return;
            }

            // que el tel tenga solo numeros y qu sean 10 caracteres
            if (txtTelefono.Text.Length != 10 || !txtTelefono.Text.All(char.IsDigit))
            {
                lblMsjError.Text = "El telefono debe contener exactamente 10 dígitos numéricos.";
                lblMsjError.Visible = true;
                return;
            }

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