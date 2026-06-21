<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/ErrorStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


  <div class="error-container">

        <div class="error-card">

            <h1 class="error-title">Acceso denegado</h1>

            <p class="error-text">
                Tu perfil no tiene permisos suficientes para acceder a esta página!
            </p>

            <asp:Button ID="btnVolver" runat="server" Text="Volver al inicio" CssClass="btn-volver-error" OnClick="btnVolver_Click"/>

        </div>

    </div>


</asp:Content>
