<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addProposal" Title="addProposal" Codebehind="addProposal.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <%#DataBinder.Eval(Container, "DataItem.Depto.name")%>
    <script src="../Include/javascript/mdfu.js" type="text/javascript"></script>

    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        ActiveTabIndex="1">
        <cc1:TabPanel runat="server" HeaderText="Información de la Propuesta" ID="TabPanel1"
            Width="600" TabIndex="0">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lblidsummoning" runat="server" Text="Convocatoria"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:DropDownList ID="ddlSummoning" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvidsummoning" runat="server" ControlToValidate="ddlSummoning"
                                ErrorMessage="*" ValidationGroup="Principal" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpidsummoning" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lblprojectname" runat="server" Text="Nombre Proyecto"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:TextBox ID="txtprojectname" runat="server" Width="400px" MaxLength="255" TextMode="MultiLine"
                                onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvProjectname" runat="server" ControlToValidate="txtprojectname"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="Principal"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpprojectname" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lblbriefprojectdescription" runat="server" Text="Descripción Breve Proyecto"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:TextBox ID="txtbriefprojectdescription" runat="server" Width="400px" MaxLength="255"
                                TextMode="MultiLine" onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBriefprojectdescription" runat="server" ControlToValidate="txtbriefprojectdescription"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="Principal"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpbriefprojectdescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lbloperator" runat="server" Text="Operador"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:TextBox ID="txtoperator" runat="server" Width="400px" MaxLength="255" TextMode="MultiLine"
                                onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpoperator" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lbloperatornit" runat="server" Text="Nit Operador"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:TextBox ID="txtoperatornit" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpoperatornit" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lbltarget" runat="server" Text="Objetivo"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:TextBox ID="txttarget" runat="server" Width="400px" MaxLength="255" TextMode="MultiLine"
                                onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelptarget" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lbltargetpopulation" runat="server" Text="Población Objetivo"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:TextBox ID="txttargetpopulation" runat="server" Width="400px" MaxLength="255"
                                TextMode="MultiLine" onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelptargetpopulation" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lblexpectedresults" runat="server" Text="Resultados Esperados"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:TextBox ID="txtexpectedresults" runat="server" Width="400px" MaxLength="1500"
                                TextMode="MultiLine" onkeypress="return textboxAreaMaxNumber(this,1500)"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpexpectedresults" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lbltotalvalue" runat="server" Text="Valor Total"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:TextBox ID="txttotalvalue" runat="server" Width="200px" MaxLength="15"  onkeyup="format(this)" onchange="format(this)" ></asp:TextBox>
                            <%--<cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                TargetControlID="txttotalvalue"
                                Mask="9,999,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender>--%>
                            <%--<asp:CompareValidator ID="cvtotalvalue" runat="server" ControlToValidate="txttotalvalue"
                                ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                                Type="Double" Display="Dynamic"></asp:CompareValidator>--%>
                                
                        </td>
                        <td>
                            <asp:Label ID="lblHelptotalvalue" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lblinputfsc" runat="server" Text="Aporte FSC"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:TextBox ID="txtinputfsc" runat="server" Width="200px" MaxLength="15"  onkeyup="format(this)" onchange="format(this)" ></asp:TextBox>
                            <%-- <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                TargetControlID="txtinputfsc"
                                Mask="9,999,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender>--%>
                           <%-- <asp:CompareValidator ID="cvinputfsc" runat="server" ControlToValidate="txtinputfsc"
                                ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                                Type="Double" Display="Dynamic"></asp:CompareValidator>--%>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpinputfsc" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lblinputothersources" runat="server" Text="Aporte Otras Fuentes"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:TextBox ID="txtinputothersources" runat="server" Width="200px" MaxLength="15"  onkeyup="format(this)" onchange="format(this)" ></asp:TextBox>
                            <%-- <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                TargetControlID="txtinputothersources"
                                Mask="9,999,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender>--%>
                            <%--<asp:CompareValidator ID="cvinputothersources" runat="server" ControlToValidate="txtinputothersources"
                                ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                                Type="Double" Display="Dynamic"></asp:CompareValidator>--%>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpinputothersources" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:DropDownList runat="server" ID="ddlenabled">
                                <asp:ListItem Text="Habilitado" Value="True"></asp:ListItem>
                                <asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpresponsiblereview" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td> 
                            <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de Creación"></asp:Label>
                        </td>
                        <td style="width: 437px">
                            <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" TabIndex="1" runat="server" HeaderText="Archivos Anexos">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                           <table id="tableAttachments">
                                <tr>
                                    <td style="width: 20%">
                                        <asp:Label ID="lblattachfile" runat="server" Text="Archivo anexo"></asp:Label>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td id="tdFileInputs" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <img src="../App_Themes/GattacaAdmin/Images/attach.gif" alt="" />
                                                    <a id="lnkAttch" onmouseover="this.style.textDecoration='underline'" onmouseout="this.style.textDecoration='none'"
                                                        style="cursor: hand" onclick="AddFileInput()">Adjuntar un archivo</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="upData" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="~/ResearchAndDevelopment/addDocuments.aspx?op=edit&id={0}&isPopup=True"
                                                HeaderText="Edición" Text="Editar" Target="_blank" />
                                            <asp:TemplateField HeaderText="Eliminación" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                        OnClientClick="return confirm('Esta seguro?')" Text="Eliminar"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="title" HeaderText="Título" />
                                            <asp:BoundField DataField="description" HeaderText="Descripción" />
                                            <asp:BoundField DataField="EDITEDFORNAME" HeaderText="Editado por" />
                                            <asp:BoundField DataField="VISIBILITYLEVELNAME" HeaderText="Nivel visibilidad" />
                                            <asp:BoundField DataField="DOCUMENTTYPENAME" HeaderText="Tipo documento" />
                                            <asp:BoundField DataField="createdate" HeaderText="Fecha" />
                                            <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                                            <asp:HyperLinkField DataNavigateUrlFields="attachfile" DataTextField="attachfile"
                                                HeaderText="Anexo" Target="_blank" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <asp:Button ID="btnRefresh" runat="server" Text="Actualizar Listado" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" TabIndex="1" runat="server" HeaderText="Ubicación">
            <HeaderTemplate>
                Ubicación
            </HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Table ID="tableInfoLocations" runat="server" Width="100%">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 172px">
                                                <asp:Label ID="Label1" runat="server" Text="Departamento"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDepto" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepto"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="Ubicacion"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 172px">
                                                <asp:Label ID="Label3" runat="server" Text="Municipio"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCity" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCity"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="Ubicacion"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 172px">
                                                <asp:Button ID="btnAgregarubicacion" runat="server" CausesValidation="False" Text="Agregar Ubicación"
                                                    ValidationGroup="Ubicacion" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLocationMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gvLocations" runat="server" AutoGenerateColumns="False" Width="100%">
                                                    <Columns>
                                                        <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                                                        <asp:TemplateField HeaderText="Departamento">
                                                            <ItemTemplate>
                                                                <%#DataBinder.Eval(Container, "DataItem.Depto.name")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ciudad">
                                                            <ItemTemplate>
                                                                <%#DataBinder.Eval(Container, "DataItem.City.name")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Registro de Revisión">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width: 171px">
                                    <asp:Label ID="lblscore" runat="server" Text="Puntaje"></asp:Label>
                                </td>
                                <td style="width: 437px">
                                    <asp:TextBox ID="txtscore" runat="server" Width="200px" MaxLength="15"></asp:TextBox>
                                    <asp:CompareValidator ID="cvscore" runat="server" ControlToValidate="txtscore" ErrorMessage="Valor numérico"
                                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" Display="Dynamic"></asp:CompareValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpscore" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 171px">
                                    <asp:Label ID="lblresult" runat="server" Text="Resultado"></asp:Label>
                                </td>
                                <td style="width: 437px">
                                    <asp:DropDownList ID="ddlResult" runat="server">
                                       <asp:ListItem>Aprobado</asp:ListItem>
                                        <asp:ListItem>Pendiente</asp:ListItem>
                                        <asp:ListItem>Rechazado</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpresult" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 171px">
                                    <asp:Label ID="lblresponsiblereview" runat="server" Text="Responsable Revisión"></asp:Label>
                                </td>
                                <td style="width: 437px">
                                    <asp:TextBox ID="txtresponsiblereview" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpreviewdate" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 171px">
                                    <asp:Label ID="lblreviewdate" runat="server" Text="Fecha Revisión"></asp:Label>
                                </td>
                                <td style="width: 437px">
                                    <asp:TextBox ID="txtreviewdate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpenabled" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <%#DataBinder.Eval(Container, "DataItem.City.name")%>
    <table style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblBPMMessage" runat="server" Text="Por Favor Seleccione la Actividad"
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rblCondition" runat="server" Visible="False">
                            </asp:RadioButtonList>
                        </td>
                    </tr>        <tr>
            <td colspan="3">
                <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" ValidationGroup="Principal" />
                <asp:Button ID="btnSave" runat="server" Text="Guardar" ValidationGroup="Principal" />
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
    </table>
</asp:Content>
