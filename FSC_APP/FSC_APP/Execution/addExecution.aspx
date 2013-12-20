<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addExecution" title="addExecution" Codebehind="addExecution.aspx.vb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">


 <script src="../Include/javascript/mdfu.js" type="text/javascript"></script>
   <script type="text/javascript" language="javascript">
         function textboxMultilineMaxNumber(txt, maxLen) {
             try {
                 if (txt.value.length > (maxLen - 1))

                     return false;
             } catch (e) {
             }
         }  
    </script>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        ActiveTabIndex="0">
        <cc1:TabPanel runat="server" HeaderText="Información General" ID="TabPanel1" Width="600"
            TabIndex="0">
            <HeaderTemplate>
            Información General</HeaderTemplate>
            <ContentTemplate><table style="width: 100%"><tr><td><asp:Label ID="lblid" runat="server" Text="Id"></asp:Label></td><td><asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"></asp:RequiredFieldValidator></td><td><asp:Label ID="lblHelpid" runat="server"></asp:Label></td></tr><tr><td><asp:Label ID="lblproject" runat="server" Text="Proyecto"></asp:Label></td><td><asp:DropDownList ID="ddlProject" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvidproject" runat="server" 
                                ControlToValidate="ddlProject" ErrorMessage="*"></asp:RequiredFieldValidator></td><td><asp:Label ID="lblHelpidproject" runat="server"></asp:Label></td></tr><tr><td><asp:Label 
                    ID="lblqualitativeindicators" runat="server" 
                                Text="Indicador Cualitativo "></asp:Label></td><td><asp:TextBox ID="txtqualitativeindicators" runat="server" Width="400px" 
                                MaxLength="2000"  onkeypress="return textboxMultilineMaxNumber(this,2000)" TextMode="MultiLine"></asp:TextBox></td><td><asp:Label ID="lblHelpqualitativeindicators" runat="server"></asp:Label></td></tr><tr><td><asp:Label 
                    ID="lblenable" runat="server" Text="Estado"></asp:Label></td><td><asp:DropDownList ID="ddlenabled" runat="server"><asp:ListItem Text="Habilitado" Value="True"></asp:ListItem><asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem></asp:DropDownList><asp:RequiredFieldValidator ID="rfvenable" runat="server" 
                                ControlToValidate="ddlenabled" ErrorMessage="*"></asp:RequiredFieldValidator></td><td><asp:Label ID="lblHelpenable" runat="server"></asp:Label></td></tr><tr><td><asp:Label 
                    ID="lbliduser" runat="server" Text="Usuario"></asp:Label></td><td><asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser" ErrorMessage="*"></asp:RequiredFieldValidator></td><td><asp:Label ID="lblHelpiduser" runat="server"></asp:Label></td></tr><tr><td><asp:Label 
                    ID="lblcreatedate" runat="server" Text="Fecha Creación"></asp:Label></td><td><asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" ControlToValidate="txtcreatedate" ErrorMessage="*"></asp:RequiredFieldValidator></td><td><asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label></td></tr></table>
                           
               
            </ContentTemplate>
        </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Testimonio" ID="TabPanel3" Width="600"
            TabIndex="0">
            <HeaderTemplate>
            Testimonio</HeaderTemplate>
           <ContentTemplate><asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate><table width="100%"><tr><td style="width: 148px"><asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label></td><td><asp:TextBox ID="txtName" runat="server" MaxLength="100" Width="515px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                       ControlToValidate="txtName" ErrorMessage="RequiredFieldValidator" 
                       SetFocusOnError="True" ToolTip="Requerido" ValidationGroup="Testimony">*</asp:RequiredFieldValidator></td></tr><tr><td style="width: 148px"><asp:Label ID="lblEdad" runat="server" Text="Edad"></asp:Label></td><td><asp:TextBox ID="txtAge" runat="server" MaxLength="50" Width="514px"></asp:TextBox></td></tr><tr><td style="width: 148px"><asp:Label ID="lblgenero" runat="server" Text="Género"></asp:Label></td><td><asp:DropDownList ID="ddlSex" runat="server"><asp:ListItem>Hombre</asp:ListItem><asp:ListItem>Mujer</asp:ListItem></asp:DropDownList></td></tr><tr><td style="width: 148px"><asp:Label ID="lblTelefono" runat="server" Text="Teléfono"></asp:Label></td><td><asp:TextBox ID="txtPhone" runat="server" MaxLength="50" Width="514px"></asp:TextBox></td></tr><tr><td style="width: 148px"><asp:Label ID="lblDepartamento" runat="server" Text="Departamento"></asp:Label></td><td><asp:DropDownList ID="ddlDepto" runat="server" AutoPostBack="True"></asp:DropDownList></td></tr><tr><td style="width: 148px"><asp:Label ID="lblMunicipio" runat="server" Text="Municipio"></asp:Label></td><td><asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvcity" runat="server" 
                           ControlToValidate="ddlCity" ErrorMessage="RequiredFieldValidator" 
                           SetFocusOnError="True" ToolTip="Requerido" ValidationGroup="Testimony">*</asp:RequiredFieldValidator></td></tr><tr><td style="width: 148px"><asp:Label 
                       ID="lblEmail" runat="server" Text="Correo Electrónico"></asp:Label></td><td><asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="511px"></asp:TextBox><asp:RegularExpressionValidator 
                           ID="RegularExpressionValidator2" runat="server" 
                           ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Correo Electrónico inválido" 
                           SetFocusOnError="True" 
                           ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td></tr><tr><td style="width: 148px"><asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label></td><td><asp:TextBox ID="txtDecription" runat="server" MaxLength="255" 
                           onkeypress="return textboxMultilineMaxNumber(this,255)" Rows="3" 
                           TextMode="MultiLine" Width="500px"></asp:TextBox></td></tr><tr><td style="width: 148px"><asp:Label ID="lblRol" runat="server" 
                       Text="Rol en el proyecto"></asp:Label></td><td><asp:TextBox ID="txtprojectrole" runat="server" MaxLength="100" 
                         Rows="3" Width="500px"></asp:TextBox></td></tr><tr><td style="width: 148px"><asp:Button ID="btnAddTestimony" runat="server" Text="Agregar Testimonio" 
                       ValidationGroup="Testimony" /></td><td>&#160;&#160; </td></tr><tr><td colspan="2"><hr /></td></tr><tr><td colspan="2"><asp:GridView ID="gvTestimony" runat="server" AutoGenerateColumns="False" 
                                                        Width="100%"><Columns><asp:CommandField SelectText="Quitar" ShowSelectButton="True" /><asp:TemplateField HeaderText="Nombre">  <ItemTemplate> <%# DataBinder.Eval(Container, "DataItem.name")%> </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Edad"><ItemTemplate> <%# DataBinder.Eval(Container, "DataItem.age")%> </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Genero"><ItemTemplate><%#DataBinder.Eval(Container, "DataItem.sex")%></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Teléfono"><ItemTemplate><%#DataBinder.Eval(Container, "DataItem.phone")%></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Correo Electrónico"><ItemTemplate><%# DataBinder.Eval(Container, "DataItem.email")%></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Departamento"><ItemTemplate><%#DataBinder.Eval(Container, "DataItem.Departamento")%></ItemTemplate></asp:TemplateField></Columns></asp:GridView></td></tr></table></ContentTemplate></asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Aprendizaje, Ajustes y Logros" ID="TabPanel4" Width="600"
            TabIndex="0">
            <HeaderTemplate>
            Aprendizaje, Ajustes y Logros</HeaderTemplate>
            <ContentTemplate><table style="width: 100%"><tr><td><asp:Label ID="lbllearning" runat="server" Text="Aprendizaje"></asp:Label></td><td><asp:TextBox ID="txtlearning" runat="server" Width="400px" MaxLength="4000"  onkeypress="return textboxMultilineMaxNumber(this,4000)"
                                TextMode="MultiLine"></asp:TextBox></td><td><asp:Label ID="lblHelplearning" runat="server"></asp:Label></td></tr><tr><td><asp:Label ID="lbladjust" runat="server" Text="Ajustes"></asp:Label></td><td><asp:TextBox ID="txtadjust" runat="server" Width="400px" MaxLength="4000"   onkeypress="return textboxMultilineMaxNumber(this,4000)"
                                TextMode="MultiLine"></asp:TextBox></td><td><asp:Label ID="lblHelpadjust" runat="server"></asp:Label></td></tr><tr><td><asp:Label ID="lblachievements" runat="server" Text="Otros Logros"></asp:Label></td><td><asp:TextBox ID="txtachievements" runat="server" Width="400px" MaxLength="4000"  onkeypress="return textboxMultilineMaxNumber(this,4000)"
                                TextMode="MultiLine"></asp:TextBox></td><td><asp:Label ID="lblHelpachievements" runat="server"></asp:Label></td></tr></table>
             </ContentTemplate>
        </cc1:TabPanel>
          <cc1:TabPanel ID="TabPanel2" TabIndex="1" runat="server" HeaderText="Archivos Anexos">
            <ContentTemplate><table width="100%"><tr><td>
            <table id="tableAttachments">
            <tr><td style="width: 20%">
            <asp:Label ID="lblattachfile" runat="server" Text="Archivo anexo"></asp:Label>
            </td><td><table><tr><td id="tdFileInputs" valign="top">
            </td></tr><tr><td nowrap="nowrap">
            <img src="../App_Themes/GattacaAdmin/Images/attach.gif" alt="" /> 
            <a id="lnkAttch" onmouseover="this.style.textDecoration='underline'" onmouseout="this.style.textDecoration='none'"
                                                        style="cursor: hand" onclick="AddFileInput()">Adjuntar un archivo</a> </td></tr></table></td></tr>
                                                        </table></td></tr><tr><td><hr /></td></tr><tr><td><asp:UpdatePanel ID="upData" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="False" Width="100%"><Columns><asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addDocuments.aspx?op=edit&id={0}&isPopup=True"
                                                HeaderText="Edición" Text="Editar" Target="_blank" /><asp:TemplateField HeaderText="Eliminación" ShowHeader="False"><ItemTemplate><asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                        OnClientClick="return confirm('Esta seguro?')" Text="Eliminar"></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:BoundField DataField="title" HeaderText="Título" /><asp:BoundField DataField="description" HeaderText="Descripción" /><asp:BoundField DataField="EDITEDFORNAME" HeaderText="Editado por" /><asp:BoundField DataField="VISIBILITYLEVELNAME" HeaderText="Nivel visibilidad" /><asp:BoundField DataField="DOCUMENTTYPENAME" HeaderText="Tipo documento" /><asp:BoundField DataField="createdate" HeaderText="Fecha" /><asp:BoundField DataField="USERNAME" HeaderText="Usuario" /><asp:HyperLinkField DataNavigateUrlFields="attachfile" DataTextField="attachfile"
                                                HeaderText="Anexo" Target="_blank" /></Columns></asp:GridView><br /><asp:Button ID="btnRefresh" runat="server" Text="Actualizar Listado" /></ContentTemplate></asp:UpdatePanel></td></tr></table>
            </ContentTemplate>
        </cc1:TabPanel>
    
    </cc1:TabContainer>
                <table style="width: 100%">
        <tr>
            <td colspan="3">
                <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" 
                    ValidationGroup="infoGenral" />
                <asp:Button ID="btnSave" runat="server" Text="Guardar" 
                    ValidationGroup="infoGenral" />
                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" 
                    CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" 
                    CausesValidation="False" />
                <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" 
                    CausesValidation="False" />
                &nbsp;<asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

