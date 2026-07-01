<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="ResetearPass.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.ResetearPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/LoginStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center login-container">
        <div class="col-md-5">

            <div class="login-card">

                <div class="text-center mb-4">
                    <h2 class="login-titulo">Restablecer contraseña</h2>
                    <p class="login-subtitulo">
                        Ingresá el código de 6 dígitos que te enviamos y tu nueva contraseña.
                    </p>
                </div>

                <div class="mb-3">
                    <label class="form-label login-label">Código de verificación</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="txtUser" MaxLength="6" placeholder="123456"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label login-label">Nueva contraseña</label>
                    <asp:TextBox ID="txtNuevaPass" runat="server" TextMode="Password" placeholder="********" CssClass="txtPass"></asp:TextBox>
                </div>

                <div class="mb-4">
                    <label class="form-label login-label">Confirmar contraseña</label>
                    <asp:TextBox ID="txtConfirmarPass" runat="server" TextMode="Password" placeholder="********" CssClass="txtPass"></asp:TextBox>
                </div>

                <div class="d-grid gap-3">

                    <asp:Button Text="Restablecer contraseña" CssClass="btnIngresar" ID="btnRestablecer" OnClick="btnRestablecer_Click" runat="server" />
                    <asp:LinkButton ID="lnkReenviar" runat="server" Text="Reenviar código" OnClick="lnkReenviar_Click" CssClass="Registrarse-link" />

                    <asp:Label runat="server" ID="lblMsj" Visible="false" CssClass="d-block" />

                    <a href="Login.aspx" class="cancelar-link">Volver a iniciar sesión</a>

                </div>

            </div>

        </div>
    </div>

</asp:Content>
