<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MastePages/WorkFlowAdmin.Master"
    CodeBehind="AssignResponsibles.aspx.vb" Inherits="BPMWebSite.AssignResponsibles"
    Title="Asignar Responsables a la Actividad" Theme="GattacaAdmin" %>

<%@ Register Assembly="Gattaca.WebControls.DoubleListBox" Namespace="Gattaca.WebControls.DoubleListBox"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cssLabelTitle">
        Asignar Responsables a la Actividad</div>
    <br />
    <cc2:DoubleListBox ID="dblResponsibles" runat="server" Height="200px" Width="800px" />
    <br />
    <asp:Button ID="btnOk" runat="server" Text="Aceptar" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" />
</asp:Content>
