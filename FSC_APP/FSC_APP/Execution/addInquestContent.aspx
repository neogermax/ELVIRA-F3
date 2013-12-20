<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addInquestContent" Title="addInquestContent" Codebehind="addInquestContent.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        ActiveTabIndex="2">
        <cc1:TabPanel runat="server" HeaderText="Información Principal" ID="TabPanel1" 
            Width="600px">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 122px">
                            <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                        </td>
                        <td style="width: 434px">
                            <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 122px">
                            <asp:Label ID="lblidinquest" runat="server" Text="Encuesta"></asp:Label>
                        </td>
                        <td style="width: 434px">
                            <asp:DropDownList ID="ddlInquest" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvidinquest" runat="server" ControlToValidate="ddlInquest"
                                ErrorMessage="Debe seleccionar una encuesta" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpidinquest" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 122px">
                            <asp:Label ID="lblcode" runat="server" Text="Código"></asp:Label>
                        </td>
                        <td style="width: 434px">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtcode" runat="server" Width="400px" MaxLength="50" AutoPostBack="True"
                                        CausesValidation="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtcode"
                                        ErrorMessage="Debe digitar un código">*</asp:RequiredFieldValidator>
                                    &nbsp;<asp:CustomValidator ID="cvCode" runat="server" ControlToValidate="txtcode"
                                        Display="Dynamic" ErrorMessage="El código digitado ya existe, por favor cambielo"
                                        SetFocusOnError="True">Este código ya existe, por favor cambielo</asp:CustomValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 122px">
                            <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
                        </td>
                        <td style="width: 434px">
                            <asp:DropDownList ID="ddlenabled" runat="server">
                                <asp:ListItem Text="Habilitado" Value="True"></asp:ListItem>
                                <asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpenabled" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 122px">
                            <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                        </td>
                        <td style="width: 434px">
                            <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 122px">
                            <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de Creación"></asp:Label>
                        </td>
                        <td style="width: 434px">
                            <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" TabIndex="1" runat="server" HeaderText="Preguntas">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="Label1" runat="server" Text="Texto Pregunta"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQuestionText" runat="server" MaxLength="255" TextMode="MultiLine"
                                Width="600px" onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuestionText"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="questions"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="Label3" runat="server" Text="Tipo"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlQuestionType" runat="server">
                                <asp:ListItem>Respuesta Texto</asp:ListItem>
                                <asp:ListItem>Lista de Selección</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Button ID="btnAddQuestion" runat="server" Text="Agregar Pregunta" ValidationGroup="questions" />
                        </td>
                        <td>
                            &#160;&#160;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                                    <asp:TemplateField HeaderText="Texto Pregunta">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container, "DataItem.questiontext")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container, "DataItem.questiontype")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Respuestas" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkAnswers" runat="server" CausesValidation="false" CommandName="respuestas"
                                                OnClick="lnkAnswers_Click" Text="Respuestas"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:ButtonField CommandName="tabulacion" HeaderText="Tabulación" Visible="false" Text="Ver Tabulación">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" TabIndex="1" runat="server" HeaderText="Respuestas">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Table ID="tableAnswers" runat="server" Width="100%" Enabled="false">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 172px">
                                                <asp:Label ID="lblQuestion" runat="server" Text="Pregunta"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSelectedQuestion" runat="server" MaxLength="255" TextMode="MultiLine"
                                                    Width="600px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 172px">
                                                <asp:Label ID="Label2" runat="server" Text="Texto Respuesta"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAnswerText" runat="server" MaxLength="255" TextMode="MultiLine"
                                                    Width="600px" onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAnswerText"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="answers"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 172px">
                                                <asp:Button ID="btnAddAnswer" runat="server" Text="Agregar Respuesta" ValidationGroup="answers" />
                                            </td>
                                            <td>
                                                &#160;&#160;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gvAnswers" runat="server" AutoGenerateColumns="False" Width="100%">
                                                    <Columns>
                                                        <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                                                        <asp:TemplateField HeaderText="Texto Respuesta">
                                                            <ItemTemplate>
                                                                <%#DataBinder.Eval(Container, "DataItem.answer")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <%#DataBinder.Eval(Container, "DataItem.questiontype")%>
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
    <table style="width: 100%">
        <tr>
            <td>
                <asp:ValidationSummary ID="ValSum" runat="server" HeaderText="Los siguientes errores fueron encontrados:" />
            </td>
        </tr>
    </table>
</asp:Content>
