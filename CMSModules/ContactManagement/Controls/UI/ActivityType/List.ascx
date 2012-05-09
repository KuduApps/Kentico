<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_ActivityType_List" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<cms:unigrid runat="server" id="gridElem" objecttype="om.activitytype" orderby="ActivityTypeDisplayName"
    columns="ActivityTypeID,ActivityTypeDisplayName,ActivityTypeEnabled,ActivityTypeIsCustom"
    islivesite="false" editactionurl="Tab_General.aspx?typeId={0}">
    <GridActions Parameters="ActivityTypeID">
        <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
        <ug:Action Name="#delete" ExternalSourceName="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" ModuleName="CMS.ContactManagement" Permissions="ManageActivities" />
    </GridActions>
    <GridColumns>
        <ug:Column Source="ActivityTypeDisplayName" Caption="$general.name$" Wrap="false" Localize="true">
        <Filter Type="text" Size="250" />
        </ug:Column>
        <ug:Column Source="ActivityTypeEnabled" Caption="$general.enabled$" Wrap="false" ExternalSourceName="#yesno" />
        <ug:Column Source="ActivityTypeIsCustom" Caption="$general.iscustom$" Wrap="false" ExternalSourceName="#yesno">
        <Filter Type="bool" />
        </ug:Column>
        <ug:Column Width="100%" />
    </GridColumns>
    <GridOptions DisplayFilter="true" />
</cms:unigrid>
