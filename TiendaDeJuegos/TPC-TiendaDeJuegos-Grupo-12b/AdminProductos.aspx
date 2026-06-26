<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="AdminProductos.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.AdminProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/AdminProductosStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />

    <div class="productos-contenedor">
        <div class="container">

            <div class="adminProdHeader">
                <div class="tituloConBoton">
                    <h2 class="productos-titulo">
                        <asp:Literal ID="litTitulo" runat="server" Text="Editar Producto" />
                    </h2>
                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btnVolver" OnClick="btnVolver_Click" CausesValidation="false" />
                </div>
            </div>

            <asp:Panel ID="pnlNoEncontrado" runat="server" Visible="false" CssClass="productos-vacio">
                <p>El producto solicitado no existe o no está disponible.</p>
                <a href="Catalogo.aspx">Volver al catálogo</a>
            </asp:Panel>

            <asp:Panel ID="pnlEditandoProducto" runat="server">
                <div class="productos-caja-edicion">

                    <div class="productos-fila-edicion">
                        <div class="productos-campo" style="flex: 1; min-width: 280px;">
                            <label>Nombre del producto</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="productos-input" />
                        </div>
                        <div class="productos-campo" style="flex: 1; min-width: 280px;">
                            <label>Url de la imagen</label>
                            <asp:TextBox ID="txtUrlDeLaImagen" runat="server" CssClass="productos-input" />
                        </div>
                    </div>

                    <div class="productos-fila-edicion">
                        <div class="productos-campo" style="flex: 1; min-width: 280px;">
                            <label>Descripción</label>
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="productos-input productos-textarea" TextMode="MultiLine" Rows="3" />
                        </div>
                    </div>

                    <div class="productos-fila-edicion">
                        <div class="productos-campo">
                            <label>Precio</label>
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="productos-input" />
                        </div>
                        <div class="productos-campo">
                            <label>Descuento (%)</label>
                            <asp:TextBox ID="txtPorcentajeDeDescuento" runat="server" CssClass="productos-input" />
                        </div>
                        <div class="productos-campo">
                            <label>Stock disponible</label>
                            <asp:TextBox ID="txtStockDisponible" runat="server" CssClass="productos-input" />
                        </div>
                        <div class="productos-campo">
                            <label>Fecha de lanzamiento</label>
                            <asp:TextBox ID="txtFechaDeLanzamiento" runat="server" CssClass="productos-input" TextMode="Date" />
                        </div>
                        <div class="productos-campo">
                            <label>Es digital</label>
                            <asp:CheckBox ID="chkEsDigital" runat="server" CssClass="productos-checkbox" />
                        </div>
                    </div>

                        <div class="productos-grupo-doble">
    
                            <div class="productos-campo productos-columna">
                                <label>Categorías</label>
                                <asp:UpdatePanel ID="updCategorias" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="productos-buscador-cat">
                                            <asp:TextBox ID="txtBuscarCategoria" runat="server" CssClass="productos-campo-busqueda" placeholder="Buscar categoría..." />
                                            <asp:Button ID="btnBuscarCategoria" runat="server" Text="Buscar" CssClass="productos-btn-buscar" CausesValidation="false" OnClick="btnBuscarCategoria_Click" />
                                        </div>
                                        <asp:UpdateProgress ID="updProgCategorias" runat="server" AssociatedUpdatePanelID="updCategorias">
                                            <ProgressTemplate><span class="productos-buscando">Buscando...</span></ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:CheckBoxList ID="cblCategorias" runat="server" RepeatLayout="UnorderedList" CssClass="productos-chk-categorias" />
                                        <asp:HiddenField ID="hdnCategoriasIds" runat="server" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnBuscarCategoria" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="productos-campo productos-columna">
                                <label>Accesibilidades</label>
                                <asp:UpdatePanel ID="updAccesibilidades" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="productos-buscador-cat">
                                            <asp:TextBox ID="txtBuscarAccesibilidad" runat="server" CssClass="productos-campo-busqueda" placeholder="Buscar accesibilidad..." />
                                            <asp:Button ID="btnBuscarAccesibilidad" runat="server" Text="Buscar" CssClass="productos-btn-buscar" CausesValidation="false" OnClick="btnBuscarAccesibilidad_Click" />
                                        </div>
                                        <asp:UpdateProgress ID="updProgAccesibilidades" runat="server" AssociatedUpdatePanelID="updAccesibilidades">
                                            <ProgressTemplate><span class="productos-buscando">Buscando...</span></ProgressTemplate>
                                        </asp:UpdateProgress>
                
                                        <asp:CheckBoxList ID="cblAccesibilidades" runat="server" RepeatLayout="UnorderedList" CssClass="productos-chk-categorias" />
                                        <asp:HiddenField ID="hdnAccesibilidadesIds" runat="server" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnBuscarAccesibilidad" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>

                    <asp:Label ID="lblMsjError" runat="server" CssClass="productos-mensaje-error" Visible="false"></asp:Label>

                    <div class="productos-acciones-edicion">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios" CssClass="productos-btn-guardar" OnClick="btnGuardar_Click" />
                    </div>

                </div>
            </asp:Panel>

        </div>
    </div>

</asp:Content>
