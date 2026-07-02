<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true"
    CodeBehind="MisPedidos.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.MisPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/LoginStyle.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center login-container">
        <div class="col-md-8">

            <div class="login-card">

                <!-- HEADER -->
                <div class="text-center mb-4">

                    <asp:Image ID="imgPedidos" runat="server"
                        ImageUrl="Resources/Logo/logosfB.svg"
                        CssClass="loginlogo"
                        AlternateText="NovaHub" />

                    <h2 class="login-titulo">Mis pedidos</h2>

                    <p class="login-subtitulo">
                        Historial de tus compras realizadas
                    </p>
                </div>

                <div class="mb-3">

    <asp:GridView ID="gvPedidos" runat="server"
    CssClass="table table-dark table-hover"
    AutoGenerateColumns="False"
    OnRowDataBound="gvPedidos_RowDataBound"
    OnRowCommand="gvPedidos_RowCommand">

    <Columns>

        <asp:BoundField DataField="IdPedido" HeaderText="ID" />
        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
        <asp:BoundField DataField="Estado" HeaderText="Estado" />
        <asp:BoundField DataField="FormaDeEntrega" HeaderText="Entrega" />
        <asp:BoundField DataField="FormaDePagoTexto" HeaderText="Pago" />
        <asp:BoundField DataField="MontoTotal" HeaderText="Total" />
        <asp:BoundField DataField="DireccionTexto" HeaderText="Dirección" />

        <asp:TemplateField HeaderText="Acciones">
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Button ID="btnDetalle" runat="server"
                    Text="Ver detalle"
                    CommandName="VerDetalle"
                    CommandArgument='<%# Eval("IdPedido") %>'
                    CssClass="btn btn-sm btn-primary" />
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>

</asp:GridView>

                </div>

                <!-- MENSAJE -->
                <asp:Label ID="lblMensaje" runat="server"
                    CssClass="text-center text-success d-block" />

                <!-- BOTÓN VOLVER -->
                <div class="d-grid gap-3 mt-3">

                    <a href="Home.aspx" class="cancelar-link text-center">
                        Volver al inicio
                    </a>

                </div>

            </div>

        </div>
    </div>

</asp:Content>