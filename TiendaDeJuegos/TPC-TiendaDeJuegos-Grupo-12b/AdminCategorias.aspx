<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="AdminCategorias.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.AdminCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/AdminCategoriasStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="categorias-contenedor">
        <div class="container">

            <div class="adminCatHeader">

                <div class="tituloConBoton">
                    <h2 class="categorias-titulo">Administrar categorías</h2>

                    <asp:Button Text="Volver" ID="btnVolver" CssClass="btnVolver"  OnClick="btnVolver_Click" runat="server" />
                </div>

            </div>

            <div class="categorias-barra-herramientas">
                <asp:TextBox ID="txtBuscar" runat="server"
                    CssClass="categorias-campo-busqueda"
                    placeholder="Buscar categoría por nombre...">
                </asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server"
                    Text="Buscar"
                    CssClass="categorias-btn-buscar"
                    OnClick="btnBuscar_Click" />
                <asp:Button ID="btnAgregar" runat="server"
                    Text="+ Agregar categoría"
                    CssClass="categorias-btn-agregar"
                    OnClick="btnAgregar_Click"
                    CausesValidation="false" />
            </div>
            <asp:Panel ID="pnlVacio" runat="server" Visible="false" CssClass="categorias-vacio">
                No se encontraron categorías.
            </asp:Panel>

            <asp:HiddenField ID="hfSelectedId" runat="server" Value="" />

            <div class="categorias-caja-edicion">
                <div class="categorias-titulo-edicion">Detalle de la categoría seleccionada</div>

                <asp:Panel ID="pnlSinSeleccion" runat="server" CssClass="categorias-vacio" Style="margin: 0;">
                    Seleccioná una categoría de la lista.
                </asp:Panel>

                <asp:Panel ID="pnlEdicion" runat="server" Visible="false">
                    <div class="categorias-fila-edicion">
                        <div class="categorias-campo">
                            <label>ID</label>
                            <span class="categorias-caja-id">
                                <asp:Label ID="lblIdEditar" runat="server" Text=""></asp:Label>
                            </span>
                        </div>
                        <div class="categorias-campo" style="flex: 1; min-width: 280px;">
                            <label>Nombre</label>
                            <asp:TextBox ID="txtNombreEditar" runat="server"
                                CssClass="categorias-campo-nombre"
                                MaxLength="100">
                            </asp:TextBox>
                        </div>
                    </div>

                    <div class="categorias-acciones-edicion">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios"
                            CssClass="categorias-btn-guardar" OnClick="btnGuardar_Click" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                            CssClass="categorias-btn-eliminar" OnClick="btnEliminar_Click"
                            OnClientClick="return confirm('¿Eliminar esta categoría? (baja lógica)');"
                            CausesValidation="false" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar selección"
                            CssClass="categorias-btn-limpiar" OnClick="btnLimpiar_Click"
                            CausesValidation="false" />
                    </div>

                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </asp:Panel>

                <asp:Panel ID="pnlAlta" runat="server" Visible="false">
                    <div class="categorias-fila-edicion">
                        <div class="categorias-campo" style="flex: 1; min-width: 280px;">
                            <label>Nombre de la nueva categoría</label>
                            <asp:TextBox ID="txtNombreNueva" runat="server"
                                CssClass="categorias-campo-nombre"
                                MaxLength="100">
                            </asp:TextBox>
                        </div>
                    </div>

                    <div class="categorias-acciones-edicion">
                        <asp:Button ID="btnGuardarNueva" runat="server" Text="Guardar"
                            CssClass="categorias-btn-guardar" OnClick="btnGuardarNueva_Click" />
                        <asp:Button ID="btnCancelarNueva" runat="server" Text="Cancelar"
                            CssClass="categorias-btn-limpiar" OnClick="btnCancelarNueva_Click"
                            CausesValidation="false" />
                    </div>

                    <asp:Label ID="lblMensajeAlta" runat="server" CssClass="categorias-mensaje-error" Text=""></asp:Label>
                </asp:Panel>
            </div>


            <asp:Repeater ID="rptCategorias" runat="server"
                OnItemCommand="rptCategorias_ItemCommand">
                <HeaderTemplate>
                    <div class="categorias-lista">
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkCat" runat="server"
                        CssClass='categorias-tarjeta <%# (bool)Eval("Activo") ? "" : "inactiva" %> <%# ((int)Eval("IdCategoria")) == (hfSelectedId.Value == "" ? -1 : int.Parse(hfSelectedId.Value)) ? "seleccionada" : "" %>'
                        CommandName="Seleccionar"
                        CommandArgument='<%# Eval("IdCategoria") %>'>
                        <div class="categorias-tarjeta-info">
                            <span class="categorias-tarjeta-id">ID: <%# Eval("IdCategoria") %></span>
                            <span class="categorias-tarjeta-nombre"><%# Eval("NombreCategoria") %></span>
                        </div>
                        <span class="categorias-tarjeta-flecha"><i class="bi bi-chevron-right"></i></span>
                    </asp:LinkButton>
                </ItemTemplate>
                <FooterTemplate></div></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>

