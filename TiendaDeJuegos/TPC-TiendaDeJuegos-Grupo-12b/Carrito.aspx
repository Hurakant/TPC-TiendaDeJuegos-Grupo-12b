<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/LoginStyle.css" rel="stylesheet" />
    <link href="Resources/StyleTPC/CarritoStyle.css" rel="stylesheet" />
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
                <%--<div class="mb-3">

                    <asp:GridView ID="gvCarrito" runat="server"
                        CssClass="carrito-grid"
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

                    </asp:GridView>--%>

                <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" />

                <asp:Repeater ID="rptCarrito" runat="server" OnItemCommand="rptCarrito_ItemCommand">
                    <HeaderTemplate>

                        <div class="carrito">

                            <div class="carrito-header">
                                <div>Producto</div>
                                <div>Precio</div>
                                <div>Cantidad</div>
                                <div>Subtotal</div>
                                <div></div>
                            </div>
                    </HeaderTemplate>

                    <ItemTemplate>

                        <div class="carrito-item">

                            <div class="producto">
                                <%# Eval("Nombre") %>
                            </div>

                            <div>
                                $ <%# Eval("Precio") %>
                            </div>

                            <div>
                                <%# Eval("Cantidad") %>
                            </div>

                            <div class="subtotal">
                                $ <%# Eval("Subtotal", "{0:N2}") %>
                            </div>

                            <div>
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btnEliminar" CommandName="Eliminar" CommandArgument='<%# Eval("IdProducto") %>' />
                            </div>

                        </div>

                    </ItemTemplate>

                    <FooterTemplate>
                        </div>

   
                    </FooterTemplate>

                </asp:Repeater>

                <!-- TOTAL -->
                <div class="carrito-total">

                    <span class="titulo-total">TOTAL</span>

                    <asp:Label
                        ID="lblTotal"
                        runat="server"
                        CssClass="precio-total"
                        Text="$0" />

                </div>

                <%--ddl--%>
                <div Class="ddl-carrito">

                <p class="titulo-opcion" >Forma de entrega:</p>
                <asp:DropDownList ID="ddlEntrega" CssClass="select-carrito" runat="server" OnSelectedIndexChanged="ddlEntrega_SelectedIndexChanged" AutoPostBack="true" />

                <div id="divDirecciones" runat="server" visible="false">
                    <p>Elija la direccion de envio:</p>
                    <asp:DropDownList
                        ID="ddlDirecciones"
                        runat="server"
                        CssClass="select-carrito"  />
                </div>

                <asp:HyperLink
                    ID="lnkDireccion"
                    runat="server"
                    NavigateUrl="MiCuenta.aspx"
                    CssClass="btn btn-warning mt-2"
                    Text="➕ Agregar una dirección"
                    Visible="false" />

                <p class="titulo-opcion" >Forma de pago:</p>
                <asp:DropDownList ID="ddlPago" runat="server" CssClass="select-carrito" />
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

                </div>

            </div>

        </div>
    </div>

</asp:Content>
