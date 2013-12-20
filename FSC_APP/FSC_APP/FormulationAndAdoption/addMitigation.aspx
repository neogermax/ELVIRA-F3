<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addMitigation" Title="addMitigation" Codebehind="addMitigation.aspx.vb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register Assembly="DoubleListBox" Namespace="DoubleListBox" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script type="text/javascript" language="javascript">
        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1))

                    return false;
            } catch (e) {
            }
        }  
    </script>

    <cc2:TabContainer ID="TabContainer1" runat="server" Height="500px" Width="810px"
        ActiveTabIndex="0" ScrollBars="Vertical">
        <cc2:TabPanel runat="server" HeaderText="Datos generales del riesgo" ID="TabPanel1"
            Width="600" TabIndex="0">
            <ContentTemplate>
                <div>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvid" runat="server"  ValidationGroup="mitigationForm" ControlToValidate="txtid" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td> 
                <asp:Label ID="lblHelpid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblcode" runat="server" Text="Código"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtcode" runat="server" Width="400px" MaxLength="50" AutoPostBack="True"
                    AutoCompleteType="Disabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvcode" runat="server" ValidationGroup="mitigationForm"  ControlToValidate="txtcode"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpcode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblname" runat="server" Text="Nombre"></asp:Label>
                /Descipción
            </td>
            <td>
                <asp:TextBox ID="txtname" runat="server" Height="70px" MaxLength="255" 
                    TextMode="MultiLine" Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvname" runat="server" ValidationGroup="mitigationForm"  ControlToValidate="txtname"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpname" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblimpactonrisk" runat="server" Text="Impacto sobre el riesgo"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlimpactonrisk" runat="server">
                    <asp:ListItem Value="1">Alta</asp:ListItem>
                    <asp:ListItem Value="2">Media</asp:ListItem>
                    <asp:ListItem Value="3">Baja</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvimpactonrisk" runat="server" 
                    ControlToValidate="ddlimpactonrisk" ErrorMessage="*" 
                    ValidationGroup="mitigationForm"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpimpactonrisk" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblrisponsable" runat="server" Text="Responsable"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidresponsible" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvrisponsable" runat="server" 
                    ControlToValidate="ddlidresponsible" ValidationGroup="mitigationForm"  
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelprisponsable" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlenabled" runat="server">
                    <asp:ListItem Text="Habilitado" Value="True"></asp:ListItem>
                    <asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvenabled" runat="server" 
                    ValidationGroup="mitigationForm"  ControlToValidate="ddlenabled"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpenabled" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtiduser" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfviduser" runat="server" 
                    ValidationGroup="mitigationForm"  ControlToValidate="txtiduser"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha Creación"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtcreatedate" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" 
                    ValidationGroup="mitigationForm"  ControlToValidate="txtcreatedate"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label>
            </td>
        </tr>
       
    </table>
    </div>
      </ContentTemplate>
        </cc2:TabPanel>
          <cc2:TabPanel runat="server" HeaderText="Riesgos" ID="TabPanel2" Width="600"
            TabIndex="1">
            <ContentTemplate>
            <div>
             <table id="Table1" border="0" cellpadding="1" cellspacing="1" width="100%">
                        <tbody>
                            <tr>
                                <td>
                                         <cc1:DoubleListBox Enabled="true"  ID="dlbRisk" runat="server" Width="100%" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    
                     <table width="90%">
                        <tr>
                            <td>
                                <asp:Label ID="lblHelpMetigationByProject" runat="server" Text="Recuerde hacer click en guardar para efectuar los cambios"
                                    ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
      </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>
   
   

 
     <table style="width: 100%">
     <tr>
            <td colspan="3">
                <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" ValidationGroup="mitigationForm"  />
                <asp:Button ID="btnSave" runat="server" Text="Guardar" ValidationGroup="mitigationForm"  />
                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                &nbsp;<asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="3">
               <%-- <asp:Label ID="lblVersion" runat="server" Text="Versiones Anteriores"></asp:Label>--%>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <%--<asp:GridView ID="gvVersion" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="createdate" HeaderText="Fecha" />
                        <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                        <asp:BoundField DataField="code" HeaderText="Codigo" />
                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addMitigation.aspx?op=show&amp;id={0}&consultLastVersion=false"
                            DataTextField="Name" HeaderText="Nombre" Target="_blank" />
                    </Columns>
                </asp:GridView>--%>
            </td>
        </tr>
    </table>
</asp:Content>
