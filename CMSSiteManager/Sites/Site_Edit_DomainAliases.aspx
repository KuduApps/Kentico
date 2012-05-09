<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_Sites_Site_Edit_DomainAliases"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Site Edit - Domain Aliases"
    CodeFile="Site_Edit_DomainAliases.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:UniGrid ID="UniGridAliases" runat="server" GridName="Aliases_List.xml" OrderBy="SiteDomainAliasName"
        HideControlForZeroRows="true" IsLiveSite="false" />
</asp:Content>
