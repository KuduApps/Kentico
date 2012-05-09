<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_Macros_MacroDesignerMenu"
    CodeFile="MacroDesignerMenu.ascx.cs" %>
    
<asp:Panel runat="server" ID="pnlObjectMenu" CssClass="PortalContextMenu WebPartContextMenu">
    <cms:ContextMenuItem runat="server" ID="itemMoveUp" />
    <cms:ContextMenuItem runat="server" ID="itemMoveDown" /> 
    <cms:ContextMenuSeparator runat="server" ID="sep1" />
    <cms:ContextMenuItem runat="server" ID="itemMoveToParent" /> 
</asp:Panel>