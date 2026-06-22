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

                DireccionCard.Visible = true;
                AgregarDire.Visible = false;

                CargarGridDirecciones();

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


        // direccion
        protected void dgvDirecciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarDir")
            {
                try
                {
                    int idDireccion = Convert.ToInt32(e.CommandArgument);

                    DireccionNegocio negocio = new DireccionNegocio();

                    negocio.EliminarDireccion(idDireccion);
                    CargarGridDirecciones();
                }
                catch (Exception ex)
                {
                    lblMsjDireccion.Text = "No se pudo eliminar la dirección: " + ex.Message;
                    lblMsjDireccion.Visible = true;
                }
            }
        }

        private void CargarGridDirecciones()
        {

            try
            {
                Usuario user = (Usuario)Session["usuarioLogueado"];
                DireccionNegocio negocio = new DireccionNegocio();

                List<Direccion> listaDirecciones = negocio.ListarPorUsuario(user.IdUsuario);

                dgvDirecciones.DataSource = listaDirecciones;
                dgvDirecciones.DataBind();

                if (listaDirecciones != null && listaDirecciones.Count >= 3)
                {
                    btnAgregarDireccion.Enabled = false;
                    lblMsjDireccion.Text = "Alcanzaste el límite de 3 direcciones. Borrá una para agregar otra.";
                    lblMsjDireccion.Visible = true;
                }
                else
                {
                    btnAgregarDireccion.Enabled = true;
                    lblMsjDireccion.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMsjDireccion.Text = "Error al cargar las direcciones: " + ex.Message;
                lblMsjDireccion.Visible = true;
            }
        }

        protected void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            AgregarDire.Visible = true;
            DireccionCard.Visible = false;
        }

        protected void btnGuardarDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCalle.Text) || string.IsNullOrWhiteSpace(txtNumero.Text) || string.IsNullOrWhiteSpace(txtLocalidad.Text) || string.IsNullOrWhiteSpace(txtProvincia.Text) || string.IsNullOrWhiteSpace(txtCodigoPostal.Text))
                {
                    lblErrorDireccion.Text = "Todos los campos son obligatorios.";
                    lblErrorDireccion.Visible = true;
                    return;
                }

                Usuario usuario = (Usuario)Session["usuarioLogueado"];

                Direccion direccion = new Direccion();

                direccion.IDUsuario = usuario.IdUsuario;
                direccion.Calle = txtCalle.Text.Trim();
                direccion.Numero = txtNumero.Text.Trim();

                if (!string.IsNullOrWhiteSpace(txtPiso.Text))
                {
                    direccion.Piso = txtPiso.Text.Trim();
                }

                if (!string.IsNullOrWhiteSpace(txtDepto.Text))
                {
                    direccion.Depto = txtDepto.Text.Trim();
                }

                direccion.Localidad = txtLocalidad.Text.Trim();
                direccion.Provincia = txtProvincia.Text.Trim();
                direccion.CodigoPostal = txtCodigoPostal.Text.Trim();

                DireccionNegocio negocio = new DireccionNegocio();
                negocio.AgregarDireccion(direccion);

                lblErrorDireccion.Visible = false;

                AgregarDire.Visible = false;
                DireccionCard.Visible = true;

                CargarGridDirecciones();
                LimpiarCamposDireccion();
            }
            catch (Exception ex)
            {
                lblErrorDireccion.Text = ex.Message;
                lblErrorDireccion.Visible = true;
            }
        }

        private void LimpiarCamposDireccion()
        {
            txtCalle.Text = "";
            txtNumero.Text = "";
            txtPiso.Text = "";
            txtDepto.Text = "";
            txtLocalidad.Text = "";
            txtProvincia.Text = "";
            txtCodigoPostal.Text = "";
        }

        protected void btnCancelarDireccion_Click(object sender, EventArgs e)
        {
            AgregarDire.Visible = false;
            DireccionCard.Visible = true;
        }
    }
}