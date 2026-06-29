using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class MisPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuarioLogueado"] == null)
            {
                lblMensaje.Text = "Debés iniciar sesión para ver tus pedidos";
                gvPedidos.Visible = false;
                return;
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

            var pedidos = negocio.ListarPorUsuario(user.IdUsuario);

            if (pedidos == null || pedidos.Count == 0)
            {
                lblMensaje.Text = "Aún no realizaste compras";
                gvPedidos.Visible = false;
                return;
            }

            gvPedidos.Visible = true;
            gvPedidos.DataSource = pedidos;
            gvPedidos.DataBind();
        }

        protected void btnDetalle_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idPedido = Convert.ToInt32(btn.CommandArgument);

            Response.Redirect("DetallePedido.aspx?id=" + idPedido);
        }

        protected void gvPedidos_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var pedido = (Pedido)e.Row.DataItem;

                // ocultar dirección si no es envío a domicilio
                if (pedido.FormaDeEntrega != FormaDeEntrega.EnvioADomicilio)
                {
                    e.Row.Cells[6].Text = ""; // columna Dirección
                }
            }
        }
    }
}


/*  protected void btnAgregar_Click(object sender, EventArgs e)
{
    int id = Convert.ToInt32(((Button)sender).CommandArgument);

    var carrito = (dominio.Carrito)Session["Carrito"];

    var item = carrito.ItemCarrito.FirstOrDefault(x => x.IdProducto == id);

    if (item != null)
    {
        item.Cantidad++;
    }
    else
    {
        Producto p = productoNegocio.obtenerPorId(id);

        carrito.ItemCarrito.Add(new CarritoItem
        {
            IdProducto = p.IdProducto,
            Nombre = p.Nombre,
            Precio = p.Precio,
            Cantidad = 1
        });
    }

    Session["Carrito"] = carrito;

    Response.Redirect("Carrito.aspx");
}  Metogo agregar al carrito que debe ir en catalogo */