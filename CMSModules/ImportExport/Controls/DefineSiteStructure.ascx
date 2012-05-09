<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_ImportExport_Controls_DefineSiteStructure" CodeFile="DefineSiteStructure.ascx.cs" %>
<asp:Panel ID="pnlStep" runat="server" CssClass="NewSiteStepContainer">
    <iframe src="<%=ResolveUrl("~/CMSModules/ImportExport/SiteManager/NewSite/DefineSiteStructure/frameset.aspx") + Request.Url.Query + SiteName%>"
        frameborder="0" width="100%" height="430px"></iframe>
</asp:Panel>
