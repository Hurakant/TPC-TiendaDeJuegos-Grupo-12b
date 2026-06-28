using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

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
            if (Session["usuarioLogueado"] != null)
            {
                Usuario user = (Usuario)Session["usuarioLogueado"];

                litSesionTexto.Text = "Mi Cuenta";
            }
            else
            {
                litSesionTexto.Text = "Iniciar Sesión";
            }
        }

        public void ActualizarCarrito()
        {
            int cantidad = 0;

            // Caso 1: carrito global
            if (Session["Carrito"] is dominio.Carrito carrito)
            {
                cantidad = carrito.ItemCarrito.Sum(x => x.Cantidad);
            }
            // Caso 2: carrito por usuario (tu implementación actual)
            else if (Session["usuarioLogueado"] != null)
            {
                Usuario user = (Usuario)Session["usuarioLogueado"];
                string key = "Carrito_" + user.IdUsuario;

                if (Session[key] is dominio.Carrito carritoUser)
                {
                    cantidad = carritoUser.ItemCarrito.Sum(x => x.Cantidad);
                }
            }

            lblCantidadCarrito.Text = cantidad.ToString();
            lblCantidadCarrito.Visible = cantidad > 0;
        }

        protected void lnkBtnSesion_Click(object sender, EventArgs e)
        {
            if (Session["usuarioLogueado"] != null)
            {
                Response.Redirect("MiCuenta.aspx");
            }

            else
            {
                Response.Redirect("Login.aspx");
            }
                
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