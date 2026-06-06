<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.Home" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/Home.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Carrousel wiiii --%>
    <div class="home-content-wrapper">
        <div class="container mt-4">

            <div class="home-carousel-wrapper">
                <div id="carouselJuegos"
                    class="carousel slide"
                    data-bs-ride="carousel">
                    <div class="carousel-inner">
                        <asp:Repeater ID="rptCarousel"
                            runat="server"
                            OnItemDataBound="rptCarousel_ItemDataBound">
                            <ItemTemplate>
                                <div id="slideContainer" runat="server">
                                    <img src='<%# Eval("ImagenUrl") %>'
                                        class="d-block w-100"
                                        style="height: 500px; object-fit: cover;"
                                        alt='<%# Eval("Nombre") %>' />
                                    <div class="carousel-caption d-none d-md-block">
                                        <h3><%# Eval("Nombre") %></h3>
                                        <p><%# Eval("Descripcion") %></p>
                                        <h5>$ <%# Eval("Precio", "{0:N2}") %>
                                        </h5>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <button class="carousel-control-prev"
                        type="button"
                        data-bs-target="#carouselJuegos"
                        data-bs-slide="prev">
                        <span class="carousel-control-prev-icon"></span>
                    </button>
                    <button class="carousel-control-next"
                        type="button"
                        data-bs-target="#carouselJuegos"
                        data-bs-slide="next">
                        <span class="carousel-control-next-icon"></span>
                    </button>
                </div>
            </div>

        </div>
        <%-- Termina Carrousel no wiiii --%>
        <%-- AQUI REPEATERS CARDS DE JUEGOS 
        RPG--%>

        <h2 class="home-section-title">RPG</h2>

        <asp:Repeater ID="rptRpg" runat="server">
            <ItemTemplate>
                <div>
                    <div>
                        <img src='<%# Eval("ImagenUrl") %>'/>
                        <div>
                            <h5>
                                <%# Eval("Nombre") %>
                            </h5>
                            <p>$ <%# Eval("Precio") %></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <%-- ACCION --%>
        <h2 class="home-section-title">Acción</h2>

        <div class="row">
            <asp:Repeater ID="rptAccion" runat="server">
                <ItemTemplate>
                    <div>
                        <%# Eval("Nombre") %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <%-- SHOOTER --%>
        <h2 class="home-section-title">Shooter</h2>

        <div class="row">
            <asp:Repeater ID="rptShooter" runat="server">
                <ItemTemplate>
                    <div>
                        <%# Eval("Nombre") %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <%-- TERMINAN LAS CARDS --%>

    <%-- TERMINAN LAS CARDS --%>

    <hr />

    <h3>Prueba de Base de Datos</h3>

    <asp:Button ID="btnProbar" runat="server"
        Text="Obtener Juegos"
        OnClick="btnProbar_Click" />

    <br /><br />

    <asp:TextBox ID="txtResultado"
        runat="server"
        TextMode="MultiLine"
        Rows="5"
        Width="800px">
    </asp:TextBox>
</asp:Content>
