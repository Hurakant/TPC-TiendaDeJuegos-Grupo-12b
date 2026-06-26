using dominio;
using Dominio;
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
        private const int DESCRIPCION_MAX_LENGTH = 1000;

        private int idProducto = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            aplicarPermisos();

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
                CargarAccesibilidades();

                GuardarIdsPersistidos(prod.Categoria.Select(c => c.IdCategoria).ToList());
                AplicarSeleccionPersistida();
                GuardarAccesibilidadesIdsPersistidos(prod.Accesibilidad.Select(a => a.IdAccesibilidad).ToList());
                AplicarSeleccionAccesibilidadPersistida();
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
            CargarAccesibilidades();
            GuardarIdsPersistidos(new List<int>());
            GuardarAccesibilidadesIdsPersistidos(new List<int>());
        }

        protected void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            SincronizarSeleccionVisibleConPersistido();
            CargarCategorias(txtBuscarCategoria.Text.Trim());
            AplicarSeleccionPersistida();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            OcultarError();

            string nombre = (txtNombre.Text ?? "").Trim();
            string descripcion = (txtDescripcion.Text ?? "").Trim();
            string urlImagen = (txtUrlDeLaImagen.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MostrarError("El nombre del producto es obligatorio.");
                return;
            }

            if (nombre.Length > 200)
            {
                MostrarError("El nombre no puede superar los 200 caracteres.");
                return;
            }

            if (descripcion.Length > DESCRIPCION_MAX_LENGTH)
            {
                MostrarError("La descripcion no puede superar los " + DESCRIPCION_MAX_LENGTH + " caracteres.");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MostrarError("El precio debe ser un valor numerico (ej: 1999.99).");
                return;
            }

            if (precio <= 0)
            {
                MostrarError("El precio debe ser mayor a 0.");
                return;
            }

            if (!decimal.TryParse(txtPorcentajeDeDescuento.Text, out decimal descuento))
            {
                MostrarError("El porcentaje de descuento debe ser un valor numerico.");
                return;
            }

            if (descuento < 0 || descuento > 100)
            {
                MostrarError("El porcentaje de descuento debe estar entre 0 y 100.");
                return;
            }

            if (!int.TryParse(txtStockDisponible.Text, out int stock))
            {
                MostrarError("El stock debe ser un numero entero.");
                return;
            }

            if (stock < 0)
            {
                MostrarError("El stock no puede ser negativo.");
                return;
            }

            SincronizarSeleccionVisibleConPersistido();
            SincronizarSeleccionAccesibilidadVisibleConPersistido();
            List<int> idsCategoriasSeleccionadas = ObtenerIdsPersistidos();
            List<int> idsAccesibilidadesSeleccionadas = ObtenerAccesibilidadesIdsPersistidos();

            if (idsCategoriasSeleccionadas.Count == 0)
            {
                MostrarError("Debe seleccionar al menos una categoria.");
                return;
            }

            bool esDigital = chkEsDigital.Checked;

            DateTime fechaLanzamiento;
            if (!DateTime.TryParse(txtFechaDeLanzamiento.Text, out fechaLanzamiento))
                fechaLanzamiento = DateTime.Today;

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
                    Categoria = idsCategoriasSeleccionadas.Select(id => new Categoria { IdCategoria = id }).ToList(),
                    Accesibilidad = idsAccesibilidadesSeleccionadas.Select(id => new Accesibilidad { IdAccesibilidad = id }).ToList()
                };

                if (idProducto > 0)
                {
                    negocio.modificar(prod);
                }
                else
                {
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

        private void CargarCategorias(string filtro = "")
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            cblCategorias.DataSource = negocio.listar(filtro, true);
            cblCategorias.DataTextField = "NombreCategoria";
            cblCategorias.DataValueField = "IdCategoria";
            cblCategorias.DataBind();
        }

        private List<int> ObtenerIdsPersistidos()
        {
            if (string.IsNullOrWhiteSpace(hdnCategoriasIds.Value))
                return new List<int>();

            return hdnCategoriasIds.Value
                .Split(',')
                .Select(s => int.TryParse(s, out int id) ? id : -1)
                .Where(id => id > 0)
                .Distinct()
                .ToList();
        }

        private void GuardarIdsPersistidos(List<int> ids)
        {
            hdnCategoriasIds.Value = string.Join(",", ids.Distinct());
        }

        private void SincronizarSeleccionVisibleConPersistido()
        {
            List<int> persistidos = ObtenerIdsPersistidos();

            foreach (ListItem item in cblCategorias.Items)
            {
                if (!int.TryParse(item.Value, out int id))
                    continue;

                if (item.Selected)
                {
                    if (!persistidos.Contains(id))
                        persistidos.Add(id);
                }
                else
                {
                    persistidos.Remove(id);
                }
            }

            GuardarIdsPersistidos(persistidos);
        }

        private void AplicarSeleccionPersistida()
        {
            List<int> persistidos = ObtenerIdsPersistidos();

            foreach (ListItem item in cblCategorias.Items)
            {
                if (int.TryParse(item.Value, out int id))
                    item.Selected = persistidos.Contains(id);
            }
        }

        private void CargarAccesibilidades(string filtro = "")
        {
            AccesibilidadNegocio negocio = new AccesibilidadNegocio();

            cblAccesibilidades.DataSource = negocio.listar(filtro, true);
            cblAccesibilidades.DataTextField = "NombreAccesibilidad";
            cblAccesibilidades.DataValueField = "IdAccesibilidad";
            cblAccesibilidades.DataBind();
        }

        protected void btnBuscarAccesibilidad_Click(object sender, EventArgs e)
        {
            SincronizarSeleccionAccesibilidadVisibleConPersistido();
            CargarAccesibilidades(txtBuscarAccesibilidad.Text.Trim());
            AplicarSeleccionAccesibilidadPersistida();
        }

        private List<int> ObtenerAccesibilidadesIdsPersistidos()
        {
            if (string.IsNullOrWhiteSpace(hdnAccesibilidadesIds.Value))
                return new List<int>();

            return hdnAccesibilidadesIds.Value
                .Split(',')
                .Select(s => int.TryParse(s, out int id) ? id : -1)
                .Where(id => id > 0)
                .Distinct()
                .ToList();
        }

        private void GuardarAccesibilidadesIdsPersistidos(List<int> ids)
        {
            hdnAccesibilidadesIds.Value = string.Join(",", ids.Distinct());
        }

        private void SincronizarSeleccionAccesibilidadVisibleConPersistido()
        {
            List<int> persistidos = ObtenerAccesibilidadesIdsPersistidos();

            foreach (ListItem item in cblAccesibilidades.Items)
            {
                if (!int.TryParse(item.Value, out int id)) continue;

                if (item.Selected)
                {
                    if (!persistidos.Contains(id)) persistidos.Add(id);
                }
                else
                {
                    persistidos.Remove(id);
                }
            }

            GuardarAccesibilidadesIdsPersistidos(persistidos);
        }

        private void AplicarSeleccionAccesibilidadPersistida()
        {
            List<int> persistidos = ObtenerAccesibilidadesIdsPersistidos();

            foreach (ListItem item in cblAccesibilidades.Items)
            {
                if (int.TryParse(item.Value, out int id))
                    item.Selected = persistidos.Contains(id);
            }
        }
    }
}