<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_WebAnalytics_Controls_UI_Campaign_List"
    CodeFile="List.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<cms:UniGrid runat="server" ID="gridElem" ObjectType="analytics.campaign" OrderBy="CampaignDisplayName"
    Columns="CampaignID,CampaignDisplayName,CampaignOpenTo,CampaignEnabled,CampaignOpenFrom"
    IsLiveSite="false" >
    <GridActions Parameters="CampaignID">
        <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
        <ug:Action Name="#delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$"
            ModuleName="CMS.WebAnalytics" Permissions="modify" />
    </GridActions>
    <GridColumns>
        <ug:Column Source="CampaignDisplayName" Caption="$campaignselect.itemname$" Wrap="false">
            <Filter Type="text" />
        </ug:Column>
        <ug:Column Source="CampaignOpenFrom" Caption="$general.openfrom$" Wrap="false">
            <Filter Type="text" />
        </ug:Column>
        <ug:Column Source="CampaignOpenTo" Caption="$general.opento$" Wrap="false">
            <Filter Type="text" />
        </ug:Column>
        <ug:Column Source="CampaignEnabled" Caption="$general.enabled$" Wrap="false" ExternalSourceName="#yesno">
            <Filter Type="bool" />
        </ug:Column>
        <ug:Column Width="100%" />
    </GridColumns>
    <GridOptions DisplayFilter="true" />
</cms:UniGrid>
