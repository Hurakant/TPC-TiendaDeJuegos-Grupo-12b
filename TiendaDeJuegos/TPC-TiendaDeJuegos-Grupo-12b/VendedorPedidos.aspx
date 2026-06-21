<%@ Page Title="Pedidos Vendedor" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="VendedorPedidos.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.VendedorPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/VendedorPedidosStyle.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">

        <div class="card p-3">

            <div class="adminUsuariosHeader">

                <div class="tituloConBoton">
                    <asp:Label ID="lblTitulo" runat="server" />

                    <asp:Button Text="Volver" ID="btnVolver" CssClass="btnVolver" OnClick="btnVolver_Click" runat="server" />
                </div>

            </div>

            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />

            <br />

            <asp:GridView ID="gvPedidos" runat="server" CssClass="table table-striped"
                AutoGenerateColumns="false">

                <Columns>

                    <asp:BoundField DataField="Id" HeaderText="ID Pedido" />
                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />

                    <asp:TemplateField HeaderText="Acciones">

                        <ItemTemplate>

                            <asp:DropDownList ID="ddlEstado" runat="server">
                                <asp:ListItem>Pendiente</asp:ListItem>
                                <asp:ListItem>En preparación</asp:ListItem>
                                <asp:ListItem>Enviado</asp:ListItem>
                            </asp:DropDownList>

                            <asp:Button ID="btnActualizar" runat="server"
                                Text="Actualizar"
                                CommandArgument='<%# Eval("Id") %>'
                                OnClick="btnActualizar_Click" />

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>

    </div>

</asp:Content>
