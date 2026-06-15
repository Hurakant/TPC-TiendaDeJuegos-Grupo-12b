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