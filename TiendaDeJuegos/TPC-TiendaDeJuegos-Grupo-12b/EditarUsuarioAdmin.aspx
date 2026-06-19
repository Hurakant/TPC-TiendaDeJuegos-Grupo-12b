<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="EditarUsuarioAdmin.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.EditarUsuarioAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label Text="Nombre" runat="server" />
<asp:TextBox ID="txtNombre" runat="server" />

<asp:Label Text="Apellido" runat="server" />
<asp:TextBox ID="txtApellido" runat="server" />

<asp:Label Text="Email" runat="server" />
<asp:TextBox ID="txtEmail" runat="server" />

<asp:Label Text="Teléfono" runat="server" />
<asp:TextBox ID="txtTelefono" runat="server" />

<asp:Label Text="Rol" runat="server" />
<asp:DropDownList ID="ddlRol" runat="server" />

<asp:Button ID="btnGuardar" runat="server"
    Text="Guardar cambios" OnClick="btnGuardar_Click" />


</asp:Content>
