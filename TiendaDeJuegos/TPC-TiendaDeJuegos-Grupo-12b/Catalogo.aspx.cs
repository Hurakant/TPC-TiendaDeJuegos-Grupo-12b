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
    public partial class Catalogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarAccesibilidad();
                AplicarFiltroDesdeQueryString();
                BusquedaDelHome();
                CargarProductos();
            }
        }

        private void BusquedaDelHome()
        {
            string buscar = Request.QueryString["Buscar"];
            if (!string.IsNullOrEmpty(buscar))
            {
                txtBuscar.Text = buscar;
            }
        }

        private void AplicarFiltroDesdeQueryString()
        {
            string idParam = Request.QueryString["idDeCategoria"];
            if (string.IsNullOrEmpty(idParam)) return;

            if (!int.TryParse(idParam, out int idCategoria)) return;

            ListItem item = chkCategorias.Items.FindByValue(idCategoria.ToString());
            if (item != null)
                item.Selected = true;
        }
        //Cargar checkbox categorias con buscador
        private void CargarCategorias(string filtro = "")
        {
            try
            {
                List<string> seleccionadasPrevias = new List<string>();
                foreach (ListItem item in chkCategorias.Items)
                {
                    if (item.Selected)
                        seleccionadasPrevias.Add(item.Value);
                }

                CategoriaNegocio negocio = new CategoriaNegocio();
                List<Categoria> categorias = negocio.listar(filtro);

                chkCategorias.Items.Clear();
                foreach (Categoria cat in categorias)
                {
                    ListItem item = new ListItem(cat.NombreCategoria, cat.IdCategoria.ToString());
                    item.Selected = seleccionadasPrevias.Contains(item.Value);
                    chkCategorias.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                lblResultados.Text = "Error al cargar categorías: " + ex.Message;
            }
        }

        //Carga el checkbox list de accesibilidad (no tiene buscador)
        private void CargarAccesibilidad()
        {
            try
            {
                AccesibilidadNegocio negocio = new AccesibilidadNegocio();
                List<Accesibilidad> accesibilidades = negocio.listar();

                chkAccesibilidad.Items.Clear();
                foreach (Accesibilidad acc in accesibilidades)
                {
                    ListItem item = new ListItem(acc.NombreAccesibilidad, acc.IdAccesibilidad.ToString());
                    chkAccesibilidad.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                lblResultados.Text = "Error al cargar accesibilidad: " + ex.Message;
            }
        }

        private void CargarProductos()
        {
            try
            {
                string texto = txtBuscar != null ? txtBuscar.Text.Trim() : "";
                int orden = ddlOrden != null ? int.Parse(ddlOrden.SelectedValue) : 0;

                //ids de categorias tildados
                List<int> idsCategorias = new List<int>();
                foreach (ListItem item in chkCategorias.Items)
                {
                    if (item.Selected)
                        idsCategorias.Add(int.Parse(item.Value));
                }

                //ids de accesibilidad tildados
                List<int> idsAccesibilidades = new List<int>();
                foreach (ListItem item in chkAccesibilidad.Items)
                {
                    if (item.Selected)
                        idsAccesibilidades.Add(int.Parse(item.Value));
                }

                ProductoNegocio negocio = new ProductoNegocio();
                List<Producto> productos = negocio.listarFiltrado(texto, idsCategorias, idsAccesibilidades, orden);

                rptProductos.DataSource = productos;
                rptProductos.DataBind();

                pnlVacio.Visible = productos.Count == 0;
                lblResultados.Text = productos.Count == 1
                    ? "1 producto encontrado"
                    : productos.Count + " productos encontrados";
            }
            catch (Exception ex)
            {
                lblResultados.Text = "Error al cargar productos: " + ex.Message;
                pnlVacio.Visible = true;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }

        //Buscador de categorias por nombre (LIKE en CategoriaNegocio.listar)
        protected void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            CargarCategorias(txtBuscarCategoria.Text.Trim());
        }

        protected void chkCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProductos();
        }

        protected void chkAccesibilidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProductos();
        }

        protected void ddlOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProductos();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in chkCategorias.Items)
                item.Selected = false;

            //Tambien se limpia la seleccion de accesibilidad
            foreach (ListItem item in chkAccesibilidad.Items)
                item.Selected = false;

            txtBuscar.Text = "";
            txtBuscarCategoria.Text = "";
            ddlOrden.SelectedValue = "0";

            CargarCategorias(); //Mostrar las categorias sin el filtro
            CargarProductos();
        }
        // Método para las categorías normales
        protected string ObtenerCategorias(object item)
        {
            Producto p = (Producto)item;

            if (p.Categoria == null || p.Categoria.Count == 0)
            {
                return "<span class=\"cat-chip\">Sin categoría</span>";
            }

            int maximoAMostrar = 4;
            string html = "";

            var categoriasAMostrar = p.Categoria.Take(maximoAMostrar).ToList();

            foreach (var cat in categoriasAMostrar)
            {
                html += $"<span class=\"cat-chip\">{cat.NombreCategoria}</span> ";
            }

            if (p.Categoria.Count > maximoAMostrar)
            {
                html += "<span class=\"cat-chip\">...</span>";
            }

            return html;
        }

        // Método para las categorías de accesibilidad
        protected string ObtenerAccesibilidad(object item)
        {
            Producto p = (Producto)item;

            if (p.Accesibilidad == null || p.Accesibilidad.Count == 0)
            {
                return "";
            }

            int maximoAMostrar = 4;
            string html = "";

            var accAMostrar = p.Accesibilidad.Take(maximoAMostrar).ToList();

            foreach (var acc in accAMostrar)
            {
                html += $"<span class=\"cat-chip acc-chip\" >{acc.NombreAccesibilidad}</span> ";
            }

            if (p.Accesibilidad.Count > maximoAMostrar)
            {
                html += "<span class=\"cat-chip\" >...</span>";
            }

            return html;
        }

        protected void rptProductos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink lnkEditar = (HyperLink)e.Item.FindControl("lnkEditarProducto");

                if (Session["usuarioLogueado"] != null)
                {
                    Usuario user = (Usuario)Session["usuarioLogueado"];

                    if (user.Rol == Rol.Admin || user.Rol == Rol.Vendedor)
                    {
                        lnkEditar.Visible = true;
                    }
                    else
                    {
                        lnkEditar.Visible = false;
                    }
                }
                else
                {
                    lnkEditar.Visible = false;
                }
            }
        }
    }
}