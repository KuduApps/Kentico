<%@ Control Language="C#" AutoEventWireup="True"
    Inherits="CMSModules_SearchEngine_Controls_UI_SearchEngine_List" CodeFile="List.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>

<cms:UniGrid runat="server" ID="gridElem" ObjectType="cms.searchengine" OrderBy="SearchEngineDisplayName"
    Columns="SearchEngineID,SearchEngineDisplayName" IsLiveSite="false" EditActionUrl="Edit.aspx?engineId={0}">
    <GridActions Parameters="SearchEngineID">
        <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
        <ug:Action Name="#delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" />
    </GridActions>
    <GridColumns>
        <ug:Column Source="SearchEngineDisplayName" Caption="$general.displayname$" Wrap="false">
            <Filter Type="text" />
        </ug:Column>
        <ug:Column Width="100%" />
    </GridColumns>
    <GridOptions DisplayFilter="true" />
</cms:UniGrid>
