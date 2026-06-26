using System;
using Negocio;
using dominio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class AdminUsuarios : System.Web.UI.Page
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
                cargarUsuarios();
            }
        }

        private void cargarUsuarios()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            bool? estado = null;
            if (ddlEstado.SelectedValue == "1")
            {
                estado = true;
            }
            else if (ddlEstado.SelectedValue == "0")
            {
                estado = false;
            }

            Rol? rol = null;
            if (!string.IsNullOrEmpty(ddlRol.SelectedValue))
            {
                rol = (Rol)Enum.Parse(typeof(Rol), ddlRol.SelectedValue);
            }

            List<Usuario> lista = negocio.listar(txtFiltro.Text.Trim(), estado, rol);

            if (lista.Count == 0)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "No se encontraron coincidencias.";
            }
            else
            {
                lblMensaje.Visible = false;
            }

            gvUsuarios.DataSource = lista;
            gvUsuarios.DataBind();
        }


        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Usuario usuarioLogueado = (Usuario)Session["usuarioLogueado"];

            if (e.CommandName == "CambiarEstado")
            {
                if (id == usuarioLogueado.IdUsuario)
                {
                    return;
                }

                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario user = negocio.obtenerPorId(id);

                if (user.Activo)
                {
                    negocio.eliminar(id);
                }
                else
                {
                    negocio.activar(id);
                }

                cargarUsuarios();
            }

            if (e.CommandName == "Editar")
            {
                Response.Redirect("EditarUsuarioAdmin.aspx?id=" + id);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarUsuarios();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = null;

            ddlRol.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            cargarUsuarios();
        }
    }
}