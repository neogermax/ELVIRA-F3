<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MastePages/WorkFlowAdmin.Master"
    CodeBehind="AssignProcessCaseResponsibles.aspx.vb" Inherits="BPMWebSite.AssignProcessCaseResponsibles"
    Title="Asignar Responsables al Caso de Proceso" Theme="GattacaAdmin" %>

<%@ Register Assembly="Gattaca.WebControls.DoubleListBox" Namespace="Gattaca.WebControls.DoubleListBox"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cssLabelTitle">
        Asignar Responsables al Caso de Proceso</div>
    <br />
    <cc2:DoubleListBox ID="dblPackages" runat="server" Height="200px" Width="800px" />
    <br />
    <asp:Button ID="btnOk" runat="server" Text="Aceptar" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" />
</asp:Content>
