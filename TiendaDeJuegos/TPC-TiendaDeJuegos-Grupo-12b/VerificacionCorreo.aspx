<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="VerificacionCorreo.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.VerificacionCorreo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/LoginStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center login-container">
        <div class="col-md-5">

            <div class="login-card">

                <div class="text-center mb-4">
                    <h2 class="login-titulo">Verificá tu correo</h2>
                    <p class="login-subtitulo">
                        Te enviamos un código de 6 dígitos a tu email. Ingresalo a continuación.
                    </p>
                </div>

                <div class="mb-4">
                    <label class="form-label login-label">Código de verificación</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="txtUser" MaxLength="6" placeholder="123456"></asp:TextBox>
                </div>

                <div class="d-grid gap-3">

                    <asp:Button Text="Verificar" CssClass="btnIngresar" ID="btnVerificar" OnClick="btnVerificar_Click" runat="server" />
                    <asp:LinkButton ID="lnkReenviar" runat="server" Text="Reenviar código" OnClick="lnkReenviar_Click" CssClass="Registrarse-link" />

                    <asp:Label runat="server" ID="lblMsj" Visible="false" CssClass="d-block" />

                </div>

            </div>

        </div>
    </div>

</asp:Content>
