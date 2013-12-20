<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addStrategicActivity" Title="addStrategicActivity" Codebehind="addStrategicActivity.aspx.vb" %>

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
                <asp:Label ID="lblcode" runat="server" Text="Código"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtcode" runat="server" Width="400px" MaxLength="50" 
                    AutoPostBack="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtcode"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpcode" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblname" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtname" runat="server" Width="400px" MaxLength="255" 
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
                <asp:TextBox ID="txtdescription" runat="server" Width="400px" MaxLength="255" 
                    TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvdescription" runat="server" ControlToValidate="txtdescription"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpdescription" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblidstrategy" runat="server" Text="Estratégia"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidstrategy" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblHelpstrategy" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblbegindate" runat="server" Text="Fecha Inicio"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtbegindate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvbegindate" runat="server" ControlToValidate="txtbegindate"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <cc1:CalendarExtender ID="cesbegindate" runat="server" TargetControlID="txtbegindate"
                    Format="yyyy/MM/dd">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvbegindate" runat="server" ErrorMessage="yyyy/MM/dd"
                    ControlToValidate="txtbegindate" Operator="DataTypeCheck" SetFocusOnError="True"
                    Type="Date"></asp:CompareValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpbegindate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblenddate" runat="server" Text="Fecha Fin"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtenddate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvenddate" runat="server" ControlToValidate="txtenddate"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <cc1:CalendarExtender ID="cesenddate" runat="server" TargetControlID="txtenddate"
                    Format="yyyy/MM/dd">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvenddate" runat="server" ErrorMessage="yyyy/MM/dd"
                    ControlToValidate="txtenddate" Operator="DataTypeCheck" SetFocusOnError="True"
                    Type="Date"></asp:CompareValidator>
							
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
                <asp:Label ID="lblestimatedvalue" runat="server" Text="Valor estimado"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtestimatedvalue" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvestimatedvalue" runat="server" ControlToValidate="txtestimatedvalue"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpestimatedvalue" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblidresponsible" runat="server" Text="Responsable"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidresponsible" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblHelpidresponsible" runat="server" Text=""></asp:Label>
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
            </td>
            <td>
                <asp:Label ID="lblHelpenabled" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbliduser" runat="server" Text="Usuario creación"></asp:Label>
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
                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha Creación"></asp:Label>
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
                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" 
                    CausesValidation="False" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" 
                    CausesValidation="False" />
                <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                &nbsp;<asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
