<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecycleBinMenu.ascx.cs"
    Inherits="CMSModules_Objects_Controls_RecycleBinMenu" %>
<asp:Panel runat="server" ID="pnlMessageMenu" CssClass="PortalContextMenu WebPartContextMenu">
    <asp:Panel runat="server" ID="pnlRestoreBindings" CssClass="Item">
        <asp:Panel runat="server" ID="pnlRestoreBindingsPadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgRestoreBindings" CssClass="Icon" EnableViewState="false" />&nbsp;
            <cms:LocalizedLabel runat="server" ID="lblRestoreBindings" CssClass="Name" EnableViewState="false"
                ResourceString="objectversioning.recyclebin.restorewithoutsitebindings" />
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlRestoreCurrent" CssClass="Item">
        <asp:Panel runat="server" ID="pnlRestoreCurrentPadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgRestoreCurrent" CssClass="Icon" EnableViewState="false" />&nbsp;
            <cms:LocalizedLabel runat="server" ID="lblRestoreCurrent" CssClass="Name" EnableViewState="false" />
        </asp:Panel>
    </asp:Panel>
</asp:Panel>
