using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            
            Response.Write("Compra realizada");
        }

        protected void btnVaciar_Click(object sender, EventArgs e)
        {
            Response.Write("Carrito vaciado");
        }
    }
}