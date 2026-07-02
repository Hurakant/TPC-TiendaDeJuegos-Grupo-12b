<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master"
    AutoEventWireup="true" CodeBehind="DetalleMiPedido.aspx.cs"
    Inherits="TPC_TiendaDeJuegos_Grupo_12b.DetalleMiPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/DetallePedidoStyle.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="detalle-wrapper">

    <div class="detalle-card" style="background: rgb(255 255 255 / 0%) !important;">

        <div class="detalle-header d-flex justify-content-between align-items-center">

            <h3>
                Pedido #<asp:Label ID="lblIdPedido" runat="server" />
            </h3>

        </div>

        <div class="info-grid mt-2" style="background: rgb(255 255 255 / 0%) !important;">

            <div class="info-box">
                <strong>Fecha</strong>
                <asp:Label ID="lblFecha" runat="server" />
            </div>

            <div class="info-box">
                <strong>Entrega</strong>
                <asp:Label ID="lblEntrega" runat="server" />
            </div>

        </div>

        <div class="info-grid mt-2" style="background: rgb(255 255 255 / 0%) !important;">

            <div class="info-box">
                <strong>Forma de entrega</strong>
                <asp:Label ID="lblFormaDeEntrega" runat="server" CssClass="form-control" />
            </div>

            <div class="info-box">
                <strong>Forma de pago</strong>
                <asp:Label ID="lblFormaDePago" runat="server" CssClass="form-control" />
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
