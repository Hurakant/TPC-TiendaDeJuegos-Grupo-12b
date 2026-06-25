using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_TiendaDeJuegos_Grupo_12b
{
    public partial class PruebaMail : System.Web.UI.Page
    {
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string para = txtPara.Text.Trim();
            string asunto = txtAsunto.Text.Trim();
            string cuerpo = txtCuerpo.Text;

            if (string.IsNullOrWhiteSpace(para))
            {
                lblResultado.Text = "Indicá un destinatario.";
                lblResultado.ForeColor = System.Drawing.Color.Red;
                return;
            }

            bool ok = EmailService.Enviar(para, asunto, cuerpo, out string error);

            if (ok)
            {
                lblResultado.Text = "Email enviado. Revisá Mailtrap.";
                lblResultado.ForeColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                lblResultado.Text = "Error: " + error;
                lblResultado.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}