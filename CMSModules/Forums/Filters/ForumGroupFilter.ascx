<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Forums_Filters_ForumGroupFilter" CodeFile="ForumGroupFilter.ascx.cs" %>
<%@ Register Src="~/CMSModules/Forums/FormControls/ForumGroupSelector.ascx" TagName="ForumGroupSelector"
    TagPrefix="cms" %>
<asp:Panel CssClass="Filter" runat="server" ID="pnlSearch">
    <cms:LocalizedLabel ID="lblSite" runat="server" DisplayColon="true" ResourceString="forums.forumgroup" EnableViewState="false" />
    <cms:ForumGroupSelector ID="forumGroupSelector" runat="server" />
</asp:Panel>
