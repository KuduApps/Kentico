<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_Workflows_Workflow_Step_Roles"
    Theme="Default" CodeFile="Workflow_Step_Roles.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector" TagPrefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label ID="lblSite" runat="server" EnableViewState="false" />
    <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
    <br />
    <br />
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
            <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                Visible="false" />
            <cms:LocalizedLabel ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" ResourceString="roleselect.available"
                EnableViewState="false" />
            <cms:UniSelector ID="usRoles" runat="server" IsLiveSite="false" ObjectType="cms.role"
                SelectionMode="Multiple" ResourcePrefix="addroles" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
