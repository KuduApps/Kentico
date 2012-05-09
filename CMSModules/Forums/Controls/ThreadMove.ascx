<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Forums_Controls_ThreadMove" CodeFile="ThreadMove.ascx.cs" %>
<asp:PlaceHolder ID="plcMoveInner" runat="server">
    <cms:LocalizedLabel runat="server" ID="lblMove" ResourceString="forum.thread.movetopic" AssociatedControlID="drpMoveToForum" DisplayColon="true" />&nbsp;
    <asp:DropDownList ID="drpMoveToForum" CssClass="DropDownField" runat="server" />&nbsp;
    <cms:LocalizedLinkButton ID="btnMove" runat="server" ResourceString="general.move" />&nbsp;
</asp:PlaceHolder>
<asp:Label runat="server" ID="lblMoveError" EnableViewState="false" Visible="false" />&nbsp;
<asp:Label runat="server" ID="lblMoveInfo" EnableViewState="false" Visible="false" />&nbsp;
<asp:Literal ID="ltlMoveBack" runat="server" />