<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Sites_SiteSelector"
    CodeFile="SiteSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Panel ID="pnlSelector" runat="server" CssClass="SiteSelector">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:UniSelector ID="uniSelector" ShortID="ss" runat="server" ObjectType="cms.site"
                ResourcePrefix="siteselect" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Panel>
