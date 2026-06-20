<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="MiCuenta.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.MiCuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Resources/StyleTPC/MiCuentaStyle.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div class="cuenta-container">

    <div class="cuenta-card">

        <%--info--%>
        <div id="Info" runat="server">

            <h3 class="cuenta-titulo">Información de la Cuenta</h3>

            <p class="info-item">
                <strong>Nombre:</strong>
                <asp:Label ID="lblNombre" runat="server" />
            </p>

            <p class="info-item">
                <strong>Apellido:</strong>
                <asp:Label ID="lblApellido" runat="server" />
            </p>

            <p class="info-item">
                <strong>Email:</strong>
                <asp:Label ID="lblEmail" runat="server" />
            </p>

            <p class="info-item">
                <strong>Teléfono:</strong>
                <asp:Label ID="lblTelefono" runat="server" />
            </p>

            <asp:Button ID="btnModificar" runat="server" Text="Editar Perfil" CssClass="btnPrincipal" OnClick="btnModificar_Click" />

        </div>

        <%--modificar prfil--%>
        <div id="Editar" runat="server">

            <h3 class="cuenta-titulo">Modificar Perfil</h3>

            <p class="info-item">
            <strong>Nombre:</strong>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="txtCuenta" />
            </p>

            <p class="info-item">
            <strong>Apellido:</strong>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="txtCuenta" />
            </p>

            <p class="info-item">
            <strong>Email:</strong>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtCuenta" />
            </p>

            <p class="info-item">
            <strong>Teléfono:</strong>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="txtCuenta" />
            </p>

            <asp:Label ID="lblMsjError" runat="server" CssClass="error" Visible="false">
            </asp:Label>

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btnPrincipal" OnClick="btnGuardar_Click" />

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btnSecundario" OnClick="btnCancelar_Click" />

        </div>

        <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btnCerrar" OnClick="btnCerrarSesion_Click" />

    </div>

</div>

</asp:Content>
