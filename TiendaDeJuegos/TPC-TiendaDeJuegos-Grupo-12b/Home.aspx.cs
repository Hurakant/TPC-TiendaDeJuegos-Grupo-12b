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
                        using (SqlCommand cmd = new SqlCommand(@"
IF NOT EXISTS (
    SELECT 1 FROM Producto WHERE Nombre = @Nombre
)
BEGIN
    INSERT INTO Producto
    (
        Nombre, Descripcion, ImagenUrl, FechaLanzamiento,
        Precio, Descuento, Stock, EsDigital, Activo, IDCategoria
    )
    VALUES
    (
        @Nombre, @Descripcion, @ImagenUrl, @Fecha,
        @Precio, @Descuento, @Stock, @EsDigital, @Activo, @IDCategoria
    )
END", cn))
                        {
                            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = juego.name;
                            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = "Importado desde API RAWG";
                            cmd.Parameters.Add("@ImagenUrl", SqlDbType.VarChar).Value = juego.background_image ?? "";

                            cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value =
                                DateTime.TryParse(juego.released, out DateTime fecha)
                                ? fecha
                                : (object)DBNull.Value;

                            var precioParam = cmd.Parameters.Add("@Precio", SqlDbType.Decimal);
                            precioParam.Value = 1000m;
                            precioParam.Precision = 18;
                            precioParam.Scale = 2;

                            cmd.Parameters.Add("@Descuento", SqlDbType.Int).Value = 0;
                            cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = 10;
                            cmd.Parameters.Add("@EsDigital", SqlDbType.Bit).Value = true;
                            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = true;
                            cmd.Parameters.Add("@IDCategoria", SqlDbType.Int).Value = 1;

                            cmd.ExecuteNonQuery();
                        }
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