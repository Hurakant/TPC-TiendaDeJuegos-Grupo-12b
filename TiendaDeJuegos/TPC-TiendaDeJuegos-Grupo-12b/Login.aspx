<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="Resources/StyleTPC/LoginStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="row justify-content-center login-container">
        <div class="col-md-5">

            <div class="login-card">

                <div class="text-center mb-4" cssClass="contEncabezado">
                     <asp:Image ID="imgLogoLogin" runat="server" ImageUrl="Resources/Logo/logosfB.svg" CssClass="loginlogo" AlternateText="NovaHub" />

                    <h2 class="login-titulo">Iniciar sesión</h2>
                    <p class="login-subtitulo">
                        Accedé a tu cuenta de NovaHub
                    </p>
                </div>

                <div class="mb-3">

                    <%-- usuario --%>
                    <label class="form-label login-label">Email</label>

                    <asp:TextBox ID="TxtUser" runat="server" placeholder="TuEmail@gmail.com" CssClass="txtUser"> </asp:TextBox>

                </div>

                <div class="mb-4">
                    <%-- contraseña --%>
                    <label class="form-label login-label">Contraseña</label>

                    <asp:TextBox ID="TxtPass" runat="server" placeholder="Contraseña" TextMode="Password" CssClass="txtPass">
                    </asp:TextBox>

                </div>

                <div class="d-grid gap-3">

                    <asp:Button Text="Ingresar" CssClass="btnIngresar" ID="btnLogin" OnClick="btnLogin_Click" runat="server" />

                    <asp:Label Text="Error" runat="server" ID="lblError" Visible="false" />
                    <a href="Home.aspx" class="cancelar-link">Cancelar </a>
                    <a href="Registro.aspx" class="Registrarse-link">No tienes una cuenta? Registrate! </a>

                </div>

            </div>

        </div>
    </div>

</asp:Content>
