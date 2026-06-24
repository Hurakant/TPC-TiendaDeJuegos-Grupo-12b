using dominio;
using Negocio;
using System;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class DetallePedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarPagos();
                CargarEntrega();
                CargarPedido();
                AplicarPermisos();
            }
        }

        private void CargarPedido()
        {
            if (Request.QueryString["id"] == null)
                return;

            int idPedido = Convert.ToInt32(Request.QueryString["id"]);

            PedidoNegocio negocio = new PedidoNegocio();
            Pedido pedido = negocio.ObtenerPedido(idPedido);

            if (pedido == null)
                return;

            lblIdPedido.Text = pedido.IdPedido.ToString();
            lblFecha.Text = pedido.Fecha.ToString("dd/MM/yyyy");

            // ENTREGA
            switch (pedido.FormaDeEntrega)
            {
                case FormaDeEntrega.RetiroEnLocal:
                    lblEntrega.Text = "Retiro en local";
                    break;

                case FormaDeEntrega.EnvioADomicilio:
                    lblEntrega.Text = "Envío a domicilio";
                    break;

                case FormaDeEntrega.CodigoPorEmail:
                    lblEntrega.Text = "Código por email";
                    break;

                default:
                    lblEntrega.Text = "No especificado";
                    break;
            }

            lblTotal.Text = pedido.MontoTotal.ToString("N2");
            ddlEstado.SelectedValue = ((int)pedido.Estado).ToString();

            //  solo setear si existe
            if (pedido.FormaDePago != null && ddlPago.Items.FindByValue(pedido.FormaDePago.IdFormaDePago.ToString()) != null)
            {
                ddlPago.SelectedValue = pedido.FormaDePago.IdFormaDePago.ToString();
            }

            gvDetalle.DataSource = negocio.ObtenerDetallePedido(idPedido);
            gvDetalle.DataBind();
        }

        private void AplicarPermisos()
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];

            if (user.Rol == Rol.Vendedor)
            {
                ddlEstado.Enabled = true;

                if (ddlEntrega != null)
                    ddlEntrega.Enabled = false;

                if (ddlPago != null)
                    ddlPago.Enabled = false;
            }
            else if (user.Rol == Rol.Admin)
            {
                ddlEstado.Enabled = true;

                if (ddlEntrega != null)
                    ddlEntrega.Enabled = true;

                if (ddlPago != null)
                    ddlPago.Enabled = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            Usuario user = (Usuario)Session["usuarioLogueado"];
            int idPedido = Convert.ToInt32(Request.QueryString["id"]);

            Pedido pedido = new Pedido();
            pedido.IdPedido = idPedido;
            pedido.Estado = (EstadoPedido)Convert.ToInt32(ddlEstado.SelectedValue);

            //ADMIN PUEDE CAMBIAR PAGO Y ENTREGA
            if (user.Rol == Rol.Admin)
            {
                pedido.FormaDePago = new FormaDePago
                {
                    IdFormaDePago = Convert.ToInt32(ddlPago.SelectedValue)
                };

                pedido.FormaDeEntrega =
                    (FormaDeEntrega)Convert.ToInt32(ddlEntrega.SelectedValue);
            }
            else
            {
               
                PedidoNegocio aux = new PedidoNegocio();
                Pedido actual = aux.ObtenerPedido(idPedido);

                pedido.FormaDePago = actual.FormaDePago;
                pedido.FormaDeEntrega = actual.FormaDeEntrega;
            }

            PedidoNegocio negocio = new PedidoNegocio();
            negocio.ModificarPedidoCompleto(pedido);

            lblMensaje.Visible = true;
            lblMensaje.Text = "Pedido actualizado correctamente.";
            lblMensaje.CssClass = "alert alert-success d-block";
        }

        private void CargarPagos()
        {
            ddlPago.DataSource = new FormaDePagoNegocio().Listar();
            ddlPago.DataTextField = "Nombre";
            ddlPago.DataValueField = "IdFormaDePago";
            ddlPago.DataBind();
        }

        private void CargarEntrega()
        {
            ddlEntrega.Items.Clear();

            ddlEntrega.Items.Add(new ListItem("Retiro en local", ((int)FormaDeEntrega.RetiroEnLocal).ToString()));
            ddlEntrega.Items.Add(new ListItem("Envío a domicilio", ((int)FormaDeEntrega.EnvioADomicilio).ToString()));
            ddlEntrega.Items.Add(new ListItem("Código por email", ((int)FormaDeEntrega.CodigoPorEmail).ToString()));
        }
    }
}