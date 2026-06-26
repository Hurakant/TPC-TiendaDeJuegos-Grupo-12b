<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="AdminUsuarios.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.AdminUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Resources/StyleTPC/AdminUsuariosStyles.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="adminUsuarios-container">

        <div class="adminUsuarios-card">

            <div class="adminUsuariosHeader">

                <div class="tituloConBoton">
                    <h3 class="adminUsuariosTitulo">Gestión de Usuarios</h3>

                    <asp:Button Text="Volver" ID="btnVolver" CssClass="btnVolver" OnClick="btnVolver_Click" runat="server" />
                </div>

            </div>

            <div class="filtrosUsuarios">

                <asp:TextBox ID="txtFiltro" runat="server" CssClass="txtFiltro" placeholder="Buscar por nombre, apellido o email..." />
                <asp:DropDownList ID="ddlRol" runat="server" CssClass="ddlFiltro">

                    <asp:ListItem Text="Todos los roles" Value="" />
                    <asp:ListItem Text="Admin" Value="3" />
                    <asp:ListItem Text="Cliente" Value="1" />
                    <asp:ListItem Text="Vendedor" Value="2" />

                </asp:DropDownList>

                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ddlFiltro">

                    <asp:ListItem Text="Todos los estados" Value="" />
                    <asp:ListItem Text="Activos" Value="1" />
                    <asp:ListItem Text="Inactivos" Value="0" />

                </asp:DropDownList>

                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btnBuscar" OnClick="btnBuscar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Filtros" CssClass="btnLimpiar" OnClick="btnLimpiar_Click" />
            
            </div>
                            <asp:Label ID="lblMensaje" runat="server" CssClass="lblMensaje" />
            <%--grid usuarios--%>

            <asp:GridView ID="gvUsuarios" runat="server" DataKeyNames="IdUsuario" OnRowCommand="gvUsuarios_RowCommand" CssClass="tablaUsuarios" AutoGenerateColumns="false">
                <Columns>

                    <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
                    <asp:BoundField DataField="FechaAlta" HeaderText="Fecha Alta" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />


                    <asp:TemplateField HeaderText="Rol">
                        <ItemTemplate>
                            <%# Eval("Rol") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Activo">
                        <ItemTemplate>
                            <%# (Convert.ToBoolean(Eval("Activo")) ? "Sí" : "No") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">

                        <ItemTemplate>

                            <asp:Button ID="btnEditar" CssClass="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("IdUsuario") %>' Enabled='<%# Convert.ToInt32(Eval("IdUsuario")) != ((dominio.Usuario)Session["usuarioLogueado"]).IdUsuario %>' />
                            <asp:Button ID="btnEstado" CssClass="btnEstado" runat="server" Text='<%# Convert.ToBoolean(Eval("Activo")) ? "Desactivar" : "Activar" %>' CommandName="CambiarEstado" CommandArgument='<%# Eval("IdUsuario") %>'
                                Enabled='<%# Convert.ToInt32(Eval("IdUsuario")) != ((dominio.Usuario)Session["usuarioLogueado"]).IdUsuario %>' />

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>

    </div>

</asp:Content>
