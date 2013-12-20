<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.FomsProceedings_Proceeding_tracing" Codebehind="Proceeding_tracing.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script src="../Include/javascript/proceeding_tracing.js" type="text/javascript"></script>

    <script type="text/javascript">
        function format(input) {
            var num = input.value.replace(/\./g, "");
            if (!isNaN(num)) {
                num = num.toString().split("").reverse().join("").replace(/(?=\d*\.?)(\d{3})/g, "$1.");
                num = num.split("").reverse().join("").replace(/^[\.]/, "");
                input.value = num;
            }

            else {
                input.value = input.value.replace(/[^\d\.]*/g, "");
            }
        }        
    </script>

    <div id="containerSuccess" runat="server" visible="true" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="Lblmsginfo" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <p>
    </p>
    <p>
    </p>
    <div id="dvMain">
        <table cellpadding="1" cellspacing="1" id="Acta_Seguimiento" style="width: 80%;"
            align="center">
            <tr>
                <td style="border: solid 1px; border-color: #000000; width: 33%;" colspan="2" rowspan="1">
                    <img id="ctl00_headerLeft" src="../App_Themes/GattacaAdmin/Images/Template/header_leftBPO.jpg"
                        style="border-width: 0px;">
                </td>
                <td colspan="4" style="text-align: center; border: 1px solid #000000; font-size: large;"
                    bgcolor="#F2F2F2">
                    <b>ACTA DE COMITÉ</b>
                </td>
            </tr>
            <tr>
                <td colspan="6" rowspan="1" style="text-align: center;">
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>Acta No.:</b>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtProceedNumber" runat="server" Text="" Enabled="False"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>Fecha del comité:</b>
                </td>
                <td style="border: solid 1px; border-color: #000000;" colspan="3">
                    <asp:TextBox ID="txtComitDate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    <cc1:CalendarExtender ID="cdcomitdate" runat="server" TargetControlID="txtComitDate"
                        Format="dd/MM/yyyy" Enabled="True">
                    </cc1:CalendarExtender>
                    <asp:CompareValidator ID="cvcomitdate" runat="server" ErrorMessage="aaaa/mm/dd" Type="Date"
                        ControlToValidate="txtComitDate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtComitDate"
                        ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="6" rowspan="1" style="text-align: center;">
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" colspan="2" bgcolor="#F2F2F2">
                    <b>Nombre del</b>
                    <asp:DropDownList ID="ddlTypeThird" runat="server">
                        <asp:ListItem Value="Socio">Socio</asp:ListItem>
                        <asp:ListItem Value="Operador">Operador</asp:ListItem>
                        <asp:ListItem Value="Socio Operador">Socio Operador</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2" colspan="3">
                    <b>Numero del</b>
                    <asp:DropDownList ID="ddlTypeoF" runat="server">
                        <asp:ListItem Value="Contrato">Contrato</asp:ListItem>
                        <asp:ListItem Value="Convenio">Convenio</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>
                        <asp:Label ID="lblDates" runat="server" Text="Fechas del contrato:"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" colspan="2" rowspan="2">
                    <asp:TextBox ID="txtPartnerName" runat="server" Width="95%" Text=""></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;" rowspan="2" colspan="3">
                    <asp:TextBox ID="txtContractNumber" runat="server" Text=""></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                    <b>Desde: </b>
                    <asp:TextBox ID="txtStartingDate" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="cestartingdate" runat="server" TargetControlID="txtStartingDate"
                        Format="dd/MM/yyyy" Enabled="True">
                    </cc1:CalendarExtender>
                    <asp:CompareValidator ID="cvstartingdate" runat="server" ErrorMessage="aaaa/mm/dd"
                        Type="Date" ControlToValidate="txtStartingDate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;">
                    <b>Hasta: </b>
                    <asp:TextBox ID="txtEndingDate" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="ceendingdate" runat="server" TargetControlID="txtendingdate"
                        Format="dd/MM/yyyy" Enabled="True">
                    </cc1:CalendarExtender>
                    <asp:CompareValidator ID="cvendingdate" runat="server" ErrorMessage="aaaa/mm/dd"
                        Type="Date" ControlToValidate="txtendingdate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>
                        <asp:Label ID="lblObject" runat="server" Text="Objeto del contrato:"></asp:Label></b>
                </td>
                <td style="border: solid 1px; border-color: #000000;" colspan="5">
                    <asp:TextBox ID="txtContractObjective" runat="server" Width="95%" Text="" TextMode="MultiLine"
                        Height="55px"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                    <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                        <b>
                            <asp:Label ID="lblAdittions" runat="server" Text="Adiciones al contrato:"></asp:Label></b>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;">
                        <input id="rbAdittions1" name="Aditions" type="radio" runat="server" value="Si" style="font-weight: bold" /><b>Si
                        </b>
                        <input id="rbAdittions2" name="Aditions" type="radio" runat="server" value="No" style="font-weight: bold" /><b>No
                        </b>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                        <b>N&#65533;mero de adiciones:</b>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;" width="11%">
                        <asp:TextBox ID="txtNumberOfAditions" runat="server" Text=""></asp:TextBox>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                        <b>Valor en letras:</b>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;">
                        <asp:TextBox ID="txtNumberOfAditionsLetters" runat="server" Width="95%" Text=""></asp:TextBox>
                    </td>
                </tr>--%>
            <%--<tr>
                    <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                        <b>Fecha de la adici&#65533;n:</b>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;">
                        <asp:TextBox ID="txtAdditionDate" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
                        <cc1:CalendarExtender ID="ceadditiondate" runat="server" TargetControlID="txtAdditionDate"
                            Format="dd/MM/yyyy" Enabled="True">
                        </cc1:CalendarExtender>
                        <asp:CompareValidator ID="cvadditiondate" runat="server" ErrorMessage="dd/mm/aaaa"
                            Type="Date" ControlToValidate="txtAdditionDate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2" colspan="3">
                        <b>Valor $:</b>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;">
                        <asp:TextBox ID="txtValue" runat="server" onkeyup="format(this)" onchange="format(this)"
                            Width="200px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>--%>
            <tr>
                <td colspan="6" rowspan="1" style="text-align: center;">
                    <p>
                    </p>
                </td>
            </tr>
            <%--<tr>
                    <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                        <b>
                            <asp:Label ID="Label1" runat="server" Text="Valor final del contrato en $:"></asp:Label></b>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;">
                        <asp:TextBox ID="txtFinalValue" runat="server" onkeyup="format(this)" onchange="format(this)"
                            Width="150px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                        <b>Valor en letras:</b>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;" colspan="3">
                        <asp:TextBox ID="txtValueInLetters" runat="server" Width="95%" MaxLength="50" Style="text-align: left"></asp:TextBox>
                    </td>
                </tr>--%>
            <%--<tr>
                    <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                        <b>
                            <asp:Label ID="Label1" runat="server" Text="Valor final del contrato en $:"></asp:Label></b>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;">
                        <asp:TextBox ID="txtFinalValue" runat="server" onkeyup="format(this)" onchange="format(this)"
                            Width="150px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                        <b>Valor en letras:</b>
                    </td>
                    <td style="border: solid 1px; border-color: #000000;" colspan="3">
                        <asp:TextBox ID="txtValueInLetters" runat="server" Width="95%" MaxLength="50" Style="text-align: left"></asp:TextBox>
                    </td>
                </tr>--%>
            <tr>
                <td colspan="6" rowspan="1" style="text-align: center;">
                    <p>
                    </p>
                </td>
            </tr>
            <tr bgcolor="#F2F2F2">
                <td colspan="2" rowspan="1" style="text-align: center; border: solid 1px; border-color: #000000;">
                    <b>Asistentes </b>
                </td>
                <td colspan="4" rowspan="1" style="text-align: center; border: solid 1px; border-color: #000000;">
                    <b>Firmas </b>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" colspan="2" rowspan="1">
                    <asp:TextBox ID="txtAssistant1" runat="server" Width="95%" MaxLength="50"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;" colspan="4" rowspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" colspan="2" rowspan="1">
                    <asp:TextBox ID="txtAssistant2" runat="server" Width="95%" MaxLength="50"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;" colspan="4" rowspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" colspan="2" rowspan="1">
                    <asp:TextBox ID="txtAssistant3" runat="server" Width="95%" MaxLength="50"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;" colspan="4" rowspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" colspan="2" rowspan="1">
                    <asp:TextBox ID="txtAssistant4" runat="server" Width="95%" MaxLength="50"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;" colspan="4" rowspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" colspan="2" rowspan="1">
                    <asp:TextBox ID="txtAssistant5" runat="server" Width="95%" MaxLength="50"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;" colspan="4" rowspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" colspan="2" rowspan="1">
                    <asp:TextBox ID="txtAssistant6" runat="server" Width="95%" MaxLength="50"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;" colspan="4" rowspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" colspan="2" rowspan="1">
                    <asp:TextBox ID="txtAssistant7" runat="server" Width="95%" MaxLength="50"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;" colspan="4" rowspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6" rowspan="1" style="text-align: center;">
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" colspan="6" bgcolor="#F2F2F2">
                    <b>Orden del día</b>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" colspan="6">
                    <textarea id="taDayOrder" name="taDayOrder" runat="server" style="width: 99%; height: 100px;"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="6" rowspan="1" style="text-align: center;">
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="text-align: center; border: solid 1px; border-color: #000000;"
                    bgcolor="#F2F2F2">
                    <b>Compromisos </b>
                </td>
                <td style="border: solid 1px; border-color: #000000; text-align: center;" bgcolor="#F2F2F2"
                    colspan="2">
                    <b>Responsable </b>
                </td>
                <td style="border: solid 1px; border-color: #000000; text-align: center;" bgcolor="#F2F2F2">
                    <b>Fecha de Seguimiento</b>
                </td>
                <td style="border: solid 1px; border-color: #000000; text-align: center;" bgcolor="#F2F2F2">
                    <b>Correo Electrónico</b>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" colspan="2" rowspan="1">
                    <asp:TextBox ID="txtLiabilities" runat="server" Width="100%" TextMode="MultiLine"
                        Height="43px"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;" colspan="2">
                    <asp:TextBox ID="txtreponsible" runat="server" Width="100%"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtTracingDate" runat="server" Width="100%"></asp:TextBox>
                    <cc1:CalendarExtender ID="cetracingdate" runat="server" TargetControlID="txtTracingDate"
                        Format="dd/MM/yyyy" Enabled="True">
                    </cc1:CalendarExtender>
                </td>
                <td style="border: solid 1px; border-color: #000000;" colspan="2">
                    <asp:TextBox ID="txtemail" runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" rowspan="1" style="text-align: center;">
                    <p>
                        <asp:CompareValidator ID="cvtracingdate" runat="server" ErrorMessage="dd/mm/aaaa"
                            Type="Date" ControlToValidate="txtTracingDate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="6" rowspan="1" style="text-align: left;">
                    <p>
                        <asp:Button ID="Btnagregarcompromisos" runat="server" Text="Agregar compromiso" ValidationGroup="compromise" />
                        <asp:HiddenField ID="HFacta" runat="server" />
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="6" rowspan="1" style="text-align: center;">
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="6" rowspan="1" style="text-align: center;">
                    <div id="containerSuccess3" runat="server" visible="true" style="width: 100%; text-align: center;
                        border: 2px solid #cecece; background: #E8E8DC; height: 50px; line-height: 40px;
                        vertical-align: middle;">
                        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
                        <asp:Label ID="Lblinformationvalide" runat="server" Style="font-size: 14pt; color: #FF0040;"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="text-align: center;">
                    <asp:GridView ID="gvcompromisos" runat="server" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblidcompromiso" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No. del Proyecto">
                                <ItemTemplate>
                                    <asp:Label ID="lblidproject" runat="server" Text='<%# Eval("idproject") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Compromiso">
                                <ItemTemplate>
                                    <asp:Label ID="lblliabilities" runat="server" Text='<%# Eval("liabilities") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Responsable">
                                <ItemTemplate>
                                    <asp:Label ID="lblresponsible" runat="server" Text='<%# Eval("responsible") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha de Seguimiento">
                                <ItemTemplate>
                                    <asp:Label ID="lbltracingdate" runat="server" Text='<%# Eval("tracingdate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Correo Electrónico">
                                <ItemTemplate>
                                    <asp:Label ID="lblemail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <p>
                </p>
            </td>
            <tr>
                <td colspan="6" rowspan="1" style="text-align: center;">
                    <asp:Button ID="Btnexport" runat="server" Text="Exportar Acta de Seguimiento" ValidationGroup="compromise" />
                    
                </td>
            </tr>
        </table>
        <p>
        </p>
        <div id="containerSuccess_2" runat="server" visible="true" style="width: 100%; text-align: center;
            border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
            vertical-align: middle;">
            <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
            <asp:Label ID="Lblmsginfo_2" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
        </div>
    </div>
</asp:Content>
