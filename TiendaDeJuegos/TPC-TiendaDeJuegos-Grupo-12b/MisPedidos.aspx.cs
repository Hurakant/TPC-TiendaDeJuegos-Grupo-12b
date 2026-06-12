using dominio;
using System;
using System.Collections.Generic;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class MisPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        private void CargarPedidos()
        {
            if (Session["Pedidos"] == null)
            {
                lblMensaje.Text = "No tenés pedidos aún";
                return;
            }

            var pedidos = (List<Pedido>)Session["Pedidos"];

            gvPedidos.DataSource = pedidos;
            gvPedidos.DataBind();
        }
    }
}