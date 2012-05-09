<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Filters_SiteFilter"
    CodeFile="SiteFilter.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Panel CssClass="Filter" runat="server" ID="pnlSearch">
    <asp:PlaceHolder runat="server" ID="plcLabel" EnableViewState="false">
        <cms:LocalizedLabel ID="lblSite" runat="server" EnableViewState="false" />&nbsp;
    </asp:PlaceHolder>
    <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
</asp:Panel>
