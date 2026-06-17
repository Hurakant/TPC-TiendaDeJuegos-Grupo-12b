using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Negocio;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class Home : System.Web.UI.Page
    {
        private int contador = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProductoNegocio negocio = new ProductoNegocio();
                List<Producto> productos = negocio.listar();

                //Carousel principal de juegos

               rptCarousel.DataSource = productos;
                rptCarousel.DataBind();

                //categorías wiwiwi
                rptRpg.DataSource = productos.Where(p =>
                     p.Categoria.Any(c => c.NombreCategoria == "RPG"));

                 rptAccion.DataSource = productos.Where(p =>
                     p.Categoria.Any(c => c.NombreCategoria == "Accion"));

                 rptShooter.DataSource = productos.Where(p =>
                     p.Categoria.Any(c => c.NombreCategoria == "Shooter"));

                 rptRpg.DataBind();
                 rptAccion.DataBind();
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
        protected async void btnProbar_Click(object sender, EventArgs e)
        {
            string apiKey = "049bd22f1dfb40d9b2a605b98f5b3621";

            string url = $"https://api.rawg.io/api/games?key={apiKey}";

            using (HttpClient cliente = new HttpClient())
            {
                HttpResponseMessage respuesta = await cliente.GetAsync(url);

                if (!respuesta.IsSuccessStatusCode)
                {
                    txtResultado.Text = "Error: " + respuesta.StatusCode;
                    return;
                }

                string json = await respuesta.Content.ReadAsStringAsync();

                var datos = JsonConvert.DeserializeObject<RespuestaRAWG>(json);

                string conexion = @"Server=.\SQLEXPRESS;Database=NovaHub;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    cn.Open();

                    foreach (var juego in datos.results.Take(20))
                    {
                        SqlCommand cmd = new SqlCommand(
                        @"IF NOT EXISTS
            (
                SELECT 1
                FROM Producto
                WHERE Id = @Id
            )
            INSERT INTO Producto
            (
                Id,
                Nombre,
                FechaLanzamiento,
                Rating,
                Imagen
            )
            VALUES
            (
                @Id,
                @Nombre,
                @Fecha,
                @Rating,
                @Imagen
            )", cn);

                        cmd.Parameters.AddWithValue("@Id", juego.id);
                        cmd.Parameters.AddWithValue("@Nombre", juego.name);
                        cmd.Parameters.AddWithValue("@Rating", juego.rating);
                        cmd.Parameters.AddWithValue("@Imagen", juego.background_image ?? "");

                        if (string.IsNullOrEmpty(juego.released))
                            cmd.Parameters.AddWithValue("@Fecha", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@Fecha", DateTime.Parse(juego.released));

                        cmd.ExecuteNonQuery();
                    }
                }

                txtResultado.Text = "Productos guardados correctamente.";
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

                Response.Redirect("AdminPedidos.aspx");
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
                Response.Redirect("~/VendedorPedidos.aspx");
            }
            else
            {
                Response.Redirect("Error.aspx");
            }
        }
    }
}