<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.FormulationAndAdoption_projectForumPanel" Codebehind="projectForumPanel.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <asp:DataList ID="dlForum" runat="server" Width="100%">
        <ItemTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <table class="tableHeader" width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblSubject" Text="Asunto: " runat="server"></asp:Label>
                                    <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("subject") %>' />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="60%" align="center">
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblprojectName" Text="Proyecto" runat="server" /><strong>
                                </td>
                                <td>
                                    <asp:Label ID="IdProjectLabel" runat="server" Text='<%# Eval("PROJECTNAME") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblmessage" Text="Mensaje" runat="server" /></strong>
                                </td>
                                <td>
                                    <asp:Label ID="MessageLabel" runat="server" Text='<%# Eval("message") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblattachment" Text="Adjunto" runat="server" /><strong>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlattatchment" runat="server" Text='<%# Eval("Attachment") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblreplycount" Text="No. de respuestas" runat="server" /></strong>
                                </td>
                                <td>
                                    <asp:Label ID="ReplyCountLabel" runat="server" Text='<%# Eval("REPLYCOUNT") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblupdatedate" Text="Fecha de ultimo mensaje" runat="server" /></strong>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("LASTREPLYCREATEDATE") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblcreatedate" Text="Fecha de creación" runat="server" /></strong>
                                </td>
                                <td>
                                    <asp:Label ID="CreateDateLabel" runat="server" Text='<%# Eval("CreateDate") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblusername" Text="Autor" runat="server" /></strong>
                                </td>
                                <td>
                                    <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("USERNAME") %>' />
                                    <asp:Label ID="lblIdUser" runat="server" Text='<%# Eval("IdUser") %>' Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblenabled" Text="Estado" runat="server" /></strong>
                                </td>
                                <td>
                                    <asp:Label ID="EnabledLabel" runat="server" Text='<%# Eval("Enabled") %>' />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table align="right">
                            <tr>
                                <td style="text-align: right">
                                    <asp:Button ID="btnEditForum" runat="server" CommandArgument='<%# Eval("Id") %>'
                                        CommandName="editForum" Text="Editar Foro" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="addReply"
                                        Text="Agregar respuesta" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
    <asp:UpdatePanel ID="upReply" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:DataList ID="dlReply" runat="server" Width="100%" ShowHeader="False">
                <ItemTemplate>
                    <br />
                    <br />
                    <table width="70%" style="border-style: ridge; border-width: 3px">
                        <tr>
                            <td>
                                <table width="100%" class="tableHeader1">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblreplyusername" Text="Autor: " runat="server"></asp:Label>
                                            <asp:Label ID="replyusernamelabel" runat="server" Text='<%# Eval("USERNAME") %>' />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="60%" align="center">
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="lblreplysubject" Text="Asunto" runat="server" /><strong>
                                        </td>
                                        <td>
                                            <asp:Label ID="replysubjectlabel" runat="server" Text='<%# Eval("subject") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="lblreplyattachment" Text="Adjunto" runat="server" /><strong>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hlreplyattatchment" runat="server" Text='<%# Eval("Attachment") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="lblreplyupdatedate" Text="Fecha de actualización" runat="server" /></strong>
                                        </td>
                                        <td>
                                            <asp:Label ID="replyupdatelabel" runat="server" Text='<%# Eval("updatedate") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="lblreplycreatedate" Text="Fecha de creación" runat="server" /></strong>
                                        </td>
                                        <td>
                                            <asp:Label ID="createdatelabel" runat="server" Text='<%# Eval("createdate") %>' />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table align="right">
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Button ID="btnEditReply" runat="server" CommandArgument='<%# "?op=edit&id=" & Eval("Id") & "&idf=" & Eval("IdForum") %>'
                                                CommandName='<%# Eval("IdUser") %>' Text="Editar Respuesta" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                    <table>
                        <tr>
                            <td>                                
                                <asp:LinkButton ID="previous" runat="server" Text="<" CommandName="previous" />&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="next" runat="server" Text=">" CommandName="next" />&nbsp;&nbsp;&nbsp;                                
                            </td>
                        </tr>
                    </table>
                </FooterTemplate>
            </asp:DataList>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
