<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Execution_subActivityMainPanelTODO_LIST" Codebehind="subActivityMainPanelTODO-LIST.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="id" Selected="True">Id</asp:ListItem>
                              
                                <asp:ListItem Value="projectname">Proyecto</asp:ListItem>
                                <asp:ListItem Value="projectphase">Fase</asp:ListItem>
                                <asp:ListItem Value="componentname">Componente</asp:ListItem>
                                <asp:ListItem Value="name">Nombre/Descripción</asp:ListItem>
                                <asp:ListItem Value="type">Tipo</asp:ListItem>
                              <%--  <asp:ListItem Value="state">Estado</asp:ListItem>--%>
                                <asp:ListItem Value="begindate">Fecha Inicial</asp:ListItem>
                                <asp:ListItem Value="enddate">Fecha Final</asp:ListItem>                                
                                <asp:ListItem Value="approval">Aprobación</asp:ListItem>
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
                                <asp:ListItem Value="id" Selected="True">Id</asp:ListItem>
                                <asp:ListItem Value="strategicobjective">Objetivo estratégico</asp:ListItem>
                                <asp:ListItem Value="StrategicLine">Linea Estrategica</asp:ListItem>
                                <asp:ListItem Value="strategy">Estrategia</asp:ListItem>
                                <asp:ListItem Value="projectname">Proyecto</asp:ListItem>
                                <asp:ListItem Value="projectphase">Fase</asp:ListItem>
                                <asp:ListItem Value="componentname">Componente</asp:ListItem>
                                <asp:ListItem Value="name">Nombre/Descripción</asp:ListItem>
                                <asp:ListItem Value="type">Tipo</asp:ListItem>
                                <asp:ListItem Value="state">Estado</asp:ListItem>
                                <asp:ListItem Value="begindate">Fecha Inicial</asp:ListItem>
                                <asp:ListItem Value="enddate">Fecha Final</asp:ListItem>                                
                                <asp:ListItem Value="approval">Aprobación</asp:ListItem>
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
                                <asp:BoundField DataField="idReal" HeaderText="Id" />
                            <asp:BoundField DataField="strategicobjectiveText" HeaderText="Objetivo estratégico"
                                    HtmlEncode="false"  Visible="false"/>
                                <asp:BoundField DataField="StrategicLineText" HeaderText="Linea Estrategica" HtmlEncode="false" Visible="false" />
                                <asp:BoundField DataField="strategyText" HeaderText="Estrategia" HtmlEncode="false" Visible="false" />
                                <asp:BoundField DataField="projectname" HeaderText="Proyecto" />
                                <asp:BoundField DataField="projectphaseText" HeaderText="Fase" />
                                <asp:BoundField DataField="componentname" HeaderText="Componente" />
                                <asp:BoundField DataField="name" HeaderText="Nombre/Descripción" />
                                <asp:HyperLinkField DataNavigateUrlFields="attachment" 
                                    DataTextField="attachment" HeaderText="Anexo" Target="_blank" />
                                <asp:BoundField DataField="typeText" HeaderText="Tipo" />
                             <asp:BoundField DataField="stateText" HeaderText="Estado" Visible="false" />
                                <asp:BoundField DataField="begindateText" HeaderText="Fecha Inicial" HtmlEncode="false" />
                                <asp:BoundField DataField="enddateText" HeaderText="Fecha Final" />
                                <asp:BoundField DataField="username" HeaderText="Usuario" />
                                <asp:BoundField DataField="approvalText" HeaderText="Aprobación" />
                                <asp:BoundField DataField="type" HeaderText="Tipo" />
                                <asp:BoundField DataField="measuramentDateByIndicatorText" HeaderText="Fechas de Medición"
                                    HtmlEncode="false" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
