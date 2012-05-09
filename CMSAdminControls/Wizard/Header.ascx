<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_Wizard_Header" CodeFile="Header.ascx.cs" %>
<table cellspacing="0" cellpadding="0" border="0" class="Header">
    <tr>
        <asp:PlaceHolder ID="plcTitle" runat="server">
            <td rowspan="2" class="Title" style="width: 100px;">
                <asp:Label ID="lblTitle" runat="server" />
            </td>
        </asp:PlaceHolder>
        <td class="Header">
            <asp:Label ID="lblHeader" runat="server" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcDescription" runat="Server">
        <tr>
            <td class="Description">
                <asp:Label ID="lblDescription" runat="server" />&nbsp;
            </td>
        </tr>
    </asp:PlaceHolder>
</table>
