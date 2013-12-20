<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.FomsProceedings_Proceeding_Close"
    Title="Página sin título" Codebehind="Proceeding_Close.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script src="../Include/javascript/proceeding_close.js" type="text/javascript"></script>

    <script type="text/javascript" src="../Pretty/js/jquery.prettyPhoto.js"></script>

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
    <table runat="server" border="0" cellpadding="1" cellspacing="1" id="Acta_cierre"
        style="width: 80%;" align="center">
        <tbody>
            <tr>
                <td align="right" colspan="6">
                    <a style="font-size: 14pt;" id="linkcompromise" runat="server" href="~/Report/proceedings/ReportCompromise.aspx?"
                        title="COMPROMISOS" target="_blank">Visualizar Compromisos del Proyecto</a>
                </td>
            </tr>
              <tr>
                <td colspan="6">
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="border: solid 1px; border-color: #000000;">
                    <img id="ctl00_headerLeft" src="../App_Themes/GattacaAdmin/Images/Template/header_leftBPO.jpg"
                        style="border-width: 0px;">
                </td>
                <td colspan="4" style="text-align: center; border: 1px solid #000000; font-size: large;"
                    bgcolor="#F2F2F2">
                    <b>ACTA DE CIERRE</b>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>Nombre del</b>
                    <asp:DropDownList ID="ddlTypeThird" runat="server">
                        <asp:ListItem Value="Socio">Socio</asp:ListItem>
                        <asp:ListItem Value="Operador">Operador</asp:ListItem>
                        <asp:ListItem Value="Socio Operador">Socio Operador</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>Numero del</b>
                    <asp:DropDownList ID="ddlTypeoF" runat="server">
                        <asp:ListItem Value="Contrato">Contrato</asp:ListItem>
                        <asp:ListItem Value="Convenio">Convenio</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>
                        <asp:Label ID="lblDates" runat="server" Text="Fechas del contrato:"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td colspan="2" rowspan="2" style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtThirdName" runat="server" Width="95%"></asp:TextBox>
                </td>
                <td colspan="2" rowspan="2" style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtContractNumber" runat="server"></asp:TextBox>
                </td>
                <td colspan="2" style="border: solid 1px; border-color: #000000;">
                    <b>Desde: </b>
                    <asp:TextBox ID="txtInitialDate" runat="server" Width="130px" MaxLength="50"></asp:TextBox>
                    <cc1:CalendarExtender ID="ceinitialdate" runat="server" TargetControlID="txtInitialDate"
                        Format="dd/MM/yyyy" Enabled="True">
                    </cc1:CalendarExtender>
                    <asp:CompareValidator ID="cvinitialdate" runat="server" ErrorMessage="aaaa/mm/dd"
                        Type="Date" ControlToValidate="txtInitialDate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border: solid 1px; border-color: #000000;">
                    <b>Hasta: </b>
                    <asp:TextBox ID="txtfinalDate" runat="server" Width="130px" MaxLength="50"></asp:TextBox>
                    <cc1:CalendarExtender ID="cefinaldate" runat="server" TargetControlID="txtfinalDate"
                        Format="dd/MM/yyyy" Enabled="True">
                    </cc1:CalendarExtender>
                    <asp:CompareValidator ID="cvfinaldate" runat="server" ErrorMessage="aaaa/mm/dd" Type="Date"
                        ControlToValidate="txtfinalDate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <asp:Label ID="lblObjectContract" runat="server" Text="Objeto del contrato:" Style="font-weight: 700"></asp:Label>
                </td>
                <td colspan="5" rowspan="1" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <textarea id="taContractObject" name="taContractObject" runat="server" style="width: 95%"></textarea>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <asp:Label ID="lblContractInitialValue" runat="server" Text="Valor Inicial del contrato:"
                        Style="font-weight: 700"></asp:Label>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtInitialValue" runat="server" onkeyup="format(this)"></asp:TextBox>
                </td>
                <td colspan="2" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <asp:Label ID="lblInLetters" runat="server" Text="En letras:" Style="font-weight: 700"></asp:Label>
                </td>
                <td colspan="2" style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtLetters" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <asp:Label ID="lblContractAditions" runat="server" Text="Adiciones al contrato:"
                        Style="font-weight: 700"></asp:Label>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                    <input id="rbAdition1" name="rbAdition" type="radio" runat="server" style="font-weight: bold" /><b>Si
                    </b>
                    <input id="rbAdition2" name="rbAdition" type="radio" runat="server" style="font-weight: bold" /><b>No
                    </b>
                </td>
                <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <asp:Label ID="lblNumberOfAditions" runat="server" Text="Número de adiciones:" Style="font-weight: 700"></asp:Label>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtNumberAdition" runat="server"></asp:TextBox>
                </td>
                <td style="border: 1px solid #000000;" bgcolor="#F2F2F2">
                    <asp:Label ID="lblAditionsInLetters" runat="server" Text="En letras:" Style="font-weight: 700"></asp:Label>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtInLetters" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>Fecha de la adición:</b>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtAdditionDate" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
                    <%--<cc1:CalendarExtender ID="ceadditiondate" runat="server" TargetControlID="txtAdditionDate"
                        Format="dd/MM/yyyy" Enabled="True">
                    </cc1:CalendarExtender>
                    <asp:CompareValidator ID="cvadditiondate" runat="server" ErrorMessage="aaaa/mm/dd"
                        Type="Date" ControlToValidate="txtAdditionDate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>--%>
                </td>
                <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>Valor $</b>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtAditionValue" runat="server"></asp:TextBox>
                </td>
                <td style="border: 1px solid #000000; width: 85px;" bgcolor="#F2F2F2">
                    <b>Ampliación de la vigencia en: </b>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtVigencyExtend" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <asp:Label ID="lblFinalValue" runat="server" Text="Valor Final del contrato:" Style="font-weight: 700"></asp:Label>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtContractFinalValue" runat="server" onkeyup="format(this)"></asp:TextBox>
                </td>
                <td colspan="2" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>En letras:</b>
                </td>
                <td colspan="2" style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtContractFinalValueInLetters" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>Cumplimiento de responsabilidades contractuales </b>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border: solid 1px; border-color: #000000;">
                    <textarea id="taFullfillment" name="taFullfillment" runat="server" style="width: 95%"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>Observaciones:</b><p>
                    </p>
                    <textarea id="taObservations" name="taObservations" runat="server" style="width: 95%"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>
                        <asp:Label ID="lblWeakness" runat="server" Text="Debilidades del proyecto:"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border: solid 1px; border-color: #000000;">
                    <textarea id="taWeakness" name="taWeakness" runat="server" style="width: 95%"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>
                        <asp:Label ID="lblOportunity" runat="server" Text="Oportunidades del proyecto:"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border: solid 1px; border-color: #000000;">
                    <textarea id="taOportunities" name="taOportunities" runat="server" style="width: 95%"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>
                        <asp:Label ID="lblStrong" runat="server" Text="Fortalezas del proyecto:"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border: solid 1px; border-color: #000000;">
                    <textarea id="taStrongest" name="taStrongest" runat="server" style="width: 95%"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>
                        <asp:Label ID="lblLearning" runat="server" Text="Aprendizajes del proyecto:"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border: solid 1px; border-color: #000000;">
                    <textarea id="taLearnings" name="taLearnings" runat="server" style="width: 95%"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                    <b>Fecha de cierre: </b>
                </td>
                <td colspan="4" style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtFinishDate" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                    <cc1:CalendarExtender ID="cefinishdate" runat="server" TargetControlID="txtFinishDate"
                        Format="dd/MM/yyyy" Enabled="True">
                    </cc1:CalendarExtender>
                    <asp:CompareValidator ID="cvfinishdate" runat="server" ErrorMessage="aaaa/mm/dd"
                        Type="Date" ControlToValidate="txtFinishDate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border: solid 1px; border-color: #000000; text-align: center;"
                    bgcolor="#F2F2F2">
                    <b>Asistentes </b>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="border: solid 1px; border-color: #000000; text-align: center;"
                    bgcolor="#F2F2F2">
                    <b>
                        <asp:Label ID="lblOperator" runat="server" Text="Socio"></asp:Label>
                    </b>
                </td>
                <td colspan="3" style="border: solid 1px; border-color: #000000; text-align: center;"
                    bgcolor="#F2F2F2">
                    <b>Fundación Saldarriaga Concha </b>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border: solid 1px; border-color: #000000; text-align: center;"
                    bgcolor="#F2F2F2">
                    <b>Nombre </b>
                </td>
                <td style="border: solid 1px; border-color: #000000; text-align: center;" bgcolor="#F2F2F2">
                    <b>Firma </b>
                </td>
                <td colspan="2" style="border: solid 1px; border-color: #000000; text-align: center;"
                    bgcolor="#F2F2F2">
                    <b>Nombre </b>
                </td>
                <td style="border: solid 1px; border-color: #000000; text-align: center;" bgcolor="#F2F2F2">
                    <b>Firma</b>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtPartnerName1" runat="server" Width="95%"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                </td>
                <td colspan="2" style="border: 1px solid #000000;">
                    <asp:TextBox ID="txtFSCName1" runat="server" Width="95%"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtPartnerName2" runat="server" Width="95%"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                </td>
                <td colspan="2" style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtFSCName2" runat="server" Width="95%"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtPartnerName3" runat="server" Width="95%"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                </td>
                <td colspan="2" style="border: solid 1px; border-color: #000000;">
                    <asp:TextBox ID="txtFSCName3" runat="server" Width="95%"></asp:TextBox>
                </td>
                <td style="border: solid 1px; border-color: #000000;">
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnExport" runat="server" Text="Exportar Acta de Cierre" />
                    <asp:Label ID="lblInformation" runat="server"></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    <p>
    </p>
    <p>
    </p>
    <div id="containerSuccess_2" runat="server" visible="true" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="Lblmsginfo_2" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <table id="tablevalide" runat="server" width="100%">
        <tr>
            <td>
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <div id="Div1" runat="server" visible="true" style="width: 100%; text-align: center;
                    border: 2px solid #cecece; background: #E8E8DC; height: 100px; line-height: 40px;
                    vertical-align: middle;">
                    <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
                    <asp:Label ID="Lblinformationvalide" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <asp:Button ID="Btnexportvalidate" runat="server" Text="Exportar acta de cierre" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HFProceedClose" runat="server" />
</asp:Content>
