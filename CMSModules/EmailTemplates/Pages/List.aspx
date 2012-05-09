<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_EmailTemplates_Pages_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Email Templates - Template List"
    CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSModules/EmailTemplates/Controls/EmailTemplateList.ascx" TagName="EmailTemplateList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Content ID="Content" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Panel ID="pnlSites" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
        <cms:LocalizedLabel runat="server" ID="lblSites" EnableViewState="false" ResourceString="general.site"
            DisplayColon="true" />
        <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" AllowGlobal="true" />
    </asp:Panel>
    <asp:Panel ID="pnlNewEmailTemplate" runat="server" CssClass="PageHeaderLine" EnableViewState="false">
        <asp:Image ID="imgNewEmailTemplate" runat="server" CssClass="NewItemImage" EnableViewState="false" />
        <asp:HyperLink ID="lnkNewEmailTemplate" runat="server" CssClass="NewItemLink" EnableViewState="false" />
    </asp:Panel>
    <asp:Panel ID="pnlUsers" runat="server" CssClass="PageContent">
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cms:EmailTemplateList ID="emailTemplateListElem" runat="server" IsLiveSite="false" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" />
</asp:Content>
