using dominio;
using Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class Home : System.Web.UI.Page
    {
        private int contador = 0;

        //cuantos productos se muestran como maximo en el home
        private const int LimiteProductosHome = 20;
        public List<List<Producto>> RpgSlides { get; set; }
        public List<List<Producto>> AccionSlides { get; set; }
        public List<List<Producto>> ShooterSlides { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProductoNegocio negocio = new ProductoNegocio();
                List<Producto> productos = negocio.listar();

                //Carousel principal de juegos
                rptCarousel.DataSource = productos;
                rptCarousel.DataBind();

                //categorías wiwiwi, se dividen el slides de cards, la cantidad de cards esta en la funcion dividirSlides
                var rpg = productos.Where(p => p.Categoria.Any(c => c.NombreCategoria == "RPG")).Take(LimiteProductosHome).ToList();
                RpgSlides = DividirEnSlides(rpg, 6);
                rptRpg.DataSource = RpgSlides;
                rptRpg.DataBind();

                var accion = productos.Where(p => p.Categoria.Any(c => c.NombreCategoria == "Acción")).Take(LimiteProductosHome).ToList();
                AccionSlides = DividirEnSlides(accion, 6);
                rptAccion.DataSource = AccionSlides;
                rptAccion.DataBind();

                var shooter = productos.Where(p => p.Categoria.Any(c => c.NombreCategoria == "Shooter")).Take(LimiteProductosHome).ToList();
                ShooterSlides = DividirEnSlides(shooter, 6);
                rptShooter.DataSource = ShooterSlides;
                rptShooter.DataBind();

                if (Session["usuarioLogueado"] != null)
                {
                    Usuario user = (Usuario)Session["usuarioLogueado"];
                    lblBienvenida.Visible = true;
                    lblBienvenida.Text = "Bienvenid@, " + user.Nombre + " :) ";

                    if (user.Rol == Rol.Admin)
                    {
                        divAdmin.Visible = true;
                        divVendedor.Visible = false;
                        divCliente.Visible = false;
                    }
                    else if (user.Rol == Rol.Vendedor)
                    {
                        divAdmin.Visible = false;
                        divVendedor.Visible = true;
                        divCliente.Visible = false;
                    }
                    else //cliente
                    {
                        divAdmin.Visible = false;
                        divVendedor.Visible = false;
                        divCliente.Visible = true;
                    }
                }
                else
                {
                    lblBienvenida.Visible = false;
                    divAdmin.Visible = false;
                    divVendedor.Visible = false;
                    divCliente.Visible = true;
                }
            }
        }
        protected void rptCarousel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlGenericControl slide =
                    (HtmlGenericControl)e.Item.FindControl("slideContainer");

                slide.Attributes["class"] =
                    contador == 0
                    ? "carousel-item active"
                    : "carousel-item";

                contador++;
            }
        }
        protected void btnCategoria_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];
            if (user.Rol == Rol.Admin)
            {

                Response.Redirect("AdminCategorias.aspx");
            }
            else
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnProductos_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];
            if (user.Rol == Rol.Admin)
            {

                Response.Redirect("AdminProductos.aspx");
            }
            else
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnUsuarios_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];
            if (user.Rol == Rol.Admin)
            {

                Response.Redirect("AdminUsuarios.aspx");
            }
            else
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnPedidos_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];
            if (user.Rol == Rol.Admin)
            {

                Response.Redirect("~/Pedidos.aspx");
            }
            else
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnVendedorProductos_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];
            if (user.Rol == Rol.Vendedor)
            {

                Response.Redirect("VendedorProductos.aspx");
            }
            else
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnVendedorPedidos_Click(object sender, EventArgs e)
        {
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            Usuario user = (Usuario)Session["usuarioLogueado"];

            if (user.Rol == Rol.Vendedor)
            {
                Response.Redirect("~/Pedidos.aspx");
            }
            else
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAccesibilidad_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];
            if (user.Rol == Rol.Admin)
            {

                Response.Redirect("AdminAccesibilidad.aspx");
            }
            else
            {
                Response.Redirect("Error.aspx", false);
            }

        }
        //Funcion para dividir el slides todos los productos para ser mostrados en los carruseles
        private List<List<Producto>> DividirEnSlides(List<Producto> productos, int cantidad)
        {
            List<List<Producto>> resultado = new List<List<Producto>>();

            for (int i = 0; i < productos.Count; i += cantidad)
            {
                resultado.Add(productos.Skip(i).Take(cantidad).ToList());
            }

            return resultado;
        }

        // Antes se llamaba rptRpg_ItemDataBound y solo lo usaba rptRpg,
        // la logica es identica para RPG, Accion y Shooter, asi que los 3
        // repeaters usan este mismo metodo (OnItemDataBound usan "rptCategoria_ItemDataBound" en los 3)
        protected void rptCategoria_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlGenericControl slide =
                    (HtmlGenericControl)e.Item.FindControl("slide");

                if (e.Item.ItemIndex == 0)
                    slide.Attributes["class"] = "carousel-item active";
                else
                    slide.Attributes["class"] = "carousel-item";

                Repeater rpt =
                    (Repeater)e.Item.FindControl("rptCards");

                rpt.DataSource = (List<Producto>)e.Item.DataItem;
                rpt.DataBind();
            }
        }
    }
}