using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class AdminProductos : System.Web.UI.Page
    {
        private int idProducto = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["id"], out idProducto) || idProducto <= 0)
            { 

            }

            if (!IsPostBack)
            {
                CargarProducto();
            }
        }

        private void CargarProducto()
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                dominio.Producto prod = negocio.listarPorId(idProducto);

                if (prod == null)
                {
                    pnlEditandoProducto.Visible = false;
                    pnlNoEncontrado.Visible = true;
                    return;
                }

                txtNombre.Text = prod.Nombre;
                txtUrlDeLaImagen.Text = prod.ImagenUrl?.ToString() ?? "Sin Imagen";
                txtPrecio.Text = DecimalAString(prod.Precio);
                txtPorcentajeDeDescuento.Text = DecimalAString(prod.Descuento);
                txtStockDisponible.Text = IntAString(prod.Stock);
                txtFechaDeLanzamiento.Text = prod.FechaLanzamiento.ToString("yyyy-MM-dd");
                chkEsDigital.Checked = prod.EsDigital;

                CargarCategorias();
                SeleccionarCategoriasProducto(prod);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error cargar producto: " + ex.Message);
                pnlEditandoProducto.Visible = false;
                pnlNoEncontrado.Visible = true;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Catalogo.aspx");
        }
        private void aplicarPermisos()
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (user.Rol != Rol.Admin)
            {
                Session["ErrorNoPermisos"] = true;
                Response.Redirect("Error.aspx");
            }
        }
        public static string DecimalAString(decimal valor)
        {
            return valor.ToString();
        }

        public static decimal StringADecimal(string texto)
        {
            return decimal.TryParse(texto, out decimal valor)
                ? valor
                : 0m;
        }
        public static string IntAString(int? precio)
        {
            return (precio ?? 0).ToString();
        }

        public static int StringAInt(string texto)
        {
            return int.TryParse(texto, out int precio) ? precio : 0;
        }
        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            cblCategorias.DataSource = negocio.listar();
            cblCategorias.DataTextField = "NombreCategoria";
            cblCategorias.DataValueField = "IdCategoria";
            cblCategorias.DataBind();
        }
        private void SeleccionarCategoriasProducto(Producto prod)
        {
            foreach (Categoria categoria in prod.Categoria)
            {
                ListItem item = cblCategorias.Items.FindByValue(categoria.IdCategoria.ToString());

                if (item != null)
                    item.Selected = true;
            }
        }
        private List<Categoria> ObtenerCategoriasSeleccionadas()
        {
            List<Categoria> categorias = new List<Categoria>();

            foreach (ListItem item in cblCategorias.Items)
            {
                if (item.Selected)
                {
                    categorias.Add(new Categoria
                    {
                        IdCategoria = int.Parse(item.Value)
                    });
                }
            }

            return categorias;
        }

    }
}