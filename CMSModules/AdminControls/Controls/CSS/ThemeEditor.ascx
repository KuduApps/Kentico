<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_AdminControls_Controls_CSS_ThemeEditor"
    CodeFile="ThemeEditor.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/FileSystemSelector/FileSystemSelector.ascx"
    TagName="FileSystemSelector" TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:FileSystemSelector ID="selFile" runat="server" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
