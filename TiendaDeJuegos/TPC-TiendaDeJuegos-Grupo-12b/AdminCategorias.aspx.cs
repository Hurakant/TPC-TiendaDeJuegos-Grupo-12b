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
    public partial class AdminCategorias : System.Web.UI.Page
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
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            try
            {
                string filtro = txtBuscar.Text.Trim();
                CategoriaNegocio negocio = new CategoriaNegocio();
                List<Categoria> lista = negocio.listar(filtro, true);

                rptCategorias.DataSource = lista;
                rptCategorias.DataBind();

                pnlVacio.Visible = lista.Count == 0;

                // Si hay una seleccionada, reaplicar el panel de edicion
                if (!string.IsNullOrEmpty(hfSelectedId.Value))
                    CargarEnPanelEdicion(int.Parse(hfSelectedId.Value));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar: " + ex.Message;
                lblMensaje.CssClass = "admin-cat-msg-err";
            }
        }

        private void CargarEnPanelEdicion(int idCategoria)
        {
            try
            {
                Categoria cat = new CategoriaNegocio().obtenerPorId(idCategoria);
                if (cat == null) { LimpiarSeleccion(); return; }

                lblIdEditar.Text = cat.IdCategoria.ToString();
                txtNombreEditar.Text = cat.NombreCategoria;
                pnlSinSeleccion.Visible = false;
                pnlEdicion.Visible = true;
                lblMensaje.Text = "";
                lblMensaje.CssClass = "";

                bool activa = cat.Activo;
                txtNombreEditar.Enabled = activa;
                btnGuardar.Enabled = activa;
                btnEliminar.Enabled = activa;

                if (!activa)
                {
                    lblMensaje.Text = "Categoría inactiva. No se puede editar ni eliminar.";
                    lblMensaje.CssClass = "admin-cat-msg-err";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "admin-cat-msg-err";
            }
        }

        protected void rptCategorias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                hfSelectedId.Value = id.ToString();
                CargarEnPanelEdicion(id);
                CargarCategorias(); // Para refrescar la marca .selected en la card
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarCategorias();
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
                CategoriaNegocio negocio = new CategoriaNegocio();

                if (negocio.existeNombre(nombre))
                {
                    lblMensajeAlta.Text = "Ya existe una categoría activa con ese nombre.";
                    return;
                }

                int idNuevo = negocio.agregar(nombre);

                txtNombreNueva.Text = "";
                pnlAlta.Visible = false;

                CargarCategorias();

                // Seleccionar la recien creada para que el usuario pueda editarla inmediatamente
                hfSelectedId.Value = idNuevo.ToString();
                CargarEnPanelEdicion(idNuevo);
                CargarCategorias();
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
                lblMensaje.CssClass = "admin-cat-msg-err";
                return;
            }

            int id = int.Parse(hfSelectedId.Value);
            CategoriaNegocio negocio = new CategoriaNegocio();

            if (negocio.existeNombre(nombre, id))
            {
                lblMensaje.Text = "Ya existe otra categoría activa con ese nombre.";
                lblMensaje.CssClass = "admin-cat-msg-err";
                return;
            }

            try
            {
                negocio.modificar(id, nombre);
                lblMensaje.Text = "✔ Cambios guardados.";
                lblMensaje.CssClass = "admin-cat-msg-ok";
                CargarCategorias();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar: " + ex.Message;
                lblMensaje.CssClass = "admin-cat-msg-err";
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfSelectedId.Value)) return;

            int id = int.Parse(hfSelectedId.Value);
            try
            {
                new CategoriaNegocio().eliminar(id);
                LimpiarSeleccion();
                CargarCategorias();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar: " + ex.Message;
                lblMensaje.CssClass = "admin-cat-msg-err";
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarSeleccion();
            CargarCategorias();
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