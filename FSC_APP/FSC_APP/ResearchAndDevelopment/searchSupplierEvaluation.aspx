<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchSupplierEvaluation"
    Title="searchSupplierEvaluation" Codebehind="searchSupplierEvaluation.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="suppliername">Nombre proveedor</asp:ListItem>
                                <asp:ListItem Value="contractnumber">Número del contrato</asp:ListItem>
                                <asp:ListItem Value="contractstartdate">Fecha inicial contrato</asp:ListItem>
                                <asp:ListItem Value="contractenddate">Fecha final contrato</asp:ListItem>
                                <asp:ListItem Value="contractsubject">Objeto del contrato</asp:ListItem>
                                <asp:ListItem Value="contractvalue">Valor del contrato</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
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
                                <asp:ListItem Value="suppliername">Nombre proveedor</asp:ListItem>
                                <asp:ListItem Value="contractnumber">Número del contrato</asp:ListItem>
                                <asp:ListItem Value="contractstartdate">Fecha inicial contrato</asp:ListItem>
                                <asp:ListItem Value="contractenddate">Fecha final contrato</asp:ListItem>
                                <asp:ListItem Value="contractsubject">Objeto del contrato</asp:ListItem>
                                <asp:ListItem Value="contractvalue">Valor del contrato</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
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
                            Width="100%">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addSupplierEvaluation.aspx?op=edit&id={0}"
                                    HeaderText="Operaciones" Text="Editar" />
                                <asp:BoundField DataField="SUPPLIERNAME" HeaderText="Nombre proveedor" />
                                <asp:BoundField DataField="contractnumber" HeaderText="Número del contrato" />
                                <asp:BoundField DataField="contractstartdate" HeaderText="Fecha inicial contrato"
                                    DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="contractenddate" HeaderText="Fecha final contrato" DataFormatString="{0:dd/MM/yyyy}"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="contractsubject" HeaderText="Objeto del contrato" />
                                <asp:BoundField DataField="contractvalue" HeaderText="Valor del contrato" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:c} " />
                                <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
