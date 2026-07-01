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

            rptCarrito.DataSource = carrito.ItemCarrito;
            rptCarrito.DataBind();

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
            lblMensaje.Text = "Carrito vaciado ";
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
            lblMensaje.Text = "Producto agregado ";
        }
     */
        //protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Eliminar")
        //    {
        //        var carrito = ObtenerCarrito();

        //        int idProducto = Convert.ToInt32(e.CommandArgument);

        //        carrito.ItemCarrito.RemoveAll(x => x.IdProducto == idProducto);

        //        CargarCarrito();
        //        lblMensaje.Text = "Producto eliminado ";
        //    }
        //}

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            var carrito = ObtenerCarrito();
            Usuario user = (Usuario)Session["usuarioLogueado"];

            if (carrito.ItemCarrito.Count == 0)
            {
                lblMensaje.Text = "El carrito está vacío";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Forma de pago
            FormaDePago pago = ((List<FormaDePago>)Session["FormasDePago"])
                .FirstOrDefault(x => x.IdFormaDePago == Convert.ToInt32(ddlPago.SelectedValue));

            if (pago == null)
            {
                lblMensaje.Text = "Seleccione una forma de pago válida.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Forma de entrega
            FormaDeEntrega formaEntrega =
                (FormaDeEntrega)Convert.ToInt32(ddlEntrega.SelectedValue);

            // ID Dirección
            int? idDireccion = null;

            if (formaEntrega == FormaDeEntrega.EnvioADomicilio)
            {
                if (ddlDirecciones.Items.Count == 0 ||
                    string.IsNullOrEmpty(ddlDirecciones.SelectedValue))
                {
                    lblMensaje.Text = "Seleccione una dirección de entrega.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                idDireccion = Convert.ToInt32(ddlDirecciones.SelectedValue);
            }

            // Crear pedido
            Pedido pedido = new Pedido
            {
                Cliente = user,
                FormaDePago = pago,
                FormaDeEntrega = formaEntrega,
                Detalle = carrito.ItemCarrito.ToList(),
                IDDireccion = idDireccion   
            };

            PedidoNegocio negocio = new PedidoNegocio();

         
            int idPedido = negocio.CrearPedido(pedido);

            if (idPedido == 0)
            {
                lblMensaje.Text = "Error al crear el pedido.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

          
            foreach (CarritoItem item in carrito.ItemCarrito)
            {
                negocio.AgregarDetalle(idPedido, item);
            }

            
            carrito.ItemCarrito.Clear();
            CargarCarrito();

            lblMensaje.Text = " Pedido creado correctamente. ID: " + idPedido;
            lblMensaje.ForeColor = System.Drawing.Color.Green;
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

            var lista = dire.ListarPorUsuario(idUsuario);

            ddlDirecciones.Items.Clear();

            if (lista.Count == 0)
            {
                divDirecciones.Visible = true;

                ddlDirecciones.Items.Add(new ListItem("No posee direcciones registradas", "0"));

                btnComprar.Enabled = false;

                lblMensaje.Text = "Para recibir el pedido en su domicilio primero debe registrar una dirección.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lnkDireccion.Visible = true;

                return;
            }

            ddlDirecciones.DataSource = lista;
            ddlDirecciones.DataTextField = "DireccionCompleta";
            ddlDirecciones.DataValueField = "IDDireccion";
            ddlDirecciones.DataBind();

            ddlDirecciones.Items.Insert(0,
    new ListItem("-- Seleccione una dirección --", ""));

            btnComprar.Enabled = true;
            lblMensaje.Text = "";

            lnkDireccion.Visible = false;
        }

        protected void rptCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                var carrito = ObtenerCarrito();

                int idProducto = Convert.ToInt32(e.CommandArgument);

                carrito.ItemCarrito.RemoveAll(x => x.IdProducto == idProducto);

                CargarCarrito();
                lblMensaje.Text = "Producto eliminado";
            }
        }
    }
}