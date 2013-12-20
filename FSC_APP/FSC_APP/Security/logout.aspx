<%@ Page Language="VB" MasterPageFile="~/Master/mplogOut.master" AutoEventWireup="false" Inherits="FSC_APP.Security_logout" Title="Untitled Page" Codebehind="logout.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphLogin" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td valign="top" align="right">
                      <div class="cssTitleLogout">
                            Sesion Finalizada.
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="bottom">
                        
                    </td>
                </tr>
                <tr>
                    <td class="cssBtnLogout" valign="top" >
                        <asp:Button ID="btnLogOut" runat="server" CssClass="cssButton" Text="" />
                    </td>
                </tr>
                <tr>
                    <td class="cssSpaceLogout">&nbsp;
                        
                    </td>
                    <table border="0" cellpadding="0" cellspacing="0" height="0" width="100%">
                        <tr>
                            <td class="cssTxtFooterLogin">
                                
                            </td>
                        </tr>
                    </table>
                </tr>
                <tr>
                    <td align="center" valign="bottom">&nbsp;
                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
