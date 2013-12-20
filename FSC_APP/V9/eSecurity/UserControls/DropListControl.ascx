<%@ control language="VB" autoeventwireup="false" inherits="DropListControl, App_Web_aajw-zwy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="gtk" %>

<table>
<tr>
<td>
<asp:Label ID="lblCaption"  EnableViewState ="true" runat="server" Text="Label" />
</td>

<td>
    <asp:DropDownList ID="ddlValue" EnableViewState ="true"  runat="server" >
    </asp:DropDownList>
   </td>
    </tr>    
    </table>