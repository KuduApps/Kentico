<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Settings_FormControls_SelectCulture" CodeFile="SelectCulture.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>