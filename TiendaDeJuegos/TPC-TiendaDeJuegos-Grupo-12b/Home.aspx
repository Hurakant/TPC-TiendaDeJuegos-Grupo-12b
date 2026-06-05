<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/MasterStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Carrousel wiiii --%>
    <div class="container mt-4">

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
                            <div class="carousel-caption d-none d-md-block bg-dark bg-opacity-75 rounded p-3">
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
    <%-- Termina Carrousel no wiiii --%>
    <%-- AQUI REPEATERS CARDS DE JUEGOS 
      ♫ And when I felt like I was an old cardigan
        Under someone's bed
        You put me on and said I was your favorite ♫
        RPG--%>

    <h2 class="text-white mt-5 mb-3">RPG</h2>

    <div class="row">
        <asp:Repeater ID="rptRpg" runat="server">
            <ItemTemplate>
                <div class="col-md-3 mb-4">
                    <div class="card h-100">
                        <img src='<%# Eval("ImagenUrl") %>'
                            class="card-img-top" />
                        <div class="card-body">
                            <h5 class="card-title">
                                <%# Eval("Nombre") %>
                            </h5>
                            <p>$ <%# Eval("Precio") %></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <%-- ACCION --%>
    <h2 class="text-white mt-5 mb-3">Acción</h2>

    <div class="row">
        <asp:Repeater ID="rptAccion" runat="server">
            <ItemTemplate>
                <div class="col-md-3 mb-4">
                    <div class="card h-100">
                        <img src='<%# Eval("ImagenUrl") %>'
                            class="card-img-top" />
                        <div class="card-body">
                            <h5 class="card-title">
                                <%# Eval("Nombre") %>
                            </h5>
                            <p>$ <%# Eval("Precio") %></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <%-- SHOOTER --%>
    <h2 class="text-white mt-5 mb-3">Shooter</h2>

    <div class="row">
        <asp:Repeater ID="rptShooter" runat="server">
            <ItemTemplate>
                <div class="col-md-3 mb-4">
                    <div class="card h-100">
                        <img src='<%# Eval("ImagenUrl") %>'
                            class="card-img-top" />
                        <div class="card-body">
                            <h5 class="card-title">
                                <%# Eval("Nombre") %>
                            </h5>
                            <p>$ <%# Eval("Precio") %></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <%-- TERMINAN LAS CARDS --%>
</asp:Content>
