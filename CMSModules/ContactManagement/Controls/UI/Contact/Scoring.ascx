<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Scoring.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_Contact_Scoring" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniGrid runat="server" ID="gridElem" ShortID="g" OrderBy="ScoreDisplayName" ObjectType="om.contactscorelist"
            IsLiveSite="false" Columns="ScoreID, ScoreDisplayName, ScoreValue, ScoreEnabled"
            ShowObjectMenu="false">
            <GridActions Parameters="ScoreID">
                <ug:Action Name="view" ExternalSourceName="view" Caption="$General.View$" Icon="View.png" />
            </GridActions>
            <GridColumns>
                <ug:Column Source="ScoreDisplayName" Localize="true" Caption="$scoreselect.itemname$" Wrap="false">
                </ug:Column>
                <ug:Column Source="ScoreValue" Caption="$om.score.achievedscore$" Wrap="false">
                </ug:Column>
                <ug:Column Source="ScoreEnabled" ExternalSourceName="#yesno" Caption="$general.enabled$"
                    Wrap="false">
                </ug:Column>
                <ug:Column Width="100%" />
            </GridColumns>
        </cms:UniGrid>
    </ContentTemplate>
</cms:CMSUpdatePanel>
