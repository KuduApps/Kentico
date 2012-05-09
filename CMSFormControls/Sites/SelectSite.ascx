<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Sites_SelectSite" CodeFile="SelectSite.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>
   
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniSelector ID="usSites" ShortID="ss" runat="server" ObjectType="cms.site" SelectionMode="SingleTextBox" AllowEditTextBox="true" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
