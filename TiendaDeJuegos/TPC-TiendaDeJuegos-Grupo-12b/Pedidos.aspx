<%@ Page Title="Pedidos Vendedor" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> <link href="Resources/StyleTPC/VendedorPedidosStyle.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container mt-4">

```
<!-- CABECERA -->
<div class="card shadow-sm p-3 mb-3">

    <div class="d-flex justify-content-between align-items-center">

        <div>
            <h3 class="mb-0">
                <asp:Label ID="lblTitulo" runat="server" />
            </h3>

            <small class="text-muted">
                Gestión de pedidos del sistema
            </small>
        </div>

        <asp:Button ID="btnVolver"
            runat="server"
            Text="Volver"
            CssClass="btn btn-outline-secondary"
            OnClick="btnVolver_Click" />

    </div>

</div>

<!-- MENSAJES -->
<asp:Label ID="lblMensaje"
    runat="server"
    CssClass="text-danger fw-bold" />

<!-- TABLA -->
<div class="card shadow-sm p-3">

    <asp:GridView ID="gvPedidos"
        runat="server"
        CssClass="table table-hover table-striped align-middle shadow-sm"
        AutoGenerateColumns="False"
        GridLines="None"
        HeaderStyle-CssClass="table-dark"
        EmptyDataText="No hay pedidos registrados.">

        <Columns>

            <asp:BoundField DataField="IdPedido"
                HeaderText="#ID" />

            <asp:BoundField DataField="Cliente.Nombre"
                HeaderText="Cliente" />

            <asp:BoundField DataField="Fecha"
                HeaderText="Fecha"
                DataFormatString="{0:dd/MM/yyyy}" />

            <asp:BoundField DataField="Estado"
                HeaderText="Estado" />

            <asp:TemplateField HeaderText="Total">
                <ItemTemplate>
                    $ <%# Eval("MontoTotal", "{0:N2}") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Acciones">

                <ItemStyle HorizontalAlign="Center" />

                <ItemTemplate>

                    <asp:Button ID="btnDetalle"
                        runat="server"
                        Text="Ver detalle"
                        CommandArgument='<%# Eval("IdPedido") %>'
                        CssClass="btn btn-sm btn-primary"
                        OnClick="btnDetalle_Click" />

                </ItemTemplate>

            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</div>
```

</div>

</asp:Content>
