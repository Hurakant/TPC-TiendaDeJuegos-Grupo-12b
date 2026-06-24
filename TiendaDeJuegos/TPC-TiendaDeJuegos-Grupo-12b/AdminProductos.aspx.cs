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
            aplicarPermisos();

            // Si no viene un id valido en la querystring, estamos en modo "Nuevo producto"
            if (!int.TryParse(Request.QueryString["id"], out idProducto) || idProducto <= 0)
            {
                idProducto = 0;
            }

            if (!IsPostBack)
            {
                if (idProducto > 0)
                    CargarProducto();
                else
                    CargarProductoNuevo();
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

                litTitulo.Text = "Editar Producto";

                txtNombre.Text = prod.Nombre;
                txtDescripcion.Text = prod.Descripcion;
                txtUrlDeLaImagen.Text = string.IsNullOrWhiteSpace(prod.ImagenUrl) ? "" : prod.ImagenUrl;
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

        private void CargarProductoNuevo()
        {
            pnlNoEncontrado.Visible = false;
            pnlEditandoProducto.Visible = true;

            litTitulo.Text = "Nuevo Producto";
            txtFechaDeLanzamiento.Text = DateTime.Today.ToString("yyyy-MM-dd");

            CargarCategorias();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            OcultarError();

            string nombre = (txtNombre.Text ?? "").Trim();
            string descripcion = (txtDescripcion.Text ?? "").Trim();
            string urlImagen = (txtUrlDeLaImagen.Text ?? "").Trim();

            decimal precio = StringADecimal(txtPrecio.Text);
            decimal descuento = StringADecimal(txtPorcentajeDeDescuento.Text);
            int stock = StringAInt(txtStockDisponible.Text);
            bool esDigital = chkEsDigital.Checked;

            DateTime fechaLanzamiento;
            if (!DateTime.TryParse(txtFechaDeLanzamiento.Text, out fechaLanzamiento))
                fechaLanzamiento = DateTime.Today;

            // ---- Validaciones basicas ----
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MostrarError("El nombre del producto es obligatorio.");
                return;
            }

            if (precio <= 0)
            {
                MostrarError("El precio debe ser mayor a 0.");
                return;
            }

            if (descuento < 0 || descuento > 100)
            {
                MostrarError("El porcentaje de descuento debe estar entre 0 y 100.");
                return;
            }

            if (stock < 0)
            {
                MostrarError("El stock no puede ser negativo.");
                return;
            }

            List<Categoria> categoriasSeleccionadas = ObtenerCategoriasSeleccionadas();
            if (categoriasSeleccionadas.Count == 0)
            {
                MostrarError("Debe seleccionar al menos una categoria.");
                return;
            }

            ProductoNegocio negocio = new ProductoNegocio();

            try
            {
                if (negocio.existeNombreProducto(nombre, idProducto))
                {
                    MostrarError("Ya existe un producto con ese nombre.");
                    return;
                }

                Producto prod = new Producto
                {
                    IdProducto = idProducto,
                    Nombre = nombre,
                    Descripcion = descripcion,
                    ImagenUrl = urlImagen,
                    Precio = precio,
                    Descuento = descuento,
                    Stock = stock,
                    FechaLanzamiento = fechaLanzamiento,
                    EsDigital = esDigital,
                    Activo = true,
                    Categoria = categoriasSeleccionadas
                };

                if (idProducto > 0)
                {
                    // ---- MODIFICAR ----
                    negocio.modificar(prod);
                }
                else
                {
                    // ---- CREAR ----
                    negocio.agregar(prod);
                    idProducto = prod.IdProducto;
                }

                Response.Redirect("Catalogo.aspx");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error guardar producto: " + ex.Message);
                MostrarError("Ocurrio un error al guardar el producto. Intente nuevamente.");
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Catalogo.aspx");
        }

        private void aplicarPermisos()
        {
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            Usuario user = (Usuario)Session["usuarioLogueado"];

            if (user.Rol != Rol.Admin)
            {
                Session["ErrorNoPermisos"] = true;
                Response.Redirect("Error.aspx");
            }
        }

        private void MostrarError(string mensaje)
        {
            lblMsjError.Text = mensaje;
            lblMsjError.Visible = true;
        }

        private void OcultarError()
        {
            lblMsjError.Visible = false;
            lblMsjError.Text = "";
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