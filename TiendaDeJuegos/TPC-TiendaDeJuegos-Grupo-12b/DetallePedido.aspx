<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master"
    AutoEventWireup="true" CodeBehind="DetallePedido.aspx.cs"
    Inherits="TPC_TiendaDeJuegos_Grupo_12b.DetallePedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/DetallePedidoStyle.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="detalle-wrapper">

    <div class="container detalle-card">

        <div class="detalle-header d-flex justify-content-between align-items-center">

    <h3>
        Pedido #<asp:Label ID="lblIdPedido" runat="server" />
    </h3>

    <div class="d-flex align-items-center gap-2">

        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select w-50">

    <asp:ListItem Text="Pendiente" Value="1" />
    <asp:ListItem Text="Pagado" Value="2" />
    <asp:ListItem Text="En preparación" Value="3" />
    <asp:ListItem Text="Enviado" Value="4" />
    <asp:ListItem Text="Entregado" Value="5" />
    <asp:ListItem Text="Cancelado" Value="6" />

</asp:DropDownList>

        <asp:Button ID="btnGuardar" runat="server"
            Text="Actualizar"
            CssClass="btn btn-success"
            OnClick="btnGuardar_Click" />

    </div>

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