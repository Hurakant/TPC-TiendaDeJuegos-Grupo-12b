<%@ Page Title="Pedidos Vendedor" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/VendedorPedidosStyle.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container mt-4">

    
    <div class="card shadow-sm p-3 mb-3">

        <div class="d-flex justify-content-between align-items-center">

            <div>
                <h3 class="mb-0">
                    <asp:Label ID="lblTitulo" runat="server" />
                </h3>
                <small class="text-muted">Gestión de pedidos del sistema</small>
            </div>

            <asp:Button Text="Volver"
                ID="btnVolver"
                CssClass="btn btn-outline-secondary"
                OnClick="btnVolver_Click"
                runat="server" />

        </div>

    </div>

  
    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger fw-bold" />

    <div class="card shadow-sm p-3">

       <asp:GridView ID="gvPedidos"
    runat="server"
    CssClass="table table-hover table-striped align-middle shadow-sm"
    AutoGenerateColumns="false"
    GridLines="None"
    HeaderStyle-CssClass="table-dark"
    OnRowDataBound="gvPedidos_RowDataBound">

    <Columns>

        <asp:BoundField DataField="IdPedido" HeaderText="#ID" />
        <asp:BoundField DataField="Cliente.Nombre" HeaderText="Cliente" />
        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="Estado" HeaderText="Estado" />
        <asp:BoundField DataField="MontoTotal" HeaderText="Total" DataFormatString="{0:C}" />

        <asp:TemplateField HeaderText="Acciones">

            <ItemTemplate>

                <div class="d-flex gap-2">

                   
                    <asp:DropDownList ID="ddlEstado"
                        runat="server"
                        CssClass="form-select form-select-sm"
                        Width="140px">

                        <asp:ListItem Text="Pendiente" />
                        <asp:ListItem Text="En preparación" />
                        <asp:ListItem Text="Enviado" />

                    </asp:DropDownList>

                  
                    <asp:Button ID="btnActualizar"
                        runat="server"
                        Text="Actualizar"
                        CommandArgument='<%# Eval("IdPedido") %>'
                        CssClass="btn btn-sm btn-primary"
                        OnClick="btnActualizar_Click" />

                    
                    <asp:Button ID="btnDetalle"
                        runat="server"
                        Text="Ver"
                        CommandArgument='<%# Eval("IdPedido") %>'
                        CssClass="btn btn-sm btn-outline-dark"
                        OnClick="btnDetalle_Click" />

                </div>

            </ItemTemplate>

        </asp:TemplateField>

    </Columns>

</asp:GridView>

    </div>

</div>

</asp:Content>