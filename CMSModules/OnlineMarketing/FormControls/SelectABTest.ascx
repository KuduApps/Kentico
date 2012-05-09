<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectABTest.ascx.cs"
    Inherits="CMSModules_OnlineMarketing_FormControls_SelectABTest" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniSelector runat="server" ID="ucUniSelector" ObjectType="OM.ABTest" SelectionMode="SingleDropDownList"
            ResourcePrefix="selectabtest" ReturnColumnName="ABTestID" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
