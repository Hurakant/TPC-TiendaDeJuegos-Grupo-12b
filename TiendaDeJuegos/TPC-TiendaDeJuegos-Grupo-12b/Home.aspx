<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.Home" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/Home.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- lbl de bienvenida de user logueado --%>
    <asp:Label Text="nombre" runat="server" ID="lblBienvenida" Visible="false" CssClass="home-bienvenida" />


    <%--admin--%>

    <div id="divAdmin" runat="server" visible="false">

        <div class="container mt-4">

            <div class="home-panel">

                <h4 class="panel-title">Gestión Admin</h4>

                <div class="panel-buttons">
                    <asp:Button ID="btnCategoria" runat="server" Text="Administrar Categorias" OnClick="btnCategoria_Click" CssClass="btn-panel" />

                    <asp:Button ID="btnAccesibilidad" runat="server" Text="Administrar Accesibilidad" OnClick="btnAccesibilidad_Click" CssClass="btn-panel" />

                    <asp:Button ID="btnProductos" runat="server" Text="Administrar Productos" OnClick="btnProductos_Click" CssClass="btn-panel" />

                    <asp:Button ID="btnUsuarios" runat="server" Text="Administrar Usuarios" OnClick="btnUsuarios_Click" CssClass="btn-panel" />

                    <asp:Button ID="btnPedidos" runat="server" Text="Administrar Pedidos" OnClick="btnPedidos_Click" CssClass="btn-panel" />
                </div>
            </div>

        </div>

    </div>

    <%--vendedor--%>

    <div id="divVendedor" runat="server" visible="false">

        <div class="container mt-4">

            <div class="home-panel">

                <h4 class="panel-title">Gestión Vendedor</h4>

                <div class="panel-buttons">
                    <asp:Button ID="btnVendedorProductos" runat="server" Text="Administrar Productos" OnClick="btnVendedorProductos_Click" CssClass="btn-panel" />

                    <asp:Button ID="btnVendedorPedidos" runat="server" Text="Administrar Pedidos" OnClick="btnVendedorPedidos_Click" CssClass="btn-panel" />
                </div>
            </div>

        </div>

    </div>


    <div id="divCliente" runat="server" visible="false">
        <%-- Carrusel principal de juegos --%>
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
                                            alt='<%# Eval("Nombre") %>' />
                                        <div class="carousel-caption d-none d-md-block">
                                            <h3>
                                                <asp:HyperLink ID="lnkVerDetalleCarousel" runat="server"
                                                    CssClass="card-title-link"
                                                    NavigateUrl='<%# "VerProducto.aspx?id=" + Eval("IdProducto") %>'>
                                                    <%# Eval("Nombre") %>
                                                </asp:HyperLink>
                                            </h3>
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
            <%-- Termina carrusel --%>
            <%-- se ve largo pero no es mucho --%>
            <%-- RPG --%>
            <div class="container mt-4">

                <h2 class="home-section-title">RPG</h2>

                <div class="category-carousel-wrapper">

                    <div id="carouselRpg" class="carousel slide" data-bs-ride="false">

                        <div class="carousel-inner">

                            <asp:Repeater ID="rptRpg"
                                runat="server"
                                OnItemDataBound="rptCategoria_ItemDataBound">

                                <ItemTemplate>

                                    <div id="slide"
                                        runat="server"
                                        class="carousel-item">

                                        <div class="d-flex justify-content-center gap-3">

                                            <asp:Repeater
                                                ID="rptCards"
                                                runat="server">

                                                <ItemTemplate>

                                                    <div class="game-card">

                                                        <img class="card-img"
                                                            src="<%# Eval("ImagenUrl") %>" />

                                                        <div class="card-body">

                                                            <asp:HyperLink ID="lnkVerDetalleCarousel" runat="server"
                                                                CssClass="card-title-link"
                                                                NavigateUrl='<%# "VerProducto.aspx?id=" + Eval("IdProducto") %>'>
                                                                            <%# Eval("Nombre") %>
                                                            </asp:HyperLink>

                                                            <p class="card-price">
                                                                $ <%# Eval("Precio","{0:N0}") %>
                                                            </p>

                                                        </div>

                                                    </div>

                                                </ItemTemplate>

                                            </asp:Repeater>

                                        </div>

                                    </div>

                                </ItemTemplate>

                            </asp:Repeater>

                        </div>

                        <button class="carousel-control-prev"
                            type="button"
                            data-bs-target="#carouselRpg"
                            data-bs-slide="prev">

                            <span class="carousel-control-prev-icon"></span>

                        </button>

                        <button class="carousel-control-next"
                            type="button"
                            data-bs-target="#carouselRpg"
                            data-bs-slide="next">

                            <span class="carousel-control-next-icon"></span>

                        </button>

                    </div>

                </div>

                <%-- ACCION --%>
                <h2 class="home-section-title">Acción</h2>

                <div class="category-carousel-wrapper">

                    <div id="carouselAccion" class="carousel slide" data-bs-ride="false">

                        <div class="carousel-inner">

                            <asp:Repeater ID="rptAccion"
                                runat="server"
                                OnItemDataBound="rptCategoria_ItemDataBound">

                                <ItemTemplate>

                                    <div id="slide"
                                        runat="server"
                                        class="carousel-item">

                                        <div class="d-flex justify-content-center gap-3">

                                            <asp:Repeater
                                                ID="rptCards"
                                                runat="server">

                                                <ItemTemplate>

                                                    <div class="game-card">

                                                        <img class="card-img"
                                                            src="<%# Eval("ImagenUrl") %>" />

                                                        <div class="card-body">

                                                            <asp:HyperLink ID="lnkVerDetalleCarousel" runat="server"
                                                                CssClass="card-title-link"
                                                                NavigateUrl='<%# "VerProducto.aspx?id=" + Eval("IdProducto") %>'>
                                                                            <%# Eval("Nombre") %>
                                                            </asp:HyperLink>

                                                            <p class="card-price">
                                                                $ <%# Eval("Precio","{0:N0}") %>
                                                            </p>

                                                        </div>

                                                    </div>

                                                </ItemTemplate>

                                            </asp:Repeater>

                                        </div>

                                    </div>

                                </ItemTemplate>

                            </asp:Repeater>

                        </div>

                        <button class="carousel-control-prev"
                            type="button"
                            data-bs-target="#carouselAccion"
                            data-bs-slide="prev">

                            <span class="carousel-control-prev-icon"></span>

                        </button>

                        <button class="carousel-control-next"
                            type="button"
                            data-bs-target="#carouselAccion"
                            data-bs-slide="next">

                            <span class="carousel-control-next-icon"></span>

                        </button>

                    </div>

                </div>

                <%-- SHOOTER --%>
                <h2 class="home-section-title">Shooter</h2>

                <div class="category-carousel-wrapper">

                    <div id="carouselShooter" class="carousel slide" data-bs-ride="false">

                        <div class="carousel-inner">

                            <asp:Repeater ID="rptShooter"
                                runat="server"
                                OnItemDataBound="rptCategoria_ItemDataBound">

                                <ItemTemplate>

                                    <div id="slide"
                                        runat="server"
                                        class="carousel-item">

                                        <div class="d-flex justify-content-center gap-3">

                                            <asp:Repeater
                                                ID="rptCards"
                                                runat="server">

                                                <ItemTemplate>

                                                    <div class="game-card">

                                                        <img class="card-img"
                                                            src="<%# Eval("ImagenUrl") %>" />

                                                        <div class="card-body">

                                                            <asp:HyperLink ID="lnkVerDetalleCarousel" runat="server"
                                                                CssClass="card-title-link"
                                                                NavigateUrl='<%# "VerProducto.aspx?id=" + Eval("IdProducto") %>'>
                                                                            <%# Eval("Nombre") %>
                                                            </asp:HyperLink>

                                                            <p class="card-price">
                                                                $ <%# Eval("Precio","{0:N0}") %>
                                                            </p>

                                                        </div>

                                                    </div>

                                                </ItemTemplate>

                                            </asp:Repeater>

                                        </div>

                                    </div>

                                </ItemTemplate>

                            </asp:Repeater>

                        </div>

                        <button class="carousel-control-prev"
                            type="button"
                            data-bs-target="#carouselShooter"
                            data-bs-slide="prev">

                            <span class="carousel-control-prev-icon"></span>

                        </button>

                        <button class="carousel-control-next"
                            type="button"
                            data-bs-target="#carouselShooter"
                            data-bs-slide="next">

                            <span class="carousel-control-next-icon"></span>

                        </button>

                    </div>

                </div>

            </div>
        </div>
        <%-- Termina seccion de juegos --%> 
    </div>
</asp:Content>
