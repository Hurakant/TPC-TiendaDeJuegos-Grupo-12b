<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="VerProducto.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.VerProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/VerProductoStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="producto-wrapper">
        <div class="container">

            <asp:Panel ID="pnlNoEncontrado" runat="server" Visible="false">
                <p>El producto solicitado no existe o no está disponible.</p>
                <a href="Catalogo.aspx">Volver al catálogo</a>
            </asp:Panel>

            <asp:Panel ID="pnlProducto" runat="server">
                <div class="row">
                    <!-- Columna izquierda IMAGEN -->

                    <div class="col-md-5 mb-3">
                        <div class="producto-imagen-box">
                            <asp:Image ID="imgProducto" runat="server" CssClass="producto-imagen" AlternateText="Imagen del producto" />
                        </div>
                    </div>
                    <!-- info y botones -->
                    <div class="col-md-7 mb-3">
                        <div class="producto-info-box">
                            <h1 class="producto-titulo">
                                <!-- NOMBRE PRODUCTO -->
                                <asp:Label ID="lblNombre" runat="server" Text="ESTE ES EL NOMBRE DEL PRODUCTO"></asp:Label>
                            </h1>

                            <p class="producto-precio">
                                <!-- Precio del producto -->
                                $<asp:Label ID="lblPrecio" runat="server" Text="0.00"></asp:Label>
                            </p>

                            <div class="producto-cats">
                                <asp:Repeater ID="rptCategorias" runat="server">
                                    <ItemTemplate>
                                        <span class="cat-capsula">Capsula de categoria</span>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <asp:Label ID="lblStock" Text="Aqui va el stock" runat="server" CssClass="producto-stock" />
                            <!-- STOCK -->
                            <!-- Boton carrito -->
                            <asp:Button ID="btnAgregarCarrito" Text="Añadir al carrito" runat="server" CssClass="btnIngresar producto-btn-carrito" OnClick="btnAgregarCarrito_Click" />

                            <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="producto-mensaje"></asp:Label>

                            <a href="Catalogo.aspx" class="producto-volver">Volver al catalogo</a>

                        </div>
                    </div>

                </div>
                <!-- Descripcion -->
                <div class="producto-desc-box">
                    <div class="producto-desc-titulo">Descripción</div>
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" ReadOnly="True" Rows="10" CssClass="producto-desc-texto"
                        Text="Aqui va la descripcion del producto" />
                </div>
            </asp:Panel>
            <%-- Aqui termina el panel producto --%>
        </div>
    </div>
</asp:Content>
