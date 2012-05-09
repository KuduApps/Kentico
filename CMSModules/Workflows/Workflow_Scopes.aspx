<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Workflows_Workflow_Scopes"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" EnableEventValidation="false"
    Theme="Default" Title="Workflows - Workflow Scopes" CodeFile="Workflow_Scopes.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcSiteSelector" ID="siteSelector" runat="server">
    <cms:LocalizedLabel ID="lblSite" runat="server" ResourceString="general.site" DisplayColon="true"
        EnableViewState="false" />
    <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
</asp:Content>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:UniGrid ID="UniGridWorkflowScopes" runat="server" GridName="Workflow_Scopes.xml"
                OrderBy="ScopeID" IsLiveSite="false" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
