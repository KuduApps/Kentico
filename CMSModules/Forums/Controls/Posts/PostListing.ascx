<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Forums_Controls_Posts_PostListing"
    CodeFile="PostListing.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<cms:UniGrid ID="gridPosts" runat="server" GridName="~/CMSModules/Forums/Controls/Posts/PostListing.xml"
    EnableViewState="true" DelayedReload="false" IsLiveSite="false" OrderBy="PostTime" />
<br />
<asp:Panel ID="pnlFooter" runat="server" Style="clear: both;">
    <%--<asp:DropDownList ID="drpWhat" runat="server" CssClass="DropDownFieldSmall" />--%>
    <cms:LocalizedLabel ID="lblSelectedPosts" ResourceString="forums.listing.selectedposts"
        DisplayColon="true" runat="server" EnableViewState="false" />
    <asp:DropDownList ID="drpAction" runat="server" CssClass="DropDownFieldSmall" />
    <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.ok" CssClass="SubmitButton SelectorButton"
        EnableViewState="false" />
    <br />
    <br />
</asp:Panel>
<asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />