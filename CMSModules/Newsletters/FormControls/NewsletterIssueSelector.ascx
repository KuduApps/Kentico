<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Newsletters_FormControls_NewsletterIssueSelector" CodeFile="NewsletterIssueSelector.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="usNewsletters" runat="server" IsLiveSite="false" ObjectType="Newsletter.Newsletter"
            SelectionMode="SingleDropDownList" AllowEmpty="true" ResourcePrefix="newsletterselect" /><br />
        <cms:UniSelector ID="usIssues" runat="server" IsLiveSite="false" ObjectType="newsletter.issue"
            SelectionMode="SingleDropDownList" AllowEmpty="true" ResourcePrefix="newsletterissueselect" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
