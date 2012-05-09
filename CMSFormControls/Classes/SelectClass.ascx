<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Classes_SelectClass"
    CodeFile="SelectClass.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%ClassDisplayName%}"
            ReturnColumnName="ClassID" ObjectType="cms.class" ResourcePrefix="allowedclasscontrol"
            SelectionMode="SingleDropDownList" AllowEmpty="false" AllowAll="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
