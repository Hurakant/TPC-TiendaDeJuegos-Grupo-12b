using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class MiCuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                Usuario user = (Usuario)Session["usuarioLogueado"];

                lblNombre.Text = user.Nombre + " " + user.Apellido;
                lblEmail.Text = user.Email;
                lblRol.Text = user.Rol.ToString();
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Home.aspx");
        }
    }
}