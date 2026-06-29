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
    public partial class VerProducto : System.Web.UI.Page
    {
        private int idProducto = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["id"], out idProducto) || idProducto <= 0)
            {
                pnlProducto.Visible = false;
                pnlNoEncontrado.Visible = true;
                return;
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
                    pnlProducto.Visible = false;
                    pnlNoEncontrado.Visible = true;
                    return;
                }

                lblNombre.Text = prod.Nombre;
                lblPrecio.Text = prod.Precio.ToString("N2");
                txtDescripcion.Text = string.IsNullOrWhiteSpace(prod.Descripcion)
                    ? "Sin descripción disponible."
                    : prod.Descripcion;

                if (!string.IsNullOrEmpty(prod.ImagenUrl))
                    imgProducto.ImageUrl = prod.ImagenUrl;
                else
                    imgProducto.ImageUrl = "https://via.placeholder.com/500x420/0d0d14/666666?text=Sin+Imagen";

                rptCategorias.DataSource = prod.Categoria;
                rptCategorias.DataBind();

                if (prod.Stock > 0)
                {
                    lblStock.Text = "Stock disponible: " + prod.Stock + " unidades";
                    btnAgregarCarrito.Enabled = true;
                }
                else
                {
                    lblStock.Text = "Sin stock disponible";
                    btnAgregarCarrito.Enabled = false;
                    btnAgregarCarrito.Text = "Sin stock";
                }


                Usuario user = (Usuario)Session["usuarioLogueado"];

                if (user != null && user.Rol != Rol.Cliente)
                {
                    btnAgregarCarrito.Enabled = false;
                    btnAgregarCarrito.Text = "Solo para clientes";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error cargar producto: " + ex.Message);
                pnlProducto.Visible = false;
                pnlNoEncontrado.Visible = true;
            }
        }
        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = (Usuario)Session["usuarioLogueado"];

                if (user == null)
                {
                    lblMensaje.Text = "Inicie sesión para continuar con la compra.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (user.Rol != Rol.Cliente)
                {
                    lblMensaje.Text = "Solo los clientes pueden agregar productos al carrito.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                string key = "Carrito_" + user.IdUsuario;

                ProductoNegocio negocio = new ProductoNegocio();
                dominio.Producto prod = negocio.listarPorId(idProducto);

                if (prod == null || prod.Stock <= 0)
                {
                    lblMensaje.Text = "No se pudo agregar: sin stock.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (Session[key] == null)
                    Session[key] = new dominio.Carrito();

                dominio.Carrito carrito = (dominio.Carrito)Session[key];

                CarritoNegocio carritoNegocio = new CarritoNegocio(carrito);
                carritoNegocio.AgregarProducto(prod, 1);

                lblMensaje.Text = "✔ Producto agregado al carrito";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    
    }
}