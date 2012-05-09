<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_Sites_site_list"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="CMSSiteManager - Sites" CodeFile="site_list.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<%@ Register Src="~/CMSAdminControls/UI/System/ActivityBar.ascx" TagName="ActivityBar" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <asp:Label ID="lblErrorMsg" runat="server" Text="" CssClass="ErrorLabel" />
    <cms:UniGrid ID="UniGridSites" runat="server" GridName="site_list.xml" OrderBy="SiteDisplayName"
        IsLiveSite="false" ObjectType="cms.site" />
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</asp:Content>
