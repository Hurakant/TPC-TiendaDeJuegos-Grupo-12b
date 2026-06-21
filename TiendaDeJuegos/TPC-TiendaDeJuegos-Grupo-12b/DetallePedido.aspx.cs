using dominio;
using Negocio;
using System;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class DetallePedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            ddlEstado.SelectedValue = pedido.Estado.ToString();
            ddlEstado.SelectedValue = ((int)pedido.Estado).ToString();
            lblPago.Text = pedido.FormaDePagoTexto;
            lblTotal.Text = pedido.MontoTotal.ToString("N2");

            gvDetalle.DataSource = negocio.ObtenerDetallePedido(idPedido);
            gvDetalle.DataBind();
        }

        private void AplicarPermisos()
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];

            if (user.Rol == Rol.Vendedor)
            {
                
                ddlEstado.Enabled = true;
            }
            else if (user.Rol == Rol.Admin)
            {
                
                ddlEstado.Enabled = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idPedido = Convert.ToInt32(Request.QueryString["id"]);

            PedidoNegocio negocio = new PedidoNegocio();

            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                Estado = (EstadoPedido)Convert.ToInt32(ddlEstado.SelectedValue)
            };

            negocio.ModificarEstado(pedido);
        }
    }
}