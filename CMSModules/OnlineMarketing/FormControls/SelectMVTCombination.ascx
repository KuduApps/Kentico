<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectMVTCombination.ascx.cs"
    Inherits="CMSModules_OnlineMarketing_FormControls_SelectMVTCombination" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniSelector runat="server" ID="ucUniSelector" ObjectType="OM.MVTCombination" SelectionMode="SingleDropDownList"
            ResourcePrefix="selectmvtcombination" MaxDisplayedItems="1000" MaxDisplayedTotalItems="1000"
            ReturnColumnName="MVTCombinationName" DisplayNameFormat="{%MVTCombinationCustomName%}" DefaultDisplayNameFormat="{%MVTCombinationName%}" OrderBy="MVTCombinationName" EmptyReplacement="" />
    </ContentTemplate>
</cms:CMSUpdatePanel>

