<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Membership_Pages_Users_User_Edit_Subscriptions" Theme="Default"
    CodeFile="User_Edit_Subscriptions.aspx.cs" %>

<%@ Register Src="~/CMSModules/Membership/Controls/Subscriptions.ascx" TagName="Subscriptions"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntSiteSelect" runat="server" ContentPlaceHolderID="plcSiteSelector">
    <cms:LocalizedLabel ID="lblSelectSite" runat="server" ResourceString="general.site"
        DisplayColon="true" />&nbsp;
    <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" AllowEmpty="false"
        ShortID="sl" />
</asp:Content>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:CMSUpdatePanel ID="updateContent" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:Subscriptions ID="elemSubscriptions" runat="server" IsLiveSite="false" ShortID="sb" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
