using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class AdminCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogueado"];
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (user.Rol != Rol.Admin)
            {
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}