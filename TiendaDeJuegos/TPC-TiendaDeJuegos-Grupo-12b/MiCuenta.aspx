<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="MiCuenta.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.MiCuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div ID="Info" runat="server">

    <h3>Información de la Cuenta</h3>

    <p><strong>Nombre: </strong> 
        <asp:Label ID="lblNombre" runat="server" />
    </p>

    <p><strong>Apellido: </strong> 
        <asp:Label ID="lblApellido" runat="server" />
    </p>

    <p><strong>Email: </strong> 
        <asp:Label ID="lblEmail" runat="server" />

    </p>
    <p><strong>Teléfono: </strong> 
        <asp:Label ID="lblTelefono" runat="server" />
    </p>

    <asp:Button ID="btnModificar" runat="server" Text="Editar Perfil" OnClick="btnModificar_Click" />

</div>

<div ID="Editar" runat="server">

    <h3>Modificar Perfil</h3>

    <asp:TextBox ID="txtNombre" runat="server" />

    <asp:TextBox ID="txtApellido" runat="server" />

    <asp:TextBox ID="txtEmail" runat="server" />

    <asp:TextBox ID="txtTelefono" runat="server" />

    <asp:Label ID="lblMsjError" runat="server" CssClass="error" Visible="false"></asp:Label>


    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" OnClick="btnGuardar_Click" />

    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />

</div>

    <asp:Button Text="Cerrar Sesión" runat="server" ID="btnCerrarSesion" OnClick="btnCerrarSesion_Click" />

</asp:Content>
