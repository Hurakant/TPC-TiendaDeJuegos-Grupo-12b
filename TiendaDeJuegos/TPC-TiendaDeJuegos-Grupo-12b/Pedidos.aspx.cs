using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class Pedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            Usuario user = (Usuario)Session["usuarioLogueado"];

            if (user.Rol != Rol.Vendedor && user.Rol != Rol.Admin)
            {
                Session["ErrorNoPermisos"] = true;
                Response.Redirect("Error.aspx");
                Response.Redirect("Error.aspx");
                return;
            }

            if (user.Rol == Rol.Admin)
            {
                lblTitulo.Text = "Gestión de Pedidos (Administrador)";
            }
            else
            {
                lblTitulo.Text = "Gestión de Pedidos";
            }


            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        private void CargarPedidos()
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];

            PedidoNegocio negocio = new PedidoNegocio();

            List<Pedido> pedidos;

            if (user.Rol == Rol.Admin || user.Rol == Rol.Vendedor)
                pedidos = negocio.ListarTodos();
            else
                pedidos = negocio.ListarPorUsuario(user.IdUsuario);

            gvPedidos.DataSource = pedidos;
            gvPedidos.DataBind();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "Estado actualizado (conectar a BD después)";
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}