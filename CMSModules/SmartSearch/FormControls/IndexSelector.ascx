<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_SmartSearch_FormControls_IndexSelector" CodeFile="IndexSelector.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ObjectType="cms.SearchIndex" SelectionMode="MultipleTextBox"
            OrderBy="IndexDisplayName" ResourcePrefix="indexselect" runat="server"
            ID="usIndexes" AllowEditTextBox="true" ReturnColumnName="IndexName" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
