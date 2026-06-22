<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="MiCuenta.aspx.cs" Inherits="TPC_TiendaDeJuegos_Grupo_12b.MiCuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Resources/StyleTPC/MiCuentaStyle.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div class="cuenta-container">

    <div class="cuenta-card">

        <%--info--%>
        <div id="Info" runat="server">

            <h3 class="cuenta-titulo">Información de la Cuenta</h3>

            <p class="info-item">
                <strong>Nombre:</strong>
                <asp:Label ID="lblNombre" runat="server" />
            </p>

            <p class="info-item">
                <strong>Apellido:</strong>
                <asp:Label ID="lblApellido" runat="server" />
            </p>

            <p class="info-item">
                <strong>Email:</strong>
                <asp:Label ID="lblEmail" runat="server" />
            </p>

            <p class="info-item">
                <strong>Teléfono:</strong>
                <asp:Label ID="lblTelefono" runat="server" />
            </p>

            <asp:Button ID="btnModificar" runat="server" Text="Editar Perfil" CssClass="btnPrincipal" OnClick="btnModificar_Click" />

        </div>

        <%--modificar prfil--%>
        <div id="Editar" runat="server">

            <h3 class="cuenta-titulo">Modificar Perfil</h3>

            <p class="info-item">
            <strong>Nombre:</strong>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="txtCuenta" />
            </p>

            <p class="info-item">
            <strong>Apellido:</strong>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="txtCuenta" />
            </p>

            <p class="info-item">
            <strong>Email:</strong>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtCuenta" />
            </p>

            <p class="info-item">
            <strong>Teléfono:</strong>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="txtCuenta" />
            </p>

            <asp:Label ID="lblMsjError" runat="server" CssClass="error" Visible="false">
            </asp:Label>

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btnPrincipal" OnClick="btnGuardar_Click" />

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btnSecundario" OnClick="btnCancelar_Click" />

        </div>

        <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btnCerrar" OnClick="btnCerrarSesion_Click" />

    </div>

     <%--direccion--%>
 <div class="DireccionCard" id="DireccionCard" runat="server">
     <h3 class="DireTitulo">Mis Direcciones</h3>
     <p class="DireTexto">Podés registrar un máximo de 3 direcciones disponibles.</p>

     <asp:GridView ID="dgvDirecciones" runat="server" AutoGenerateColumns="false" CssClass="tablaDire" GridLines="None" DataKeyNames="IDDireccion" OnRowCommand="dgvDirecciones_RowCommand" EmptyDataText="No hay direcciones registradas.">
         <Columns>
             <asp:BoundField DataField="Calle" HeaderText="Calle" />
             <asp:BoundField DataField="Numero" HeaderText="Número" />
             <asp:BoundField DataField="Piso" HeaderText="Piso" NullDisplayText="-" />
             <asp:BoundField DataField="Depto" HeaderText="Depto" NullDisplayText="-" />
             <asp:BoundField DataField="Localidad" HeaderText="Localidad" />
             <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
             <asp:BoundField DataField="CodigoPostal" HeaderText="Cód. Postal" />

             <asp:TemplateField HeaderText="Acción">
                 <ItemTemplate>
                     <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="EliminarDir" CommandArgument='<%# Eval("IDDireccion") %>' CssClass="btnCerrar" Style="padding: 5px 10px; font-size: 0.85em; margin: 0;" OnClientClick="return confirm('¿Estas seguro de eliminar la direccion?');" />
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
     </asp:GridView>

     <%-- btn agregar --%>
     <asp:Button ID="btnAgregarDireccion" runat="server" Text="+ Agregar" CssClass="btnPrincipal" OnClick="btnAgregarDireccion_Click"/>
     <asp:Label ID="lblMsjDireccion" runat="server" CssClass="error" Visible="false"></asp:Label>
 </div>

 <%-- agregar dirección --%>
 <div id="AgregarDire" runat="server">

     <h3 class="cuentaTitulo">Nueva Dirección</h3>

     <p class="info-item">
         <strong>Calle:</strong>
         <asp:TextBox ID="txtCalle" runat="server" CssClass="txtCuenta" />
     </p>

     <p class="info-item">
         <strong>Número:</strong>
         <asp:TextBox ID="txtNumero" runat="server" CssClass="txtCuenta" />
     </p>

     <p class="info-item">
         <strong>Piso:</strong>
         <asp:TextBox ID="txtPiso" runat="server" CssClass="txtCuenta" />
     </p>

     <p class="info-item">
         <strong>Departamento:</strong>
         <asp:TextBox ID="txtDepto" runat="server" CssClass="txtCuenta" />
     </p>

     <p class="info-item">
         <strong>Localidad:</strong>
         <asp:TextBox ID="txtLocalidad" runat="server" CssClass="txtCuenta" />
     </p>

     <p class="info-item">
         <strong>Provincia:</strong>
         <asp:TextBox ID="txtProvincia" runat="server" CssClass="txtCuenta" />
     </p>

     <p class="info-item">
         <strong>Código Postal:</strong>
         <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="txtCuenta" />
     </p>

     <asp:Label ID="lblErrorDireccion" runat="server" CssClass="error" Visible="false"> </asp:Label>

     <asp:Button ID="btnGuardarDireccion" runat="server" Text="Guardar Dirección" CssClass="btnPrincipal" OnClick="btnGuardarDireccion_Click" />

     <asp:Button ID="btnCancelarDireccion" runat="server" Text="Cancelar" CssClass="btnSecundario" OnClick="btnCancelarDireccion_Click" />

 </div>

</div>

</asp:Content>
