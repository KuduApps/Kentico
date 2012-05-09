<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="CMSModules_Scoring_Controls_UI_Rule_List" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<cms:UniGrid runat="server" ID="gridElem" ObjectType="om.rule" Columns="RuleID,RuleDisplayName,RuleValue,RuleValidUntil,RuleValidity,RuleValidFor,RuleIsRecurring,RuleType"
    IsLiveSite="false">
    <GridActions Parameters="RuleID">
        <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
        <ug:Action Name="#delete" ExternalSourceName="delete" Caption="$General.Delete$"
            Icon="Delete.png" Confirmation="$General.ConfirmDelete$" ModuleName="CMS.Scoring"
            Permissions="Modify" />
    </GridActions>
    <GridColumns>
        <ug:Column Source="RuleDisplayName" Localize="true" Caption="$general.displayname$" Wrap="false">
            <Filter Type="text" />
        </ug:Column>
        <ug:Column Source="RuleValue" Caption="$general.value$" Wrap="false">
            <Filter Type="integer" />
        </ug:Column>
        <ug:Column Source="##ALL##" Caption="$om.score.validity$" Wrap="false" ExternalSourceName="Validity" />
        <ug:Column Source="RuleIsRecurring" Caption="$om.score.isrecurring$" Wrap="false"
            ExternalSourceName="#yesno">
            <Filter Type="bool" />
        </ug:Column>
        <ug:Column Source="RuleType" Caption="$om.score.ruletype$" Wrap="false"  ExternalSourceName="RuleType" />
        <ug:Column Width="100%" />
    </GridColumns>
</cms:UniGrid>
