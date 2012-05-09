<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectCampaign.ascx.cs" Inherits="CMSModules_WebAnalytics_FormControls_SelectCampaign" %>

<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>
   
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniSelector ID="usCampaign" ShortID="ss" runat="server" ObjectType="analytics.campaign" ResourcePrefix="campaignselect"
        SelectionMode="SingleDropDownList" ReturnColumnName="CampaignName" AllowEditTextBox="true" AllowEmpty="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>