<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/RegistroStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="registrar-container">

        <div class="row justify-content-center w-100 m-0">

            <div class="col-md-8">

                <div class="registro-card">

                    <div class="row m-0">

                        <%-- texto de la izq --%>
                        <div class="col-md-4 registro-info">

                            <asp:Image ID="imgLogo" CssClass="imgLogo" runat="server" ImageUrl="Resources/Logo/logosfB.svg" Width="150px" />

                            <h2>Unite a NovaHub</h2>
                            <p>
                                ¡Creá tu cuenta para enterarte 
                         de las novedades!
                            </p>

                        </div>

                        <%-- form de registro --%>

                        <div class="col-md-8 registro-form">

                            <h3 class="TituloRegistro">Crear Cuenta</h3>

                            <div class="mb-3">

                                <label class="LblEmail">Email</label>
                                <asp:TextBox ID="TxtEmail" runat="server" placeholder="TuEmail@gmail.com" CssClass="TxtEmail"></asp:TextBox>

                            </div>

                            <div class="mb-3">
                                <label class="lblNombre">Nombre</label>
                                <asp:TextBox ID="TxtNombre" runat="server" placeholder="Nombre" CssClass="TxtNombre"></asp:TextBox>
                                <label class="lblNombre">Apellido</label>
                                <asp:TextBox ID="txtApellido" runat="server" placeholder="Apellido" CssClass="txtApellido"></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <label class="lblTelefono">Teléfono</label>
                                <asp:TextBox ID="TxtTelefono" runat="server" placeholder="11 2345-6789" CssClass="TxtTelefono" TextMode="Phone"></asp:TextBox>
                            </div>

                            <div class="mb-4">

                                <label class="lblPass">Contraseña</label>
                                <asp:TextBox runat="server" CssClass="txtPass" ID="txtPass" TextMode="Password" placeholder="********" />
                            </div>

                            <div class="d-grid gap-3">

                                <asp:Button Text="Registrarse" runat="server" CssClass="btnRegistrarse" ID="btnRegistrarse" OnClick="btnRegistrarse_Click" />

                                <asp:Label ID="lblMsjError" runat="server" CssClass="error" Visible="false"></asp:Label>
                                <a href="Home.aspx" class="cancelar-link">Cancelar </a>
                                <a href="Login.aspx" class="InSec-link">Tienes una cuenta? Inicia Sesión! </a>

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    </div>


</asp:Content>


