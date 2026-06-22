<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="AdminProductos.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.AdminProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/EditarUserAdminStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Panel editar producto --%>
    <asp:Panel ID="pnlNoEncontrado" runat="server" Visible="false">
        <p>El producto solicitado no existe o no está disponible.</p>
        <a href="Catalogo.aspx">Volver al catálogo</a>
    </asp:Panel>

    <asp:Panel ID="pnlEditandoProducto" runat="server">

        <div class="edit-user-cont">

            <h2>Editar Producto</h2>

            <div class="form-group">
                <asp:Label Text="Nombre del producto" runat="server" CssClass="lblform" />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="txtform" />
            </div>

            <div class="form-group">
                <asp:Label Text="Url de la imagen" runat="server" CssClass="lblform" />
                <asp:TextBox ID="txtUrlDeLaImagen" runat="server" CssClass="txtform" />
            </div>

            <div class="form-group">
                <asp:Label Text="Precio" runat="server" CssClass="lblform" />
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="txtform" />
            </div>

            <div class="form-group">
                <asp:Label Text="Porcentaje de descuento" runat="server" CssClass="lblform" />
                <asp:TextBox ID="txtPorcentajeDeDescuento" runat="server" CssClass="txtform" />
            </div>

            <div class="form-group">
                <asp:Label Text="Stock disponible" runat="server" CssClass="lblform" />
                <asp:TextBox ID="txtStockDisponible" runat="server" CssClass="txtform" />
            </div>

            <div class="form-group">
                <asp:Label Text="Fecha de lanzamiento" runat="server" CssClass="lblform" />
                <asp:TextBox ID="txtFechaDeLanzamiento" runat="server" CssClass="txtform" TextMode="Date" />
            </div>

            <div class="form-group">
                <asp:Label Text="EsDigital" runat="server" CssClass="lblform" />
                <asp:CheckBox ID="chkEsDigital" runat="server" />
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <asp:Label Text="Categorías" runat="server" CssClass="lblform" />

                <asp:CheckBoxList ID="cblCategorias" runat="server" RepeatColumns="3" CssClass="txtform" />
            </div>

            <asp:Label ID="lblMsjError" runat="server" CssClass="mensaje" Visible="false"></asp:Label>

            <!-- Botones -->
            <div class="botones">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios" CssClass="btn-guardar" OnClick="btnGuardar_Click" />

                <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btnVolver" OnClick="btnVolver_Click" />
            </div>

        </div>
    </asp:Panel>
    <%-- Aqui termina el panel editar producto --%>
</asp:Content>
