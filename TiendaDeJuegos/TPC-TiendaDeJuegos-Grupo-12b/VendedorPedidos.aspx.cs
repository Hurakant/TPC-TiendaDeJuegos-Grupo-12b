using dominio;
using System;
using System.Data;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class VendedorPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // CONTROL DE ACCESO CORRECTO
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            Usuario user = (Usuario)Session["usuarioLogueado"];

            if (user.Rol != Rol.Vendedor)
            {
                Response.Redirect("Home.aspx");
                return;
            }

            // SOLO si pasa el control entra acá
            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        private void CargarPedidos()
        {
            // Falta conectar a la base esto es somulacion)
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Cliente");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Estado");
            dt.Columns.Add("Total");

            dt.Rows.Add("1", "Juan Perez", "2026-06-17", "Pendiente", "1500");
            dt.Rows.Add("2", "Maria Lopez", "2026-06-16", "Enviado", "3200");

            gvPedidos.DataSource = dt;
            gvPedidos.DataBind();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "Estado actualizado (conectar a BD después)";
        }
    }
}