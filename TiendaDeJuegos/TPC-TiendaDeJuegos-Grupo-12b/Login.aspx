<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
   <div class="row justify-content-center login-container">
    <div class="col-md-5">

        <div class="login-card">

            <div class="text-center mb-4">
                <h2 class="login-titulo">Iniciar sesión</h2>
                <p class="login-subtitulo">
                    Accedé a tu cuenta de NovaHub
                </p>
            </div>

            <div class="mb-3">

                <%-- usuario --%>
                <label class="form-label login-label">Usuario</label>

                <asp:TextBox ID="TxtUser" runat="server" placeholder="Nombre de usuario" CssClass="txtUser">
                </asp:TextBox>

            </div>

            <div class="mb-4">
                <%-- contraseña --%>
                <label class="form-label login-label">Contraseña</label>

                <asp:TextBox ID="TxtPass" runat="server" placeholder="Contraseña" TextMode="Password" CssClass="txtPass">
                </asp:TextBox>

            </div>

            <div class="d-grid gap-3">

                <asp:Button Text="Ingresar" CssClass="btnIngresar" id="btnLogin" OnClick="btnLogin_Click" runat="server" />

                <a href="Home.aspx" class="cancelar-link"> Cancelar </a>

            </div>

        </div>

    </div>
</div>

</asp:Content>
