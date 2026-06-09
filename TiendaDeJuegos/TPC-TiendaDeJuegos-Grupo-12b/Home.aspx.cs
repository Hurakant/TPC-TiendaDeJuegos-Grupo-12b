using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class Home : System.Web.UI.Page
    {
        private int contador = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Producto> productos = ObtenerProductosBD();

                // Carousel principal de juegos
                rptCarousel.DataSource = productos;
                rptCarousel.DataBind();

                //categorías wiwiwi
                /* rptRpg.DataSource = productos.Where(p =>
                     p.Categoria.Any(c => c.NombreCategoria == "RPG"));

                 rptAccion.DataSource = productos.Where(p =>
                     p.Categoria.Any(c => c.NombreCategoria == "Accion"));

                 rptShooter.DataSource = productos.Where(p =>
                     p.Categoria.Any(c => c.NombreCategoria == "Shooter"));

                 rptRpg.DataBind();
                 rptAccion.DataBind();
                 rptShooter.DataBind();*/


                if (Session["usuarioLogueado"] != null)
                {
                    Usuario user = (Usuario)Session["usuarioLogueado"];
                    lblBienvenida.Visible = true;
                    lblBienvenida.Text = "Bienvenid@, " + user.Nombre;
                }
                else
                {
                    lblBienvenida.Visible = false;
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

                string conexion =
                               @"Server=localhost,1433;Database=TP1;User Id=sa;Password=;TrustServerCertificate=True;";

                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    cn.Open();

                    foreach (var juego in datos.results.Take(20))
                    {
                        SqlCommand cmd = new SqlCommand(
                        @"IF NOT EXISTS
                  (
                    SELECT 1
                    FROM Juegos
                    WHERE Id = @Id
                  )
                  INSERT INTO Juegos
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

                        if (string.IsNullOrEmpty(juego.released))
                            cmd.Parameters.AddWithValue("@Fecha", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@Fecha",
                                DateTime.Parse(juego.released));

                        cmd.Parameters.AddWithValue("@Rating", juego.rating);
                        cmd.Parameters.AddWithValue("@Imagen",
                            juego.background_image ?? "");

                        cmd.ExecuteNonQuery();
                    }
                }

                txtResultado.Text = "Juegos guardados correctamente.";
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

        private List<Producto> ObtenerProductosBD()
        {
            List<Producto> lista = new List<Producto>();

            string conexion =
                "server=.\\SQLEXPRESS; database=Productos; integrated security=true;";

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Juegos", cn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Producto aux = new Producto();

                    aux.IdProducto = (int)dr["Id"];
                    aux.Nombre = dr["Nombre"].ToString();

                    // La imagen de RAWG está guardada en la columna Imagen
                    aux.ImagenUrl = dr["Imagen"].ToString();

                    //  para que no se rompa el diseño
                    aux.Descripcion = "Juego importado desde RAWG";
                    aux.Precio = 0;

                    aux.Categoria = new List<Categoria>();

                    lista.Add(aux);
                }
            }

            return lista;
        }
    }
}