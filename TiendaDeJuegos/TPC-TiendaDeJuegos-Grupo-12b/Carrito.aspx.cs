using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class Carrito : Page
    {
        private CarritoNegocio carritoNegocio;

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarSesion();

            carritoNegocio = new CarritoNegocio((dominio.Carrito)Session["Carrito"]);

            if (!IsPostBack)
            {
                CargarCarrito();
                CargarCombos();
            }
        }

        private void InicializarSesion()
        {
            if (Session["Carrito"] == null)
                Session["Carrito"] = new dominio.Carrito();

            if (Session["Pedidos"] == null)
                Session["Pedidos"] = new List<Pedido>();

            if (Session["FormasDePago"] == null)
            {
                Session["FormasDePago"] = new List<FormaDePago>
                {
                    new FormaDePago { IdFormaDePago = 1, Nombre = "Efectivo", Activa = true },
                    new FormaDePago { IdFormaDePago = 2, Nombre = "Transferencia bancaria", Activa = true },
                    new FormaDePago { IdFormaDePago = 3, Nombre = "Tarjeta", Activa = true },
                    new FormaDePago { IdFormaDePago = 4, Nombre = "MercadoPago", Activa = true }
                };
            }
        }

        private void CargarCarrito()
        {
            var carrito = (dominio.Carrito)Session["Carrito"];

            gvCarrito.DataSource = carrito.ItemCarrito;
            gvCarrito.DataBind();

            lblTotal.Text = "$" + carritoNegocio.CalcularTotal().ToString("0.00");
            lblDebug.Text = "Items: " + carrito.ItemCarrito.Count;
        }

        private void CargarCombos()
        {
            ddlEntrega.Items.Clear();
            ddlEntrega.Items.Add(new ListItem("Retiro en local", "1"));
            ddlEntrega.Items.Add(new ListItem("Envío a domicilio", "2"));
            ddlEntrega.Items.Add(new ListItem("Código por email", "3"));

            var formas = (List<FormaDePago>)Session["FormasDePago"];

            ddlPago.DataSource = formas;
            ddlPago.DataTextField = "Nombre";
            ddlPago.DataValueField = "IdFormaDePago";
            ddlPago.DataBind();
        }

        protected void btnVaciar_Click(object sender, EventArgs e)
        {
            carritoNegocio.VaciarCarrito();
            CargarCarrito();
            lblMensaje.Text = "Carrito vaciado 🧹";
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
            lblMensaje.Text = "Producto agregado ✔";
        }

        protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);
                carritoNegocio.EliminarProducto(idProducto);
                CargarCarrito();
                lblMensaje.Text = "Producto eliminado ✔";
            }
        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            var carrito = (dominio.Carrito)Session["Carrito"];

            if (carrito.ItemCarrito.Count == 0)
            {
                lblMensaje.Text = "El carrito está vacío";
                return;
            }

            var pedidos = (List<Pedido>)Session["Pedidos"];
            var formas = (List<FormaDePago>)Session["FormasDePago"];

            FormaDePago pago = formas.FirstOrDefault(x =>
                x.IdFormaDePago == Convert.ToInt32(ddlPago.SelectedValue));

            Pedido pedido = new Pedido
            {
                IdPedido = pedidos.Count + 1,
                Fecha = DateTime.Now,
                Estado = EstadoPedido.Pendiente,
                FormaDeEntrega = (FormaDeEntrega)Convert.ToInt32(ddlEntrega.SelectedValue),
                FormaDePago = pago,
                Detalle = carrito.ItemCarrito.ToList()
            };

            pedidos.Add(pedido);

            carritoNegocio.VaciarCarrito();
            CargarCarrito();

            var ultimo = pedidos.Last();

            lblMensaje.Text =
                "Pedido OK | ID: " + ultimo.IdPedido +
                " | Total: $" + ultimo.Total +
                " | Items: " + ultimo.Detalle.Count;
        }
    }
}