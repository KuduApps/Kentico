<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_FormControls_FullMediaLibrarySelector" CodeFile="FullMediaLibrarySelector.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" RenderMode="InLine">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" ObjectType="Media.Library" SelectionMode="SingleDropDownList"  AllowEditTextBox="false" />        
    </ContentTemplate>
</cms:CMSUpdatePanel>