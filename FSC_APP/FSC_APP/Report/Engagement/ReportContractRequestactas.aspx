<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_Engagement_ReportContractRequestactas"
    Title="reporte por contratacion" Codebehind="ReportContractRequestactas.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script src="../../Include/javascript/jquery-1.6.1.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Include/javascript/reportcontractactas.js"></script>

    <script src="../../Include/javascript/chosen.jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
    </script>

    <h1>
        <span style="font-family: arial,helvetica,sans-serif;">REPORTE ACTAS</span></h1>
    <p>
        &nbsp;</p>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
        <tbody>
            <tr>
                <td style="width: 15%; text-align: center">
                    <strong><span style="font-family: arial,helvetica,sans-serif;">Proyecto</span></strong>
                </td>
                <td style="text-align: left; width: 1108px;">
                    <asp:DropDownList ID="ddlproyect" runat="server" CssClass="Ccombo">
                    </asp:DropDownList>
                </td>
                <td style="width: 30%;">
                    <asp:Button ID="btnviewsactas" runat="server" Text="Consultar Actas" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%;" colspan="3">
                    <p>
                        &nbsp;</p>
                </td>
            </tr>
            <tr>
                <td style="width: 10%;" colspan="3">
                    <p>
                        &nbsp;</p>
                </td>
            </tr>
            <tr>
                <td style="width: 15%;">
                </td>
                <td style="width: 1108px">
                    <asp:GridView ID="gvactas" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href='<%# Eval("ruta") %>'>Descargar</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo de Acta">
                                <ItemTemplate>
                                    <asp:Label ID="lbltipacta" runat="server" Text='<%# Eval("Actaname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Consecutivo Acta">
                                <ItemTemplate>
                                    <asp:Label ID="lblconact" runat="server" Text='<%# Eval("id_acta") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre del Acta">
                                <ItemTemplate>
                                    <asp:Label ID="lblnameacta" runat="server" Text='<%# Eval("FileName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha de creación">
                                <ItemTemplate>
                                    <asp:Label ID="lblfecha" runat="server" Text='<%# Eval("Create_Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Creado por el Usuario">
                                <ItemTemplate>
                                    <asp:Label ID="lbluser" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:HiddenField ID="HDreportact" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <div id="containerSuccess" runat="server" visible="false" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblsaveinformation" runat="server" Style="font-size: 14pt; color: #FF0040;"></asp:Label>
    </div>
    <br />
</asp:Content>
