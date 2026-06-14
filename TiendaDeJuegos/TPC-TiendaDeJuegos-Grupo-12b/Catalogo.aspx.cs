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
                CargarProductos();
            }
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
        protected string ObtenerCategoria(object item)
        {
            Producto p = (Producto)item;

            return p.Categoria.Count > 0
                ? p.Categoria[0].NombreCategoria
                : "Sin categoría";
        }
    }
}