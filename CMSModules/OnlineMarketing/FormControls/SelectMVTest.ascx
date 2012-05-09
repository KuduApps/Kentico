<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectMVTest.ascx.cs"
    Inherits="CMSModules_OnlineMarketing_FormControls_SelectMVTest" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniSelector runat="server" ID="ucUniSelector" ObjectType="OM.MVTest" SelectionMode="SingleDropDownList"
            ResourcePrefix="selectmvtest" ReturnColumnName="MVTestID" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
