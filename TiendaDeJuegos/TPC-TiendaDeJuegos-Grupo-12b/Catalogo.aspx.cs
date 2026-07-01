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
    public partial class Catalogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                AplicarFiltroDesdeQueryString();
                CargarProductos();
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

        private void CargarCategorias()
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                List<Categoria> categorias = negocio.listar();

                chkCategorias.Items.Clear();
                foreach (Categoria cat in categorias)
                {
                    ListItem item = new ListItem(cat.NombreCategoria, cat.IdCategoria.ToString());
                    chkCategorias.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                lblResultados.Text = "Error al cargar categorías: " + ex.Message;
            }
        }

        private void CargarProductos()
        {
            try
            {
                string texto = txtBuscar != null ? txtBuscar.Text.Trim() : "";
                int orden = ddlOrden != null ? int.Parse(ddlOrden.SelectedValue) : 0;

                List<int> idsCategorias = new List<int>();
                foreach (ListItem item in chkCategorias.Items)
                {
                    if (item.Selected)
                        idsCategorias.Add(int.Parse(item.Value));
                }

                ProductoNegocio negocio = new ProductoNegocio();
                List<Producto> productos = negocio.listarFiltrado(texto, idsCategorias, orden);

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

        protected void chkCategorias_SelectedIndexChanged(object sender, EventArgs e)
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

            txtBuscar.Text = "";
            ddlOrden.SelectedValue = "0";

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