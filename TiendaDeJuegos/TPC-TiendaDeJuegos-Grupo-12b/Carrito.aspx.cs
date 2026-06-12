using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class Carrito : Page
    {
        private CarritoNegocio carritoNegocio;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Carrito"] == null)
            {
                Session["Carrito"] = new dominio.Carrito();
            }

            carritoNegocio = new CarritoNegocio((dominio.Carrito)Session["Carrito"]);

            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            var carrito = (dominio.Carrito)Session["Carrito"];

            gvCarrito.DataSource = carrito.ItemCarrito;
            gvCarrito.DataBind();

            lblTotal.Text = "$" + carritoNegocio.CalcularTotal().ToString("0.00");
            lblDebug.Text = "Session items: " + carrito.ItemCarrito.Count;
        }

        protected void btnVaciar_Click(object sender, EventArgs e)
        {
            carritoNegocio.VaciarCarrito();
            CargarCarrito();

            lblMensaje.Text = "Carrito vaciado 🧹";
        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            var carrito = (dominio.Carrito)Session["Carrito"];

            if (carrito.ItemCarrito.Count == 0)
            {
                lblMensaje.Text = "El carrito está vacío";
                return;
            }

            // 1. Crear lista de pedidos si no existe
            if (Session["Pedidos"] == null)
                Session["Pedidos"] = new List<Pedido>();

            var pedidos = (List<Pedido>)Session["Pedidos"];

            // 2. Crear pedido
            Pedido pedido = new Pedido
            {
                IdPedido = pedidos.Count + 1,
                Fecha = DateTime.Now,
                Estado = EstadoPedido.Pendiente,
                FormaDeEntrega = (FormaDeEntrega)Convert.ToInt32(ddlEntrega.SelectedValue),

                Detalle = carrito.ItemCarrito.ToList()
            };

            // 3. Guardar pedido
            pedidos.Add(pedido);

            // 4. Vaciar carrito
            carritoNegocio.VaciarCarrito();
            CargarCarrito();

          //  lblMensaje.Text = "Pedido generado correctamente ";

            var ultimo = pedidos.Last();

            lblMensaje.Text =
            "Pedido OK  | ID: " + ultimo.IdPedido +
            " | Total: $" + ultimo.Total +
            " | Items: " + ultimo.Detalle.Count;
        }

        protected void btnAgregarTest_Click(object sender, EventArgs e)
        {
            var carrito = (dominio.Carrito)Session["Carrito"];

            carrito.ItemCarrito.Add(new CarritoItem
            {
                IdProducto = 1,
                Nombre = "Juego de prueba",
                Precio = 100,
                Cantidad = 1
            });

            CargarCarrito();

            lblMensaje.Text = "Producto agregado al carrito ✔";
        }

        protected void gvCarrito_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);

                carritoNegocio.EliminarProducto(idProducto);

                CargarCarrito();

                lblMensaje.Text = "Producto eliminado ✔";
            }
        }

        

    }
}