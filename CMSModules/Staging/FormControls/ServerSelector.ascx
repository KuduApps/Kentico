<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Staging_FormControls_ServerSelector" CodeFile="ServerSelector.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%ServerDisplayName%}"
            SelectionMode="SingleDropDownList" ObjectType="staging.server" ResourcePrefix="serverselector"
            AllowEmpty="false" AllowAll="true" ReturnColumnName="ServerID" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
