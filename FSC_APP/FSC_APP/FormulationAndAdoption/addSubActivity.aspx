<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addSubActivity" Title="addSubActivity" Codebehind="addSubActivity.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpid" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblidproject" runat="server" Text="Proyecto"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidproject" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblHelpidproject" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblidobjective" runat="server" Text="Objetivo"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidobjective" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblHelpidobjective" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblidcomponent" runat="server" Text="Componente"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidcomponent" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblHelpidcomponent" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblidactivity" runat="server" Text="Actividad"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidactivity" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvidactivity" runat="server" ControlToValidate="ddlidactivity"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpidactivity" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbltype" runat="server" Text="Tipo"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddltype" runat="server">
                    <asp:ListItem Value="1">Actividad</asp:ListItem>
                    <asp:ListItem Value="2">Visita</asp:ListItem>
                    <asp:ListItem Value="3">Registro de Ejecución</asp:ListItem>
                     <asp:ListItem Value="4">Actividad  FSC</asp:ListItem>
                    <asp:ListItem Value="5">Actividad operador</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvtype" runat="server" ControlToValidate="ddltype"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelptype" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblnumber" runat="server" Text="Código"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtnumber" runat="server" Width="400px" MaxLength="50" AutoCompleteType="Disabled"
                    AutoPostBack="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvnumber" runat="server" ControlToValidate="txtnumber"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpnumber" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblname" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtname" runat="server" Width="400px" MaxLength="255" Height="70px"
                    TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="txtname"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpname" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbldescription" runat="server" Text="Descripción"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtdescription" runat="server" Width="400px" MaxLength="100" TextMode="MultiLine"
                    Height="50px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblHelpdescription" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblresponsible" runat="server" Text="Responsable"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidresponsible" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvidresponsible" runat="server" ControlToValidate="ddlidresponsible"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpresponsible" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblbegindate" runat="server" Text="Fecha Inicio"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtbegindate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <cc1:CalendarExtender ID="cesbegindate" runat="server" TargetControlID="txtbegindate"
                    Format="yyyy/MM/dd" Enabled="True">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvbegindate" runat="server" ErrorMessage="aaaa/mm/dd" Type="Date"
                    ControlToValidate="txtbegindate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpbegindate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblenddate" runat="server" Text="Fecha Final"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtenddate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <cc1:CalendarExtender ID="ceenddate" runat="server" TargetControlID="txtenddate"
                    Format="yyyy/MM/dd" Enabled="True">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvenddate" runat="server" ErrorMessage="aaaa/mm/dd" Type="Date"
                    ControlToValidate="txtenddate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
							
                            <asp:CompareValidator runat="server" ControlToCompare="txtbegindate" 
                                Operator="GreaterThanEqual" ControlToValidate="txtenddate" 
                                ErrorMessage="El valor de la fecha inicial no puede ser superior al valor de la fecha final" 
                                Display="Dynamic" SetFocusOnError="True" ID="cvFechafin"></asp:CompareValidator>
							
            </td>
            <td>
                <asp:Label ID="lblHelpenddate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbltotalcost" runat="server" Text="Costo total"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txttotalcost" runat="server" Width="400px" MaxLength="12" 
                    onkeyup="format(this)" onchange="format(this)"  ></asp:TextBox>
                <%--   <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Double" ControlToValidate="txtofcontribution"
                    ErrorMessage="Valor Num&#233;rico" SetFocusOnError="True" ValidationGroup="IndicatorForm"
                    ID="cvofcontribution"></asp:CompareValidator>--%>
                    <%-- <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                TargetControlID="txttotalcost"
                                Mask="9,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                 AutoComplete="False"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender> --%>
                </td>
            <td>
                <asp:Label ID="lblHelptotalcost" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblduration" runat="server" Text="Duración en dias"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtduration" runat="server" Width="400px" MaxLength="9"></asp:TextBox>
                <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtduration"
                    ErrorMessage="Valor Num&#233;rico" SetFocusOnError="True" ValidationGroup="IndicatorForm"
                    ID="cvduration"></asp:CompareValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpduration" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblfsccontribution" runat="server" Text="Aporte FSC"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtfsccontribution" runat="server" Width="400px" MaxLength="12"  onkeyup="format(this)" onchange="format(this)" ></asp:TextBox>
                <%--   <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Double" ControlToValidate="txtofcontribution"
                    ErrorMessage="Valor Num&#233;rico" SetFocusOnError="True" ValidationGroup="IndicatorForm"
                    ID="cvofcontribution"></asp:CompareValidator>--%>
                      <%--      <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                TargetControlID="txtfsccontribution"
                                Mask="9,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                 AutoComplete="False"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender> --%>
            </td>
            <td>
                <asp:Label ID="lblHelpfsccontribution" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblofcontribution" runat="server" Text="Aporte OF"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtofcontribution" runat="server" Width="400px" MaxLength="12"  onkeyup="format(this)" onchange="format(this)"  ></asp:TextBox>
             <%--   <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Double" ControlToValidate="txtofcontribution"
                    ErrorMessage="Valor Num&#233;rico" SetFocusOnError="True" ValidationGroup="IndicatorForm"
                    ID="cvofcontribution"></asp:CompareValidator>--%>
                     <%--   <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                TargetControlID="txtofcontribution"
                                Mask="9,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender>--%> 
            </td>
            <td>
                <asp:Label ID="lblHelpofcontribution" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblattachment" runat="server" Text="Archivo"></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="fuattachment" runat="server" />
                <asp:HyperLink ID="hlattachment" runat="server" Visible="false" Target="_blank">Descargar</asp:HyperLink>
            </td>
            <td>
                <asp:Label ID="lblHelpattachment" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblcriticalpath" runat="server" Text="Ruta Crítica"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="cbcriticalpath" runat="server" />
            </td>
            <td>
                <asp:Label ID="lblHelpcriticalpath" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblrequiresapproval" runat="server" Text="Requiere aprobación"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="cbrequiresapproval" runat="server" />
            </td>
            <td>
                <asp:Label ID="lblHelprequiresapproval" runat="server" Text=""></asp:Label>
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
                <asp:RequiredFieldValidator ID="rfvenabled" runat="server" ControlToValidate="ddlenabled"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpenabled" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de creación"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" ControlToValidate="txtcreatedate"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpcreatedate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" />
                <asp:Button ID="btnSave" runat="server" Text="Guardar" />
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
							<asp:Label ID="lblVersion" runat="server" Text="Versiones Anteriores"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
							<asp:GridView ID="gvVersion" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="createdate" HeaderText="Fecha" />
                                    <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                                    <asp:BoundField DataField="number" HeaderText="Código" />
                                    <asp:HyperLinkField DataNavigateUrlFields="id" 
                                        DataNavigateUrlFormatString="addSubActivity.aspx?op=show&amp;id={0}&consultLastVersion=false"
                                        DataTextField="Name" HeaderText="Nombre/Descripción" Target="_blank" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
    </table>
</asp:Content>
