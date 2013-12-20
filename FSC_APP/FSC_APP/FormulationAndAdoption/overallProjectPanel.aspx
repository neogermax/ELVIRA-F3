<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.overallProjectPanel" Title="Panel General de proyectos" Codebehind="overallProjectPanel.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="upSubTitle" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upData" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            Width="100%" PageSize="10" OnRowCommand="updatePhase">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="Id" Visible="false"/>
                                <asp:BoundField DataField="STRATEGICOBJECTIVENAME" HeaderText="Obj. estratégico" />
                                <asp:BoundField DataField="StrategicLineNAME" HeaderText="LineaEstrategica/Estrategia" />
                                <asp:BoundField DataField="ideaname" HeaderText="Idea" />                               
                                <asp:BoundField DataField="name" HeaderText="Proyecto" />
                                <asp:BoundField DataField="PHASENAME" HeaderText="Fase" />
                                <asp:BoundField DataField="effectivebudget" HeaderText="Vigencia presupuestal" />
                                <asp:TemplateField HeaderText="Operaciones">                                
                                    <ItemTemplate>
                                        <asp:LinkButton  ID="btnUpdatePhase1" Text="Cambiar fase" runat="server" CommandArgument='<%# Eval("idkey") %>' CommandName='<%# Eval("idphase") %>' CausesValidation="False" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:HyperLinkField DataNavigateUrlFields="name" DataNavigateUrlFormatString="searchForum.aspx?prjn={0}" HeaderText="Operaciones" Text="Buscar foros" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>    
</asp:Content>
