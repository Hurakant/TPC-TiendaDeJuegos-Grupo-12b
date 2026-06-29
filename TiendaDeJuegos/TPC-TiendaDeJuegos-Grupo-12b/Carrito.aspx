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

                <!-- LISTA DE PRODUCTOS -->
                <div class="mb-3">

                    <asp:GridView ID="gvCarrito" runat="server"
    CssClass="table table-dark table-hover"
    AutoGenerateColumns="False"
    OnRowCommand="gvCarrito_RowCommand">

    <Columns>

        <asp:BoundField DataField="Nombre" HeaderText="Producto" />
        <asp:BoundField DataField="Precio" HeaderText="Precio" />
        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
        <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" />

        <asp:TemplateField HeaderText="Acción">
            <ItemTemplate>
                <asp:Button ID="btnEliminar"
                    runat="server"
                    Text="Eliminar"
                    CssClass="btn btn-danger btn-sm"
                    CommandName="Eliminar"
                    CommandArgument='<%# Eval("IdProducto") %>' />
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>

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
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" />
                    <asp:Label ID="lblDebug" runat="server" ForeColor="Red" />

                </div>

                <!-- BOTONES -->
                <div class="d-grid gap-3">

                    <asp:Button ID="btnComprar" runat="server"
                        Text="Finalizar compra"
                        CssClass="btnIngresar"
                        OnClick="btnComprar_Click" />

                    <a href="Catalogo.aspx" class="cancelar-link">Seguir comprando</a>

                    <asp:Button ID="btnVaciar" runat="server"
                        Text="Vaciar carrito"
                        CssClass="btn btn-outline-danger"
                        OnClick="btnVaciar_Click" />

                    <p>Forma de entrega:</p>
                    <asp:DropDownList ID="ddlEntrega" runat="server" OnSelectedIndexChanged="ddlEntrega_SelectedIndexChanged" AutoPostBack="true"/>

                    <div id="divDirecciones" runat="server" visible="false">
                        <p>Elija la direccion de envio:</p>
                        <asp:DropDownList
    ID="ddlDirecciones"
    runat="server"
    CssClass="form-select" />
                    </div>

    <asp:HyperLink
    ID="lnkDireccion"
    runat="server"
    NavigateUrl="MiCuenta.aspx"
    CssClass="btn btn-warning mt-2"
    Text="➕ Agregar una dirección"
    Visible="false" />

                    <p>Forma de pago:</p>
                    <asp:DropDownList ID="ddlPago" runat="server" />


                </div>

            </div>

        </div>
    </div>

</asp:Content>