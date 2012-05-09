<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Newsletters_Tools_Customers_Customer_Edit_Newsletters" Theme="Default"
    Title="Customer newsletters" CodeFile="Customer_Edit_Newsletters.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcSiteSelector" ID="siteSelector" runat="server">
    <asp:Panel runat="server" ID="pnlSiteSelector" Visible="false">
        <cms:LocalizedLabel runat="server" ID="lblSite" EnableViewState="false" DisplayColon="true"
            ResourceString="General.Site" />
        <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" AllowAll="false" />
    </asp:Panel>
</asp:Content>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
            <asp:Label runat="server" ID="lblTitle" CssClass="InfoLabel" EnableViewState="false" />
            <cms:UniSelector ID="usNewsletters" runat="server" ObjectType="Newsletter.Newsletter"
                SelectionMode="Multiple" ResourcePrefix="newsletterselect" IsLiveSite="false" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
