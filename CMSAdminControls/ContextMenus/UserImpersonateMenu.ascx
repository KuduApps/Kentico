<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserImpersonateMenu.ascx.cs"
    Inherits="CMSAdminControls_ContextMenus_UserImpersonateMenu" %>
<asp:Panel runat="server" ID="pnlUniGridMenu" CssClass="PortalContextMenu WebPartContextMenu"
    EnableViewState="false">
    <asp:Panel runat="server" ID="pnlAdvancedExport" CssClass="ItemLast">
        <asp:Panel runat="server" ID="pnlAdvancedExportPadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgImpersonate" CssClass="Icon" EnableViewState="false" />&nbsp;
            <cms:LocalizedLabel runat="server" ID="lblImpersonate" CssClass="Name" EnableViewState="false"
                ResourceString="users.impersonate" />
        </asp:Panel>
    </asp:Panel>
</asp:Panel>
