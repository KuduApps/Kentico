<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_TrackedLinks"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Tools - Link tracking" CodeFile="Newsletter_Issue_TrackedLinks.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagPrefix="cms" TagName="UniGrid" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="server">
    <div class="PageContent">
        <cms:UniGrid runat="server" ID="UniGrid" ShortID="g" OrderBy="ClickRate DESC" IsLiveSite="false"
            ObjectType="newsletter.linklist">
            <GridActions Parameters="LinkID">
                <ug:Action Name="view" Caption="$Unigrid.Newsletter.Actions.ViewParticipated$" Icon="ViewUsers.png"
                    OnClick="ViewClicks({0}); return false;" ExternalSourceName="view" />
                <ug:Action Name="deleteoutdated" Caption="$Unigrid.Newsletter.Actions.DeleteOutdated$"
                    Icon="DeleteOutdated.png" Confirmation="$General.ConfirmDelete$" ExternalSourceName="deleteoutdated" />
            </GridActions>
            <GridColumns>
                <ug:Column Source="LinkTarget" ExternalSourceName="linktarget" Caption="$general.link$"
                    Wrap="false" Width="100%">
                    <Tooltip Source="LinkTarget" ExternalSourceName="linktargettooltip" />
                    <Filter Type="text" />
                </ug:Column>
                <ug:Column Source="LinkDescription" ExternalSourceName="linkdescription" Caption="$general.description$"
                    Wrap="false">
                    <Tooltip Source="LinkDescription" ExternalSourceName="linkdescriptiontooltip" />
                    <Filter Type="text" />
                </ug:Column>
                <ug:Column Source="UniqueClicks" Caption="$unigrid.newsletter_issue_trackedlinks.columns.uniqueclicks$"
                    Wrap="false" CssClass="TableCell" />
                <ug:Column Source="TotalClicks" Caption="$unigrid.newsletter_issue_trackedlinks.columns.totalclicks$"
                    Wrap="false" CssClass="TableCell" />
                <ug:Column Source="ClickRate" ExternalSourceName="clickrate" Caption="$unigrid.newsletter_issue_trackedlinks.columns.clickrate$"
                    Wrap="false" CssClass="TableCell" />
            </GridColumns>
            <GridOptions DisplayFilter="true" />
        </cms:UniGrid>
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" EnableViewState="false"
            OnClientClick="window.close(); return false;" ResourceString="general.close" />
    </div>
</asp:Content>
