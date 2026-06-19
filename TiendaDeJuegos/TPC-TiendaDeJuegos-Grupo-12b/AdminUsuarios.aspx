<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="AdminUsuarios.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.AdminUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container mt-4">

        <div class="card p-3">

            <h3>Gestión de Usuarios</h3>

            <asp:Label ID="lblMensaje" runat="server" />

            <asp:GridView ID="gvUsuarios" runat="server" DataKeyNames="IdUsuario" OnRowCommand="gvUsuarios_RowCommand" CssClass="table table-striped" AutoGenerateColumns="false">
                <Columns>

                    <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
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

                            <asp:Button ID="btnEditar" visible="true" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("IdUsuario") %>' Enabled='<%# Convert.ToInt32(Eval("IdUsuario")) != ((dominio.Usuario)Session["usuarioLogueado"]).IdUsuario %>'/>
                            <asp:Button ID="btnEstado" visible="true" runat="server" Text='<%# Convert.ToBoolean(Eval("Activo")) ? "Desactivar" : "Activar" %>' CommandName="CambiarEstado" CommandArgument='<%# Eval("IdUsuario") %>'
                                   Enabled='<%# Convert.ToInt32(Eval("IdUsuario")) != ((dominio.Usuario)Session["usuarioLogueado"]).IdUsuario %>'/>

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>

    </div>

</asp:Content>
