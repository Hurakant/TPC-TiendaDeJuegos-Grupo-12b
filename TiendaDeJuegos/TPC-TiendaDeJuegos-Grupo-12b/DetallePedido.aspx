<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master"
    AutoEventWireup="true" CodeBehind="DetallePedido.aspx.cs"
    Inherits="TPC_TiendaDeJuegos_Grupo_12b.DetallePedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/DetallePedidoStyle.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="detalle-wrapper">

    <div class="container detalle-card">

        <div class="detalle-header">
            <h3>
                Pedido #<asp:Label ID="lblIdPedido" runat="server" />
            </h3>

            <span class="badge-estado">
                <asp:Label ID="lblEstado" runat="server" />
            </span>
        </div>

        <div class="info-grid">

            <div class="info-box">
                <strong>Fecha</strong>
                <asp:Label ID="lblFecha" runat="server" />
            </div>

            <div class="info-box">
                <strong>Entrega</strong>
                <asp:Label ID="lblEntrega" runat="server" />
            </div>

            <div class="info-box">
                <strong>Pago</strong>
                <asp:Label ID="lblPago" runat="server" />
            </div>

        </div>

        <asp:GridView ID="gvDetalle" runat="server"
            AutoGenerateColumns="False"
            CssClass="detalle-table">

            <Columns>
                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" />
            </Columns>

        </asp:GridView>

        <div class="total-box">
            Total: $<asp:Label ID="lblTotal" runat="server" />
        </div>

        <a href="MisPedidos.aspx" class="volver-link">
            ← Volver
        </a>

    </div>

</div>

</asp:Content>