<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true" Inherits="CMSModules_TagGroups_Pages_Development_TagGroup_List"
    Title="Tag groups - List" Theme="Default" CodeFile="TagGroup_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcSiteSelector" runat="server">
    <asp:PlaceHolder ID="plcSites" runat="server">
        <cms:LocalizedLabel ID="lblSites" runat="server" DisplayColon="true" ResourceString="general.site"
            CssClass="FieldLabel" EnableViewState="false" />
        <cms:SiteSelector runat="server" ID="siteSelector" AllowAll="false" AllowEmpty="false"
            OnlyRunningSites="false" />
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
            <asp:Label ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
            <cms:UniGrid ID="gridTagGroups" runat="server" IsLiveSite="false" Columns="TagGroupID, TagGroupDisplayName" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
