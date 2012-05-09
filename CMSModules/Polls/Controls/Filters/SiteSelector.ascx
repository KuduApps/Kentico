<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteSelector.ascx.cs"
    Inherits="CMSModules_Polls_Controls_Filters_SiteSelector" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteOrGlobalSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Panel CssClass="Filter" runat="server" ID="pnlSearch">
    <cms:LocalizedLabel runat="server" ID="lblSite" EnableViewState="false" DisplayColon="true"
        ResourceString="General.Site" />
    <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
</asp:Panel>
