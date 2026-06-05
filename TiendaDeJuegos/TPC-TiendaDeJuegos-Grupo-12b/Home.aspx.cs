using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class Home : System.Web.UI.Page
    {
        private int contador = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Producto> productos = ObtenerProductos();

                // Carousel principal de juegos
                rptCarousel.DataSource = productos;
                rptCarousel.DataBind();

                // Categorías wiwiwi
                rptRpg.DataSource = productos.Where(p =>
                    p.Categoria.Any(c => c.NombreCategoria == "RPG"));

                rptAccion.DataSource = productos.Where(p =>
                    p.Categoria.Any(c => c.NombreCategoria == "Accion"));

                rptShooter.DataSource = productos.Where(p =>
                    p.Categoria.Any(c => c.NombreCategoria == "Shooter"));

                rptRpg.DataBind();
                rptAccion.DataBind();
                rptShooter.DataBind();
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
        private List<Producto> ObtenerProductos()
        {
            return new List<Producto>
            {
                new Producto
                {
                    IdProducto = 1,
                    Nombre = "Elden Ring",
                    Descripcion = "Action RPG de mundo abierto",
                    ImagenUrl = "https://picsum.photos/id/1018/1200/500",
                    Precio = 59999,
                    Descuento = 0,
                    Stock = 10,
                    FechaLanzamiento = 2022,
                    EsDigital = true,
                    Activo = true,

                    Categoria = new List<Categoria>
                    {
                        new Categoria
                        {
                            IdCategoria = 1,
                            NombreCategoria = "RPG"
                        }
                    }
                },

                new Producto
                {
                    IdProducto = 2,
                    Nombre = "Cyberpunk 2077",
                    Descripcion = "RPG futurista",
                    ImagenUrl = "https://picsum.photos/id/1015/1200/500",
                    Precio = 44999,
                    Descuento = 15,
                    Stock = 5,
                    FechaLanzamiento = 2020,
                    EsDigital = true,
                    Activo = true,

                    Categoria = new List<Categoria>
                    {
                        new Categoria
                        {
                            IdCategoria = 1,
                            NombreCategoria = "RPG"
                        },
                        new Categoria
                        {
                            IdCategoria = 2,
                            NombreCategoria = "Accion"
                        }
                    }
                },

                new Producto
                {
                    IdProducto = 3,
                    Nombre = "Call Of Duty",
                    Descripcion = "Shooter militar",
                    ImagenUrl = "https://picsum.photos/id/1019/1200/500",
                    Precio = 39999,
                    Descuento = 0,
                    Stock = 20,
                    FechaLanzamiento = 2023,
                    EsDigital = true,
                    Activo = true,

                    Categoria = new List<Categoria>
                    {
                        new Categoria
                        {
                            IdCategoria = 3,
                            NombreCategoria = "Shooter"
                        }
                    }
                }
            };
        }
    }
}