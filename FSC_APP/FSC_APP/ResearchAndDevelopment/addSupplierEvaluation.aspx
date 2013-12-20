<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addSupplierEvaluation" Title="addSupplierEvaluation" Codebehind="addSupplierEvaluation.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="1">
        <cc1:TabPanel runat="server" HeaderText="Datos Básicos" ID="TabPanel1" Width="600"
            TabIndex="0">
            <HeaderTemplate>
                Datos Básicos
            </HeaderTemplate>
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 137px">
                            <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 137px">
                            <asp:Label ID="lblidsupplier" runat="server" Text="N proveedor"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlThird" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpidsupplier" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 137px">
                            <asp:Label ID="lblcontractnumber" runat="server" Text="Número del contrato"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcontractnumber" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvcontractnumber" runat="server" ControlToValidate="txtcontractnumber"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcontractnumber" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 137px">
                            <asp:Label ID="lblcontractstartdate" runat="server" Text="Fecha inicial contrato"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcontractstartdate" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            <cc1:CalendarExtender ID="cecontractstartdate" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                TargetControlID="txtcontractstartdate">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvcontractstartdate" runat="server" ControlToValidate="txtcontractstartdate"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Date" ControlToValidate="txtcontractstartdate"
                                ErrorMessage="yyyy/MM/dd" SetFocusOnError="True" ID="cvstartdate"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcontractstartdate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 137px">
                            <asp:Label ID="lblcontractenddate" runat="server" Text="Fecha final contrato"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcontractenddate" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            <cc1:CalendarExtender ID="cecontractenddate" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                TargetControlID="txtcontractenddate">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvcontractenddate" runat="server" ControlToValidate="txtcontractenddate"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Date" ControlToValidate="txtcontractenddate"
                                ErrorMessage="yyyy/MM/dd" SetFocusOnError="True" ID="cvstartdate0" Display="Dynamic"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtcontractstartdate"
                                ControlToValidate="txtcontractenddate" Display="Dynamic" ErrorMessage="El valor de la fecha inicial no puede ser superior al valor de la fecha final"
                                Operator="GreaterThanEqual" SetFocusOnError="True"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcontractenddate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 137px">
                            <asp:Label ID="lblcontractsubject" runat="server" Text="Objeto del contrato"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcontractsubject" runat="server" Width="400px" MaxLength="255"
                                Rows="2" TextMode="MultiLine" onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcontractsubject" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 137px">
                            <asp:Label ID="lblcontractvalue" runat="server" Text="Valor del contrato"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcontractvalue" runat="server" Width="200px" MaxLength="15" onkeyup="format(this)" onchange="format(this)" ></asp:TextBox>
                         <%--   <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Currency" ControlToValidate="txtcontractvalue"
                                ErrorMessage="Valor num&#233;rico" SetFocusOnError="True" ID="cvcost"></asp:CompareValidator>--%>
                                <%-- <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                TargetControlID="txtcontractvalue"
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
                            <asp:Label ID="lblHelpcontractvalue" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 137px">
                            <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 137px">
                            <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de registro"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" ControlToValidate="txtcreatedate"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Calificación Proveedor">
            <HeaderTemplate>
                Calificación Proveedor
            </HeaderTemplate>
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="Label1" runat="server" Text="CUMPLIMIENTO" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label2" runat="server" Text="Objeto del contrato orden de compra o servicio"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlContractSubject" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label3" runat="server" Text="Obligaciones contractuales"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlContractualObligations" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label4" runat="server" Text="Metas definidas"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDefinedGoals" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label5" runat="server" Text="Plazoz acordados"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAgreedDeadlines" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label6" runat="server" Text="Totalidad de los productos y/o servicios entregados"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTotalityDeliveredProducts" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label7" runat="server" Text="Solicitudes realizadas por FSC"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRequestsMadeByFSC" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label9" runat="server" Text="Total puntaje obtenido"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPuntajeCumplimiento" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label10" runat="server" Text="Calificación obtenida"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCalificacionCumplimiento" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label11" runat="server" Text="Porcentaje"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPorcentajeCumplimiento" runat="server" Width="50px"></asp:TextBox>
                            &#160;%<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPorcentajeCumplimiento"
                                Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                                ValidationGroup="calificacion">*</asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPorcentajeCumplimiento"
                                Display="Dynamic" ErrorMessage="Debe digitar un valor entre 0 (cero) y 100 (cien)"
                                MaximumValue="100" MinimumValue="0" SetFocusOnError="True" Type="Double" ValidationGroup="calificacion"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label12" runat="server" Text="Ponderación"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPonderacionCumplimiento" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="Label8" runat="server" Text="OPORTUNIDAD" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label13" runat="server" Text="Entrega de productos y/o servicios"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDeliveryProductsServices" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label14" runat="server" Text="Presentación de informes"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlReporting" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label15" runat="server" Text="Total puntaje obtenido"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPuntajeOportunidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label16" runat="server" Text="Calificación obtenida"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCalificacionOportunidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label17" runat="server" Text="Porcentaje"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPorcentajeOportunidad" runat="server" Width="50px"></asp:TextBox>
                            &#160;%<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPorcentajeOportunidad"
                                Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                                ValidationGroup="calificacion">*</asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPorcentajeOportunidad"
                                Display="Dynamic" ErrorMessage="Debe digitar un valor entre 0 (cero) y 100 (cien)"
                                MaximumValue="100" MinimumValue="0" SetFocusOnError="True" Type="Double" ValidationGroup="calificacion"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label18" runat="server" Text="Ponderación"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPonderacionOportunidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="Label19" runat="server" Text="CALIDAD" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label20" runat="server" Text="Calidad del producto o servicio entregado"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProductQuality" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label21" runat="server" Text="Calidad de los informes"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlReportsQuality" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label22" runat="server" Text="Calidad del acompañamiento"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAccompanimentQuality" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label23" runat="server" Text="Atención a las solicitudes quejas reclamos"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAttentionComplaintsClaims" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label24" runat="server" Text="Productos devueltos o no conformes por incumplimiento de requisitos"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlReturnedProducts" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label25" runat="server" Text="Total puntaje obtenido"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPuntajeCalidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label26" runat="server" Text="Calificación obtenida"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCalificacionCalidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label27" runat="server" Text="Porcentaje"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPorcentajeCalidad" runat="server" Width="50px"></asp:TextBox>
                            &#160;%<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPorcentajeCalidad"
                                Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                                ValidationGroup="calificacion">*</asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtPorcentajeCalidad"
                                Display="Dynamic" ErrorMessage="Debe digitar un valor entre 0 (cero) y 100 (cien)"
                                MaximumValue="100" MinimumValue="0" SetFocusOnError="True" Type="Double" ValidationGroup="calificacion"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label28" runat="server" Text="Ponderación"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPonderacionCalidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="Label29" runat="server" Text="VALOR AGREGADO" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label30" runat="server" Text="Del producto o servicio"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProductValueAdded" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label31" runat="server" Text="Del acompañamiento"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAccompanimentValueAdded" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label32" runat="server" Text="De los informes solicitados"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlReportsValueAdded" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label33" runat="server" Text="Total puntaje obtenido"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPuntajeValorAgregado" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label34" runat="server" Text="Calificación obtenida"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCalificacionValorAgregado" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label35" runat="server" Text="Porcentaje"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPorcentajeValorAgregado" runat="server" Width="50px"></asp:TextBox>
                            &#160;%<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPorcentajeValorAgregado"
                                Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                                ValidationGroup="calificacion">*</asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtPorcentajeValorAgregado"
                                Display="Dynamic" ErrorMessage="Debe digitar un valor entre 0 (cero) y 100 (cien)"
                                MaximumValue="100" MinimumValue="0" SetFocusOnError="True" Type="Double" ValidationGroup="calificacion"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label36" runat="server" Text="Ponderación"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPonderacionValorAgregado" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="Label37" runat="server" Text="METODOLOGÍA" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label38" runat="server" Text="Planeación del proyecto"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProjectPlaneacion" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label39" runat="server" Text="Metodología implementada"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMethodologyImplemented" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label40" runat="server" Text="Organización en desarrollo del proyecto"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDevelopmentProjectOrganization" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label41" runat="server" Text="Articulación con las actividades"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlJointActivities" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label42" runat="server" Text="Control del proyecto en general"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProjectControl" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label43" runat="server" Text="Total puntaje obtenido"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPuntajeMetodologia" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label44" runat="server" Text="Calificación obtenida"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCalificacionMetodologia" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label45" runat="server" Text="Porcentaje"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPorcentajeMetodologia" runat="server" Width="50px"></asp:TextBox>
                            &#160;%<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPorcentajeMetodologia"
                                Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                                ValidationGroup="calificacion">*</asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtPorcentajeMetodologia"
                                Display="Dynamic" ErrorMessage="Debe digitar un valor entre 0 (cero) y 100 (cien)"
                                MaximumValue="100" MinimumValue="0" SetFocusOnError="True" Type="Double" ValidationGroup="calificacion"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label46" runat="server" Text="Ponderación"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPonderacionMetodologia" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="Label47" runat="server" Text="COMPETENCIA DEL PERSONAL PRESTADOR DEL SERVICIO"
                                Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label48" runat="server" Text="Competencia del personal prestador del servicio"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlServiceStaffCompetence" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label49" runat="server" Text="Competencia general del proveedor / contratista"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSupplierCompetence" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label50" runat="server" Text="Total puntaje obtenido"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPuntajeCompetenciaPersonal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label51" runat="server" Text="Calificación obtenida"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCalificacionCompetenciaPersonal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label52" runat="server" Text="Porcentaje"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPorcentajeCompetenciaPersonal" runat="server" Width="50px"></asp:TextBox>
                            &#160;%<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPorcentajeCompetenciaPersonal"
                                Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                                ValidationGroup="calificacion">*</asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtPorcentajeCompetenciaPersonal"
                                Display="Dynamic" ErrorMessage="Debe digitar un valor entre 0 (cero) y 100 (cien)"
                                MaximumValue="100" MinimumValue="0" SetFocusOnError="True" Type="Double" ValidationGroup="calificacion"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label53" runat="server" Text="Ponderación"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPonderacionCompetenciaPersonal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="Label54" runat="server" Text="CONFIDENCIALIDAD" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label55" runat="server" Text="Manejo confidencial de la información"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlInformationConfidentiality" runat="server">
                                <asp:ListItem Value="0">NA</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label56" runat="server" Text="Total puntaje obtenido"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPuntajeConfidencialidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label57" runat="server" Text="Calificación obtenida"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCalificacionConfidencialidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label58" runat="server" Text="Porcentaje"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPorcentajeConfidencialidad" runat="server" Width="50px"></asp:TextBox>
                            &#160;%<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPorcentajeConfidencialidad"
                                Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                                ValidationGroup="calificacion">*</asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator7" runat="server" ControlToValidate="txtPorcentajeConfidencialidad"
                                Display="Dynamic" ErrorMessage="Debe digitar un valor entre 0 (cero) y 100 (cien)"
                                MaximumValue="100" MinimumValue="0" SetFocusOnError="True" Type="Double" ValidationGroup="calificacion"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label59" runat="server" Text="Ponderación"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPonderacionConfidencialidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="Label60" runat="server" Text="TOTAL DEL PORCENTAJE" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label61" runat="server" Text="Cumplimiento"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPorcentajeCumplimiento" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label62" runat="server" Text="Oportunidad"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPorcentajeOprtunidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label63" runat="server" Text="Calidad"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPorcentajeCalidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label64" runat="server" Text="Valor agregado"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPorcentajeValorAgregado" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label65" runat="server" Text="Metodología"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPorcentajeMetodologia" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label66" runat="server" Text="Competencia prestador de servicio"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPorcentajeCompetenciaPrestadorServicio" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label67" runat="server" Text="Confidencialidad"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPorcentajeConfidencialidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label68" runat="server" Text="Calificación total"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCalificacionTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="Label69" runat="server" Text="Calificación en letras"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCalificacionLetras" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="color: Black" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Button ID="bntCalQualification" runat="server" Text="Calcular Calificación"
                                ValidationGroup="calificacion" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <%--Panel de funciones--%>
    <table style="width: 100%">
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
    </table>
</asp:Content>
