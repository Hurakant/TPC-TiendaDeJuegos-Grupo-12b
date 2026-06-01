using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class Master1 : System.Web.UI.MasterPage
    {
        // ESTO ES SOLO PARA PLACEHOLDER
        // Clase para bindear el Repeater
        public class Categoria
        {
            public string Nombre { get; set; }
            public string Url { get; set; }
            public string Icono { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CantidadCarrito"] = 777;
                CargarCategorias();
                ActualizarSesion();
                ActualizarCarrito();
            }
        }

        public void CargarCategorias()
        {
            var categorias = new List<Categoria>
    {
        new Categoria { Nombre = "Shooters",     Url = "~/Cat/Shooters.aspx",    Icono = "bi bi-crosshair me-2" },
        new Categoria { Nombre = "Action-RPG",   Url = "~/Cat/ActionRPG.aspx",   Icono = "bi bi-shield" },
        new Categoria { Nombre = "Battle Royale",Url = "~/Cat/BattleRoyale.aspx",Icono = "bi bi-flag me-2" },
        new Categoria { Nombre = "Sandbox",      Url = "~/Cat/Sandbox.aspx",     Icono = "bi bi-box me-2" },
        new Categoria { Nombre = "MOBA",         Url = "~/Cat/MOBA.aspx",        Icono = "bi bi-people me-2" },
    };
            rptCategorias.DataSource = categorias;
            rptCategorias.DataBind();
        }

        public void ActualizarSesion()
        {
            if (Session["Usuario"] != null)
            {
                litSesionTexto.Text = Session["Usuario"].ToString();
            }
            else
            {
                litSesionTexto.Text = "Iniciar Sesion";
            }
        }

        public void ActualizarCarrito()
        {
            int cantidad = Session["CantidadCarrito"] != null
                ? (int)Session["CantidadCarrito"]
                : 0;

            lblCantidadCarrito.Text = cantidad.ToString();
            lblCantidadCarrito.Visible = cantidad > 0;
        }

        protected void lnkBtnSesion_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
                Response.Redirect("~/MiCuenta.aspx");
            else
                Response.Redirect("~/Login.aspx");
        }

        protected void lnkBtnCarrito_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Carrito.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string termino = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(termino))
                Response.Redirect($"~/Busqueda.aspx?q={Server.UrlEncode(termino)}");
        }
    }
}