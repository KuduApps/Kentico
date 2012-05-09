<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_SelectModule" CodeFile="SelectModule.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%ResourceDisplayName%}"
            SelectionMode="SingleDropDownList" ObjectType="cms.resource" ResourcePrefix="moduleselector"
            AllowEmpty="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
