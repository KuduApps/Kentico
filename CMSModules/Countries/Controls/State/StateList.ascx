<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StateList.ascx.cs" Inherits="CMSModules_Countries_Controls_State_StateList" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<cms:UniGrid runat="server" ID="gridStates" ObjectType="cms.state" OrderBy="StateDisplayName"
    Columns="StateID, StateDisplayName" IsLiveSite="false" EditActionUrl="Edit.aspx?countryid={%EditedObjectParent.ID%}&stateid={0}"
    WhereCondition="CountryID = {%EditedObjectParent.ID%}">
    <GridActions Parameters="StateID">
        <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
        <ug:Action Name="#delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" />
    </GridActions>
    <GridColumns>
        <ug:Column Source="StateDisplayName" Caption="$Unigrid.Country_State.Columns.StateDisplayName$"
            Wrap="false" Localize="true">
            <Filter Type="text" />
        </ug:Column>
        <ug:Column Width="100%" />
    </GridColumns>
    <GridOptions DisplayFilter="true" />
</cms:UniGrid>
