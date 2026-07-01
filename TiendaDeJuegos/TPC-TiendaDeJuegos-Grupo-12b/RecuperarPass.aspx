<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="RecuperarPass.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.RecuperarPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/LoginStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center login-container">
        <div class="col-md-5">

            <div class="login-card">

                <div class="text-center mb-4">
                    <h2 class="login-titulo">Recuperar contraseña</h2>
                    <p class="login-subtitulo">
                        Ingresá tu email y te enviamos un código para restablecerla.
                    </p>
                </div>

                <div class="mb-4">
                    <label class="form-label login-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="TuEmail@gmail.com" CssClass="txtUser"></asp:TextBox>
                </div>

                <div class="d-grid gap-3">

                    <asp:Button Text="Enviar código" CssClass="btnIngresar" ID="btnEnviar" OnClick="btnEnviar_Click" runat="server" />

                    <asp:Label runat="server" ID="lblMsj" Visible="false" CssClass="d-block" />

                    <a href="Login.aspx" class="cancelar-link">Volver a iniciar sesión</a>

                </div>

            </div>

        </div>
    </div>

</asp:Content>