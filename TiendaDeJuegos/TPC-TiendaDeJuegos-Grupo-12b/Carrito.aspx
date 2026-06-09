<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/LoginStyle.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center login-container">
        <div class="col-md-7">

            <div class="login-card">

               
                <div class="text-center mb-4">

                    <asp:Image ID="imgCarrito" runat="server"
                        ImageUrl="Resources/Logo/logosfB.svg"
                        CssClass="loginlogo"
                        AlternateText="NovaHub" />

                    <h2 class="login-titulo">Tu carrito</h2>

                    <p class="login-subtitulo">
                        Revisá los productos seleccionados antes de comprar
                    </p>
                </div>

                <!-- LISTA DE PRODUCTOS (BÁSICO) -->
                <div class="mb-3">

                    <asp:GridView ID="gvCarrito" runat="server"
                        CssClass="table table-dark table-hover"
                        AutoGenerateColumns="true">
                    </asp:GridView>

                </div>

                <!-- TOTAL -->
                <div class="mb-4 text-end">

                    <asp:Label ID="lblTotalTexto" runat="server"
                        Text="Total:"
                        CssClass="login-label" />

                    <asp:Label ID="lblTotal" runat="server"
                        Text="$0"
                        CssClass="login-titulo" />

                </div>

                <!-- BOTONES -->
                <div class="d-grid gap-3">

                    <asp:Button ID="btnComprar" runat="server"
                        Text="Finalizar compra"
                        CssClass="btnIngresar"
                        OnClick="btnComprar_Click" />

                    <a href="Home.aspx" class="cancelar-link">Seguir comprando</a>

                    <asp:Button ID="btnVaciar" runat="server"
                        Text="Vaciar carrito"
                        CssClass="btn btn-outline-danger"
                        OnClick="btnVaciar_Click" />

                </div>

            </div>

        </div>
    </div>

</asp:Content>
