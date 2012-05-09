<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Campaign properties" Inherits="CMSModules_WebAnalytics_Pages_Tools_Campaign_New"
    Theme="Default" CodeFile="New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
    <cms:UIForm runat="server" ID="EditForm" ObjectType="analytics.campaign">
        <SecurityCheck Resource="CMS.WebAnalytics" Permission="managecampaigns" />
        <LayoutTemplate>
            <cms:FormField runat="server" ID="fDisplay" Field="CampaignDisplayName" FormControl="LocalizableTextBox" ResourceString="campaign.displayname" DisplayColon="true" Trim="true" />
            <cms:FormField runat="server" ID="fCodeName" Field="CampaignName" FormControl="TextBoxControl" ResourceString="campaign.codename" DisplayColon="true" Trim="true"/>
            <cms:FormField runat="server" ID="fDescription" Field="CampaignDescription" ResourceString="campaign.description" DisplayColon="true">
                <cms:LocalizableTextBox runat="server" ID="txtDescription" TextMode="MultiLine" CssClass="TextAreaField" />
            </cms:FormField>
            <cms:FormField runat="server" ID="fOpenFrom" Field="CampaignOpenFrom" FormControl="CalendarControl" ResourceString="general.openfrom" DisplayColon="true" />
            <cms:FormField runat="server" ID="fOpenTo" Field="CampaignOpenTo" FormControl="CalendarControl" ResourceString="general.opento" DisplayColon="true" />
            <cms:FormField runat="server" ID="fEnabled" Field="CampaignEnabled" FormControl="CheckBoxControl" Value="true" ResourceString="general.enabled" DisplayColon="true" />                                 
            <cms:FormSubmit runat="server" ID="btnSubmit" />
        </LayoutTemplate>
    </cms:UIForm>
</asp:Content>
