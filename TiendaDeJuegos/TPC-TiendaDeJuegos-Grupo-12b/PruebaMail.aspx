<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="PruebaMail.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.PruebaMail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container py-5" style="max-width: 600px;">
        <h2 class="text-white mb-4">Probar envío de Mail (Mailtrap)</h2>

        <div class="mb-3">
            <label class="form-label text-white-50">Destino</label>
            <asp:TextBox ID="txtPara" runat="server" CssClass="form-control" placeholder="email@dominio.com"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label text-white-50">Asunto</label>
            <asp:TextBox ID="txtAsunto" runat="server" CssClass="form-control" Text="Hola desde NovaHub"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label text-white-50">Cuerpo</label>
            <asp:TextBox ID="txtCuerpo" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6">Este es un email de prueba.</asp:TextBox>
        </div>

        <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="btn btn-primary" OnClick="btnEnviar_Click" />

        <asp:Label ID="lblResultado" runat="server" CssClass="d-block mt-3"></asp:Label>

        <p class="text-white-50 mt-4 small">Revisá los logs en <a href="https://mailtrap.io/sending/email_logs" target="_blank" class="text-info">mailtrap.io/sending/email_logs</a></p>
    </div>

</asp:Content>
