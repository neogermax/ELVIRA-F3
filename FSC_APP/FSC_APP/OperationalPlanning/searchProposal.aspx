<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchProposal" Title="searchProposal" Codebehind="searchProposal.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="idLike">Id</asp:ListItem>
                                <asp:ListItem Value="summoningName">Convocatoria</asp:ListItem>
                                <asp:ListItem Value="nameOperator">Operador</asp:ListItem>
                                <asp:ListItem Value="operatornit">Nit Operador</asp:ListItem>
                                <asp:ListItem Value="projectname">Nombre Proyecto</asp:ListItem>
                                <asp:ListItem Value="targetpopulation">Población Objetivo</asp:ListItem>
                                <asp:ListItem Value="deptoName">Departamento</asp:ListItem>
                                <asp:ListItem Value="cityName">Municipio</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha de Creación</asp:ListItem>
                                <asp:ListItem Value="result">Estado Calificación</asp:ListItem>
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
                                <asp:ListItem Value="summoningName">Convocatoria</asp:ListItem>
                                <asp:ListItem Value="operator">Operador</asp:ListItem>
                                <asp:ListItem Value="operatornit">Nit Operador</asp:ListItem>
                                <asp:ListItem Value="projectname">Nombre Proyecto</asp:ListItem>
                                <asp:ListItem Value="targetpopulation">Población Objetivo</asp:ListItem>
                                <asp:ListItem Value="deptoName">Departamento</asp:ListItem>
                                <asp:ListItem Value="cityName">Municipio</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha de Creación</asp:ListItem>
                                <asp:ListItem Value="result">Estado Calificación</asp:ListItem>
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
                                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addProposal.aspx?op=edit&id={0}"
                                    HeaderText="Operaciones" Text="Editar" />
                                <asp:BoundField DataField="id" HeaderText="Id" />
                                <asp:BoundField DataField="summoningName" HeaderText="Convocatoria" />
                                <asp:BoundField DataField="nameOperator" HeaderText="Operador" />
                                <asp:BoundField DataField="operatornit" HeaderText="Nit Operador" />
                                <asp:BoundField DataField="projectname" HeaderText="Nombre Proyecto" />
                                <asp:BoundField DataField="targetpopulation" HeaderText="Población Objetivo" />
                                <asp:BoundField DataField="deptoName" HeaderText="Departamento" />
                                <asp:BoundField DataField="cityName" HeaderText="Municipio" />                                
                                <asp:BoundField DataField="createdate" HeaderText="Fecha de Creación" />
                                <asp:BoundField DataField="result" HeaderText="Estado Calificación" />                                 
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
