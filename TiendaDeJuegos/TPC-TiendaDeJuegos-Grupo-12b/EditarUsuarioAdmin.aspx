<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="EditarUsuarioAdmin.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.EditarUsuarioAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/EditarUserAdminStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="edit-user-cont">

        <h2>Editar Usuario</h2>

        <div class="form-group">
            <asp:Label Text="Nombre" runat="server" CssClass="lblform" />
            <asp:TextBox ID="txtNombre" runat="server" CssClass="txtform" />
        </div>

        <div class="form-group">
            <asp:Label Text="Apellido" runat="server" CssClass="lblform" />
            <asp:TextBox ID="txtApellido" runat="server" CssClass="txtform" />
        </div>

        <div class="form-group">
            <asp:Label Text="Email" runat="server" CssClass="lblform" />
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtform" />
        </div>

        <div class="form-group">
            <asp:Label Text="Teléfono" runat="server" CssClass="lblform" />
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="txtform" />
        </div>

        <div class="form-group">
            <asp:Label Text="Rol" runat="server" CssClass="lblform" />
            <asp:DropDownList ID="ddlRol" runat="server" CssClass="txtform" />
        </div>

        <asp:Label ID="lblMsjError" runat="server" CssClass="mensaje" Visible="false"></asp:Label>

        <div class="botones">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios" CssClass="btn-guardar" OnClick="btnGuardar_Click" />

            <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btnVolver" OnClick="btnVolver_Click" />
        </div>

    </div>


</asp:Content>
