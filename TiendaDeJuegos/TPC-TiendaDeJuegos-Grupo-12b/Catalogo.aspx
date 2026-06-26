<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.Catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/CatalogoStyle.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="catalogo-wrapper">
        <div class="container-fluid">

            <div class="row">

                <!-- COLUMNA IZQUIERDA: FILTROS POR CATEGORIA -->
                <div class="col-lg-2 col-md-3 mb-3">
                    <div class="catalogo-sidebar">
                        <div class="catalogo-sidebar-title">Filtrar por categoria:</div>
                        <asp:CheckBoxList ID="chkCategorias" runat="server"
                            CssClass="chk-filtros"
                            RepeatLayout="UnorderedList"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="chkCategorias_SelectedIndexChanged">
                        </asp:CheckBoxList>
                        <asp:Button ID="btnLimpiarFiltros" runat="server"
                            Text="Limpiar filtros"
                            CssClass="catalogo-sidebar-clear"
                            OnClick="btnLimpiarFiltros_Click"
                            CausesValidation="false" />
                    </div>
                </div>

                <!-- COLUMNA CENTRAL: BUSQUEDA Y LISTADO -->
                <div class="col-lg-10 col-md-9">

                    <!-- BARRA SUPERIOR CENTRADA: BUSCAR Y ORDENAR -->
                    <div class="catalogo-toolbar">
                        <asp:TextBox ID="txtBuscar" runat="server"
                            CssClass="catalogo-search-input"
                            placeholder="Buscar juego por nombre...">
                        </asp:TextBox>

                        <asp:Button ID="btnBuscar" runat="server"
                            Text="Buscar"
                            CssClass="catalogo-btn-buscar"
                            OnClick="btnBuscar_Click" />

                        <asp:DropDownList ID="ddlOrden" runat="server"
                            CssClass="catalogo-orden-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlOrden_SelectedIndexChanged">
                            <asp:ListItem Value="0">Ordenar por precio</asp:ListItem>
                            <asp:ListItem Value="1">Menor a mayor</asp:ListItem>
                            <asp:ListItem Value="2">Mayor a menor</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <asp:Label ID="lblResultados" runat="server"
                        CssClass="catalogo-results-info"
                        Text="">
                    </asp:Label>

                    <!-- LISTADO DE PRODUCTOS -->
                    <asp:Repeater ID="rptProductos" runat="server">
                        <HeaderTemplate>
                            <div class="catalogo-lista">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="game-row-card">
                                <img src='<%# Eval("ImagenUrl") %>'
                                    class="game-row-img"
                                    alt='<%# Eval("Nombre") %>'
                                    onerror="this.src='https://via.placeholder.com/180x130/0d0d14/666666?text=Sin+Imagen';" />
                                <div class="game-row-body">
                                    <h5 class="game-row-title"><%# Eval("Nombre") %></h5>
                                    <p class="game-row-price">$ <%# Eval("Precio", "{0:N2}") %></p>
                                    <div class="game-row-cats">
                                        <div style="margin-bottom: 5px;">
                                            <%# ObtenerCategorias(Container.DataItem) %>
                                        </div>

                                        <div>
                                            <%# ObtenerAccesibilidad(Container.DataItem) %>
                                        </div>
                                    </div>
                                </div>
                                <div class="game-row-actions">
                                    <asp:HyperLink ID="lnkEditarProducto" runat="server"
                                        CssClass="game-row-link"
                                        NavigateUrl='<%# "AdminProductos.aspx?id=" + Eval("IdProducto") %>'>
                                        EditarProducto
                                    </asp:HyperLink>
                                    <asp:HyperLink ID="lnkVerDetalle" runat="server"
                                        CssClass="game-row-link"
                                        NavigateUrl='<%# "VerProducto.aspx?id=" + Eval("IdProducto") %>'>
                                        Ver detalle
                                    </asp:HyperLink>
                                </div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                       
                        </FooterTemplate>
                    </asp:Repeater>

                    <asp:Panel ID="pnlVacio" runat="server" Visible="false" CssClass="catalogo-empty">
                        No se encontraron productos con los filtros aplicados.
                   
                    </asp:Panel>

                </div>

            </div>
        </div>
    </div>

</asp:Content>
