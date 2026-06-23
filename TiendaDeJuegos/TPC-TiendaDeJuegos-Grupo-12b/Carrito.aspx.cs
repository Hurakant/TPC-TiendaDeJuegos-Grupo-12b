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
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            Usuario user = (Usuario)Session["usuarioLogueado"];

            if (user.Rol != Rol.Cliente)
            {
                Response.Redirect("Home.aspx");
                return;
            }

            InicializarSesion(user);

            carritoNegocio = new CarritoNegocio(ObtenerCarrito());

            if (!IsPostBack)
            {
                CargarCarrito();
                CargarCombos();

            }
        }

        private void InicializarSesion(Usuario user)
        {
            if (Session["Carrito_" + user.IdUsuario] == null)
                Session["Carrito_" + user.IdUsuario] = new dominio.Carrito();

            if (Session["Pedidos"] == null)
                Session["Pedidos"] = new List<Pedido>();

            if (Session["FormasDePago"] == null)
            {
                Session["FormasDePago"] = new List<FormaDePago>
        {
            new FormaDePago { IdFormaDePago = 1, Nombre = "Efectivo", Activa = true },
            new FormaDePago { IdFormaDePago = 2, Nombre = "Transferencia bancaria", Activa = true },
            new FormaDePago { IdFormaDePago = 3, Nombre = "Tarjeta", Activa = true },
            
        };
            }
        }

        private void CargarCarrito()
        {
            var carrito = ObtenerCarrito();

            gvCarrito.DataSource = carrito.ItemCarrito;
            gvCarrito.DataBind();

            lblTotal.Text = "$" + carritoNegocio.CalcularTotal().ToString("0.00");
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
            var carrito = ObtenerCarrito();

            carrito.ItemCarrito.Clear(); 

            CargarCarrito();
            lblMensaje.Text = "Carrito vaciado 🧹";
        }

     /*   protected void btnAgregarTest_Click(object sender, EventArgs e)
        {
            var carrito = ObtenerCarrito();

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
     */
        protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                var carrito = ObtenerCarrito();

                int idProducto = Convert.ToInt32(e.CommandArgument);

                carrito.ItemCarrito.RemoveAll(x => x.IdProducto == idProducto);

                CargarCarrito();
                lblMensaje.Text = "Producto eliminado ✔";
            }
        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            var carrito = ObtenerCarrito();
            Usuario user = (Usuario)Session["usuarioLogueado"];

            if (carrito.ItemCarrito.Count == 0)
            {
                lblMensaje.Text = "El carrito está vacío";
                return;
            }

            // Forma de pago
            FormaDePago pago = ((List<FormaDePago>)Session["FormasDePago"])
                .FirstOrDefault(x => x.IdFormaDePago == Convert.ToInt32(ddlPago.SelectedValue));

            if (pago == null)
            {
                lblMensaje.Text = "Seleccione una forma de pago válida";
                return;
            }

            // Forma de entrega (ENUM)
            FormaDeEntrega formaEntrega =
                (FormaDeEntrega)Convert.ToInt32(ddlEntrega.SelectedValue);

            // Crear pedido
            Pedido pedido = new Pedido
            {
                Cliente = user,
                FormaDePago = pago,
                FormaDeEntrega = formaEntrega,
                Detalle = carrito.ItemCarrito.ToList()
            };

            PedidoNegocio negocio = new PedidoNegocio();

            // INSERT PEDIDO
            int idPedido = negocio.CrearPedido(pedido);

            if (idPedido == 0)
            {
                lblMensaje.Text = "Error al crear el pedido";
                return;
            }

            // INSERT DETALLE
            foreach (CarritoItem item in carrito.ItemCarrito)
            {
                negocio.AgregarDetalle(idPedido, item);
            }

            // LIMPIAR CARRITO
            carrito.ItemCarrito.Clear();
            CargarCarrito();

            lblMensaje.Text = "Pedido creado ✔ ID: " + idPedido;
        }

        private dominio.Carrito ObtenerCarrito()
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];
            string key = "Carrito_" + user.IdUsuario;

            if (Session[key] == null)
                Session[key] = new dominio.Carrito();

            return (dominio.Carrito)Session[key];
        }

        protected void ddlEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEntrega.SelectedValue == "2")
            {
                CargarDirecciones();
                divDirecciones.Visible = true;
            }
            else
            {
                divDirecciones.Visible = false;
            }
        }

        private void CargarDirecciones()
        {
            int idUsuario = ((Usuario)Session["usuarioLogueado"]).IdUsuario;

            DireccionNegocio dire = new DireccionNegocio();

            ddlDirecciones.DataSource = dire.ListarPorUsuario(idUsuario);
            ddlDirecciones.DataTextField = "DireccionCompleta";
            ddlDirecciones.DataValueField = "IdDireccion";
            ddlDirecciones.DataBind();
        }
    }
}