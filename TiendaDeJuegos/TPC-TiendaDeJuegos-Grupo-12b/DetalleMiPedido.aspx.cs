using dominio;
using Dominio;
using Negocio;
using System;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class DetalleMiPedido : System.Web.UI.Page
    {
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
                Session["ErrorNoPermisos"] = true;
                Response.Redirect("Error.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarPedido();
            }
        }

        private void CargarPedido()
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("MisPedidos.aspx");
                return;
            }

            int idPedido = Convert.ToInt32(Request.QueryString["id"]);

            PedidoNegocio negocio = new PedidoNegocio();
            Pedido pedido = negocio.ObtenerPedido(idPedido);

            if (pedido == null)
            {
                Response.Redirect("MisPedidos.aspx");
                return;
            }

            Usuario user = (Usuario)Session["usuarioLogueado"];

            //Habilitar esto cuando traiga correctamente la id del usuario
            //if (pedido.Cliente.IdUsuario != user.IdUsuario)
            //{
            //    Session["ErrorNoPermisos"] = true;
            //    Response.Redirect("Error.aspx");
            //    return;
            //}

            lblIdPedido.Text = pedido.IdPedido.ToString();
            lblFecha.Text = pedido.Fecha.ToString("dd/MM/yyyy");

            switch (pedido.FormaDeEntrega)
            {
                case FormaDeEntrega.RetiroEnLocal:
                    lblEntrega.Text = "Retiro en local";
                    lblFormaDeEntrega.Text = "Retiro en local";
                    break;

                case FormaDeEntrega.EnvioADomicilio:
                    lblEntrega.Text = "Envío a domicilio";
                    lblFormaDeEntrega.Text = "Envío a domicilio";
                    break;

                case FormaDeEntrega.CodigoPorEmail:
                    lblEntrega.Text = "Código por email";
                    lblFormaDeEntrega.Text = "Código por email";
                    break;

                default:
                    lblEntrega.Text = "No especificado";
                    lblFormaDeEntrega.Text = "No especificado";
                    break;
            }

            lblFormaDePago.Text = pedido.FormaDePago != null
                ? pedido.FormaDePago.Nombre
                : "No especificada";

            lblTotal.Text = pedido.MontoTotal.ToString("N2");

            gvDetalle.DataSource = negocio.ObtenerDetallePedido(idPedido);
            gvDetalle.DataBind();
        }
    }
}
