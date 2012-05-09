<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="CMSModules_Scoring_Controls_UI_Score_List" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>

<cms:UniGrid runat="server" ID="gridElem" ObjectType="om.score" OrderBy="ScoreDisplayName"
    Columns="ScoreID,ScoreDisplayName,ScoreEnabled" IsLiveSite="false" EditActionUrl="Frameset.aspx?scoreId={0}">
    <GridActions>
        <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
        <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$"
            ModuleName="CMS.Scoring" Permissions="Modify" />
    </GridActions>
    <GridColumns>
        <ug:Column Source="ScoreDisplayName" Localize="true" Caption="$general.displayname$"
            Wrap="false">
            <Filter Type="text" Size="200" />
        </ug:Column>
        <ug:Column Source="ScoreEnabled" Caption="$general.enabled$" ExternalSourceName="enabled" Wrap="false" />
        <ug:Column Width="100%" />
    </GridColumns>
    <GridOptions DisplayFilter="true" />
</cms:UniGrid>