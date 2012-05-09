<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Membership list" Inherits="CMSModules_Membership_Pages_Membership_List"
    Theme="Default" CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSModules/Membership/Controls/Membership/List.ascx" TagName="MembershipList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlSite" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
        <cms:LocalizedLabel runat="server" ID="lblSites" EnableViewState="false" ResourceString="general.site"
            DisplayColon="true" />
        <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" AllowGlobal="true" />
    </asp:Panel>
    <asp:Panel ID="pnlNewMembership" runat="server" CssClass="PageHeaderLine" EnableViewState="false">
        <asp:Image ID="imgNewMembership" runat="server" CssClass="NewItemImage" EnableViewState="false" />
        <asp:HyperLink ID="lnkNewMembership" runat="server" CssClass="NewItemLink" EnableViewState="false" />
    </asp:Panel>
    <div class="PageContent">
        <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
            <ContentTemplate>
                <cms:MembershipList ID="listElem" runat="server" IsLiveSite="false" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </div>   
</asp:Content>
