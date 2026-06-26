<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="AdminAccesibilidad.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.AdminAccesibilidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/AdminAccesibilidadStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="accesibilidad-contenedor">
        <div class="container">

            <div class="adminAccHeader">

                <div class="tituloConBoton">
                    <h2 class="accesibilidad-titulo">Administrar accesibilidades</h2>

                    <asp:Button Text="Volver" ID="btnVolver" CssClass="btnVolver" OnClick="btnVolver_Click" runat="server" />
                </div>

            </div>

            <div class="accesibilidad-barra-herramientas">
                <asp:TextBox ID="txtBuscar" runat="server"
                    CssClass="accesibilidad-campo-busqueda"
                    placeholder="Buscar accesibilidad por nombre...">
                </asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server"
                    Text="Buscar"
                    CssClass="accesibilidad-btn-buscar"
                    OnClick="btnBuscar_Click" />
                <asp:Button ID="btnAgregar" runat="server"
                    Text="+ Agregar accesibilidad"
                    CssClass="accesibilidad-btn-agregar"
                    OnClick="btnAgregar_Click"
                    CausesValidation="false" />
            </div>
            <asp:Panel ID="pnlVacio" runat="server" Visible="false" CssClass="accesibilidad-vacio">
                No se encontraron accesibilidades.
            </asp:Panel>

            <asp:HiddenField ID="hfSelectedId" runat="server" Value="" />

            <div class="accesibilidad-caja-edicion">
                <div class="accesibilidad-titulo-edicion">Detalle de la accesibilidad seleccionada</div>

                <asp:Panel ID="pnlSinSeleccion" runat="server" CssClass="accesibilidad-vacio" Style="margin: 0;">
                    Seleccioná una accesibilidad de la lista.
                </asp:Panel>

                <asp:Panel ID="pnlEdicion" runat="server" Visible="false">
                    <div class="accesibilidad-fila-edicion">
                        <div class="accesibilidad-campo">
                            <label>ID</label>
                            <span class="accesibilidad-caja-id">
                                <asp:Label ID="lblIdEditar" runat="server" Text=""></asp:Label>
                            </span>
                        </div>
                        <div class="accesibilidad-campo" style="flex: 1; min-width: 280px;">
                            <label>Nombre</label>
                            <asp:TextBox ID="txtNombreEditar" runat="server"
                                CssClass="accesibilidad-campo-nombre"
                                MaxLength="100">
                            </asp:TextBox>
                        </div>
                    </div>

                    <div class="accesibilidad-acciones-edicion">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios"
                            CssClass="accesibilidad-btn-guardar" OnClick="btnGuardar_Click" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                            CssClass="accesibilidad-btn-eliminar" OnClick="btnEliminar_Click"
                            OnClientClick="return confirm('¿Eliminar esta accesibilidad? (baja lógica)');"
                            CausesValidation="false" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar selección"
                            CssClass="accesibilidad-btn-limpiar" OnClick="btnLimpiar_Click"
                            CausesValidation="false" />
                    </div>

                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </asp:Panel>

                <asp:Panel ID="pnlAlta" runat="server" Visible="false">
                    <div class="accesibilidad-fila-edicion">
                        <div class="accesibilidad-campo" style="flex: 1; min-width: 280px;">
                            <label>Nombre de la nueva accesibilidad</label>
                            <asp:TextBox ID="txtNombreNueva" runat="server"
                                CssClass="accesibilidad-campo-nombre"
                                MaxLength="100">
                            </asp:TextBox>
                        </div>
                    </div>

                    <div class="accesibilidad-acciones-edicion">
                        <asp:Button ID="btnGuardarNueva" runat="server" Text="Guardar"
                            CssClass="accesibilidad-btn-guardar" OnClick="btnGuardarNueva_Click" />
                        <asp:Button ID="btnCancelarNueva" runat="server" Text="Cancelar"
                            CssClass="accesibilidad-btn-limpiar" OnClick="btnCancelarNueva_Click"
                            CausesValidation="false" />
                    </div>

                    <asp:Label ID="lblMensajeAlta" runat="server" CssClass="accesibilidad-mensaje-error" Text=""></asp:Label>
                </asp:Panel>
            </div>


            <asp:Repeater ID="rptAccesibilidades" runat="server"
                OnItemCommand="rptAccesibilidades_ItemCommand">
                <HeaderTemplate>
                    <div class="accesibilidad-lista">
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkAcc" runat="server"
                        CssClass='accesibilidad-tarjeta <%# (bool)Eval("Activo") ? "" : "inactiva" %> <%# ((int)Eval("IdAccesibilidad")) == (hfSelectedId.Value == "" ? -1 : int.Parse(hfSelectedId.Value)) ? "seleccionada" : "" %>'
                        CommandName="Seleccionar"
                        CommandArgument='<%# Eval("IdAccesibilidad") %>'>
                        <div class="accesibilidad-tarjeta-info">
                            <span class="accesibilidad-tarjeta-id">ID: <%# Eval("IdAccesibilidad") %></span>
                            <span class="accesibilidad-tarjeta-nombre"><%# Eval("NombreAccesibilidad") %></span>
                        </div>
                        <span class="accesibilidad-tarjeta-flecha"><i class="bi bi-chevron-right"></i></span>
                    </asp:LinkButton>
                </ItemTemplate>
                <FooterTemplate></div></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>
