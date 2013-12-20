<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchExecution" title="searchExecution" Codebehind="searchExecution.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="id" Selected="True">Id</asp:ListItem>
                                <asp:ListItem Value="projectname">Proyecto</asp:ListItem>
                                <asp:ListItem Value="qualitativeindicators">Indicadores Cualitativos</asp:ListItem>
                                <asp:ListItem Value="learning">Aprendizajes</asp:ListItem>
                                <asp:ListItem Value="testimonyname">Testimonios</asp:ListItem>
                                <asp:ListItem Value="enable">Estado</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha Creación</asp:ListItem>
							</asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Buscar"/>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblSort" runat="server" Text="Ordenar por"></asp:Label>
                            <asp:DropDownList ID="ddlSort" runat="server">
	                            <asp:ListItem Value="id">Id</asp:ListItem>
	                            <asp:ListItem Value="projectname">Proyecto</asp:ListItem>
	                            <asp:ListItem Value="qualitativeindicators">Indicadores Cualitativos</asp:ListItem>
	                            <asp:ListItem Value="learning">Aprendizajes</asp:ListItem>
	                            <asp:ListItem Value="testimonyname">Testimonios</asp:ListItem>
	                           <asp:ListItem Value="enable">Estado</asp:ListItem>
	                            <asp:ListItem Value="username">Usuario</asp:ListItem>
	                            <asp:ListItem Value="createdate">Fecha Creación</asp:ListItem>
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
						<asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="100%" PageSize="10">
							<Columns>
		                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addExecution.aspx?op=edit&id={0}" HeaderText="Operaciones" Text="Editar" />
		                        <asp:BoundField DataField="id" HeaderText="Id" />
		                        <asp:BoundField DataField="PROJECTNAME" HeaderText="Proyecto" />
		                        <asp:BoundField DataField="qualitativeindicators" HeaderText="Indicadores Cualitativos " />
		                        <asp:BoundField DataField="learning" HeaderText="Aprendizaje" />
		                        <asp:BoundField DataField="TESTIMONYNAME" HeaderText="Testimonio" />
		                        <asp:HyperLinkField Target="_blank" HeaderText="Riesgos" DataNavigateUrlFields="idproject"
		                        DataNavigateUrlFormatString="~/FormulationAndAdoption/searchRisk.aspx?idproject={0}" Text="Riesgos" />
		                       <asp:BoundField DataField="enable" HeaderText="Estado" />
		                        <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
		                        <asp:BoundField DataField="createdate" HeaderText="Fecha de Creación" />
							</Columns>
						</asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
