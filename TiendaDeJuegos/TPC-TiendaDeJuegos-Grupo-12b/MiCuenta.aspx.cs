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
    public partial class MiCuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                Usuario user = (Usuario)Session["usuarioLogueado"];

                lblNombre.Text = user.Nombre;
                lblApellido.Text = user.Apellido;
                lblEmail.Text = user.Email;
                lblTelefono.Text = user.Telefono;

                txtNombre.Text = user.Nombre;
                txtApellido.Text = user.Apellido;
                txtEmail.Text = user.Email;
                txtTelefono.Text = user.Telefono;

                Info.Visible = true;
                Editar.Visible = false;

            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Home.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
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

                Usuario usuario = (Usuario)Session["usuarioLogueado"];
                UsuarioNegocio negocio = new UsuarioNegocio();

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Email = txtEmail.Text;
                usuario.Telefono = txtTelefono.Text;

                negocio.ModificarUser(usuario);

                lblNombre.Text = usuario.Nombre;
                lblApellido.Text = usuario.Apellido;
                lblEmail.Text = usuario.Email;
                lblTelefono.Text = usuario.Telefono;

                Info.Visible = true;
                Editar.Visible = false;

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];

            txtNombre.Text = user.Nombre;
            txtApellido.Text = user.Apellido;
            txtEmail.Text = user.Email;
            txtTelefono.Text = user.Telefono;

            Info.Visible = false;
            Editar.Visible = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];

            txtNombre.Text = user.Nombre;
            txtApellido.Text = user.Apellido;
            txtEmail.Text = user.Email;
            txtTelefono.Text = user.Telefono;

            Info.Visible = true;
            Editar.Visible = false;
        }
    }
}