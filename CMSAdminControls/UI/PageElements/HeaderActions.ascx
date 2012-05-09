<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSAdminControls_UI_PageElements_HeaderActions" CodeFile="HeaderActions.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/Help.ascx" tagname="Help" tagprefix="cms" %>

<table cellpadding="0" cellspacing="0" border="0" class="HeaderActionsParentTable">
    <tr>
        <td style="width: 100%;">
            <asp:Panel ID="pnlActions" runat="server" Visible="true" EnableViewState="false"
                CssClass="Actions">
            </asp:Panel>
        </td>
        <td>
            <cms:Help ID="helpElem" runat="server" EnableViewState="false" />
        </td>
    </tr>
</table>
