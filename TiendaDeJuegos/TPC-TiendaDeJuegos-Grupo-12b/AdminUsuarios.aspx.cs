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
                Response.Redirect("Error.aspx", false);
            }

            if (!IsPostBack)
            {
                cargarUsuarios();
            }
        }

        private void cargarUsuarios()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            gvUsuarios.DataSource = negocio.listar();
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
    }
}