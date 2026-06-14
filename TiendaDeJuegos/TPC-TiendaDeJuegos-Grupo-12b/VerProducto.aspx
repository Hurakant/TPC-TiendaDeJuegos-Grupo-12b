<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="VerProducto.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.VerProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/VerProductoStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="container">
            <asp:Panel runat="server">
                <p></p>
                <a href="Catalogo.aspx">Volver al catálogo</a>
            </asp:Panel>
            <asp:panel ID="pnlProducto" runat="server">
                <div class="row">
                    <!-- Columna izquierda IMAGEN -->

                    <div>
                        <div>
                            <asp:image imageurl="imgProducto" runat="server" CssClass="producto-imagen" AlternateText="Imagen del producto" />
                        </div>
                    </div>
                    <!-- info y botones -->
                    <div>
                        <div>
                            <h1 class="producto-titulo">
                                <!-- NOMBRE PRODUCTO -->
                                <asp:Label ID="lblNombre" runat="server" Text="ESTE ES EL NOMBRE DEL PRODUCTO"></asp:Label>
                            </h1>

                            <p class="producto-precio"> <!-- Precio del producto -->
                                $<asp:Label ID="lblPrecio" runat="server" Text="0.00"></asp:Label>
                            </p>

                            <div class="producto-cats">
                                <asp:repeater ID="rptCategorias" runat="server">
                                    <itemtemplate>
                                        <span>Capsula de categoria</span>
                                    </itemtemplate>
                                </asp:repeater>
                            </div>
                            <asp:label ID="lblStock" text="Aqui va el stock" runat="server" /> <!-- STOCK -->
                            <!-- Boton carrito -->
                            <asp:button ID="btnAgregarCarrito" text="Añadir al carrito" runat="server" /> 

                            <a href="Catalogo.aspx">Volver al catalogo</a>

                        </div>
                    </div>

                </div>
                <!-- Descripcion -->
                <div>
                    <div>Descripción</div>
                        <asp:textbox ID="txtDescripcion" runat="server" Textmode="MultiLine" ReadOnly="True" Rows="10" 
                            text="Aqui va la descripcion del producto"/>
                </div>
            </asp:panel>
            <%-- Aqui termina el panel producto --%>
        </div>
    </div>
</asp:Content>
