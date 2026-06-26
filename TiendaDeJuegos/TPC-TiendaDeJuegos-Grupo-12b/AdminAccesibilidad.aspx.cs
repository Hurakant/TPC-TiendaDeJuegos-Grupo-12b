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
    public partial class AdminAccesibilidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (user.Rol != Rol.Admin)
            {
                Session["ErrorNoPermisos"] = true;
                Response.Redirect("Error.aspx");
            }
            if (!IsPostBack)
            {
                hfSelectedId.Value = "";
                pnlEdicion.Visible = false;
                pnlSinSeleccion.Visible = true;
                CargarAccesibilidades();
            }
        }

        private void CargarAccesibilidades()
        {
            try
            {
                string filtro = txtBuscar.Text.Trim();
                AccesibilidadNegocio negocio = new AccesibilidadNegocio();
                List<Accesibilidad> lista = negocio.listar(filtro, true);

                rptAccesibilidades.DataSource = lista;
                rptAccesibilidades.DataBind();

                pnlVacio.Visible = lista.Count == 0;

                if (!string.IsNullOrEmpty(hfSelectedId.Value))
                    CargarEnPanelEdicion(int.Parse(hfSelectedId.Value));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar: " + ex.Message;
                lblMensaje.CssClass = "admin-acc-msg-err";
            }
        }

        private void CargarEnPanelEdicion(int idAccesibilidad)
        {
            try
            {
                Accesibilidad acc = new AccesibilidadNegocio().obtenerPorId(idAccesibilidad);
                if (acc == null) { LimpiarSeleccion(); return; }

                lblIdEditar.Text = acc.IdAccesibilidad.ToString();
                txtNombreEditar.Text = acc.NombreAccesibilidad;
                pnlSinSeleccion.Visible = false;
                pnlEdicion.Visible = true;
                lblMensaje.Text = "";
                lblMensaje.CssClass = "";

                bool activa = acc.Activo;
                txtNombreEditar.Enabled = activa;
                btnGuardar.Enabled = activa;
                btnEliminar.Enabled = activa;

                if (!activa)
                {
                    lblMensaje.Text = "Accesibilidad inactiva. No se puede editar ni eliminar.";
                    lblMensaje.CssClass = "admin-acc-msg-err";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "admin-acc-msg-err";
            }
        }

        protected void rptAccesibilidades_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                hfSelectedId.Value = id.ToString();
                CargarEnPanelEdicion(id);
                CargarAccesibilidades();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarAccesibilidades();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            LimpiarSeleccion();
            pnlAlta.Visible = true;
        }

        protected void btnCancelarNueva_Click(object sender, EventArgs e)
        {
            txtNombreNueva.Text = "";
            lblMensajeAlta.Text = "";
            pnlAlta.Visible = false;
        }

        protected void btnGuardarNueva_Click(object sender, EventArgs e)
        {
            lblMensajeAlta.Text = "";
            string nombre = txtNombreNueva.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                lblMensajeAlta.Text = "El nombre no puede estar vacío.";
                return;
            }

            try
            {
                AccesibilidadNegocio negocio = new AccesibilidadNegocio();

                if (negocio.existeNombre(nombre))
                {
                    lblMensajeAlta.Text = "Ya existe una accesibilidad activa con ese nombre.";
                    return;
                }

                int idNuevo = negocio.agregar(nombre);

                txtNombreNueva.Text = "";
                pnlAlta.Visible = false;

                CargarAccesibilidades();

                // Seleccionar la recien creada para que el usuario pueda editarla inmediatamente
                hfSelectedId.Value = idNuevo.ToString();
                CargarEnPanelEdicion(idNuevo);
                CargarAccesibilidades();
            }
            catch (Exception ex)
            {
                lblMensajeAlta.Text = "Error: " + ex.Message;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfSelectedId.Value)) return;

            string nombre = txtNombreEditar.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                lblMensaje.Text = "El nombre no puede estar vacío.";
                lblMensaje.CssClass = "admin-acc-msg-err";
                return;
            }

            int id = int.Parse(hfSelectedId.Value);
            AccesibilidadNegocio negocio = new AccesibilidadNegocio();

            if (negocio.existeNombre(nombre, id))
            {
                lblMensaje.Text = "Ya existe otra accesibilidad activa con ese nombre.";
                lblMensaje.CssClass = "admin-acc-msg-err";
                return;
            }

            try
            {
                negocio.modificar(id, nombre);
                lblMensaje.Text = "✔ Cambios guardados.";
                lblMensaje.CssClass = "admin-acc-msg-ok";
                CargarAccesibilidades();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar: " + ex.Message;
                lblMensaje.CssClass = "admin-acc-msg-err";
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfSelectedId.Value)) return;

            int id = int.Parse(hfSelectedId.Value);
            try
            {
                new AccesibilidadNegocio().eliminar(id);
                LimpiarSeleccion();
                CargarAccesibilidades();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar: " + ex.Message;
                lblMensaje.CssClass = "admin-acc-msg-err";
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarSeleccion();
            CargarAccesibilidades();
        }

        private void LimpiarSeleccion()
        {
            hfSelectedId.Value = "";
            txtNombreEditar.Text = "";
            lblIdEditar.Text = "";
            pnlEdicion.Visible = false;
            pnlSinSeleccion.Visible = true;
            lblMensaje.Text = "";
            lblMensaje.CssClass = "";
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}