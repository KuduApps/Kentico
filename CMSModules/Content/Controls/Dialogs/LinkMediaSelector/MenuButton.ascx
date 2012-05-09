<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_Dialogs_LinkMediaSelector_MenuButton" CodeFile="MenuButton.ascx.cs" %>
<asp:Panel ID="pnlMain" runat="server" EnableViewState="false">
    <asp:Image ID="imgIcon" runat="server" EnableViewState="false" />
    <cms:LocalizedLabel ID="lblText" runat="server" EnableViewState="false" />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Panel>
<cms:CMSButton ID="btnMenu" runat="server" EnableViewState="false" OnClick="btnMenu_Click" style="display:none;" />
