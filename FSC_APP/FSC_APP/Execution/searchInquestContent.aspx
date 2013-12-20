<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchInquestContent" Title="searchInquestContent" Codebehind="searchInquestContent.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="idlike">Id</asp:ListItem>
                                <asp:ListItem Value="code">Código</asp:ListItem>
                                <asp:ListItem Value="questionText">Texto Pregunta</asp:ListItem>
                                <asp:ListItem Value="questionType">Tipo Pregunta</asp:ListItem>
                                <asp:ListItem Value="answer">Respuesta</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha de Creación</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <asp:ListItem Value="enabledtext">Estado</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Buscar" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblSort" runat="server" Text="Ordenar por"></asp:Label>
                            <asp:DropDownList ID="ddlSort" runat="server">
                                <asp:ListItem Value="id">Id</asp:ListItem>
                                <asp:ListItem Value="code">Código</asp:ListItem>
                                <asp:ListItem Value="questionText">Texto Pregunta</asp:ListItem>
                                <asp:ListItem Value="questionType">Tipo Pregunta</asp:ListItem>
                                <asp:ListItem Value="answer">Respuesta</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha de Creación</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upSubTitle" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upData" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            Width="100%" PageSize="10">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addInquestContent.aspx?op=edit&id={0}"
                                    HeaderText="Operaciones" Text="Editar" />
                                <asp:BoundField DataField="id" HeaderText="Id" />
                                <asp:BoundField DataField="code" HeaderText="Código" />
                                <asp:BoundField DataField="questionText" HeaderText="Texto Pregunta" />
                                <asp:BoundField DataField="questionType" HeaderText="Tipo Pregunta" />
                                <asp:BoundField DataField="answer" HeaderText="Respuesta" />
                                <asp:BoundField DataField="createdate" HeaderText="Fecha de Creación" />
                                <asp:BoundField DataField="username" HeaderText="Usuario" />
                                <asp:BoundField DataField="enabled" HeaderText="Estado" />
                                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addResolvedInquest.aspx?op=add&idInquestContent={0}"
                                    HeaderText="" Text="Responder Encuesta" />
                                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="#"
                                    HeaderText="" Visible="false" Text="Ver Tabulación" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
