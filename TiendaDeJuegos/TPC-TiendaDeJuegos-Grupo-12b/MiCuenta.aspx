<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="MiCuenta.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.MiCuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="lblNombre" runat="server" />
<br />

<asp:Label ID="lblEmail" runat="server" />
<br />

<asp:Label ID="lblRol" runat="server" />
<br />
    <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" OnClick="btnCerrarSesion_Click" />

</asp:Content>
