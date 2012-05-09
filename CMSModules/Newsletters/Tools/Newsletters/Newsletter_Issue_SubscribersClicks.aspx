<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_SubscribersClicks"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Tools - Participated subscribers" CodeFile="Newsletter_Issue_SubscribersClicks.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagPrefix="cms" TagName="UniGrid" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="server">
    <div class="PageContent">
        <cms:UniGrid runat="server" ID="UniGrid" ShortID="g" OrderBy="Clicks DESC" IsLiveSite="false"
            ObjectType="newsletter.subscriberlinklist" ShowActionsMenu="true">
            <GridColumns>
                <ug:Column Source="SubscriberFullName" Caption="$unigrid.subscribers.columns.subscribername$"
                    ExternalSourceName="subscribername" Wrap="false" Width="100%">
                    <Tooltip Source="SubscriberFullName" />
                    <Filter Type="text" />
                </ug:Column>
                <ug:Column Source="SubscriberEmail" Caption="$general.email$" ExternalSourceName="subscriberemail"
                    Wrap="false">
                    <Tooltip Source="SubscriberEmail" />
                    <Filter Type="text" />
                </ug:Column>
                <ug:Column Source="Clicks" Caption="$unigrid.newsletter_issue_subscribersclicks.columns.clicks$"
                    CssClass="TableCell" Wrap="false" />
            </GridColumns>
            <GridOptions DisplayFilter="true" />
        </cms:UniGrid>
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" OnClientClick="window.close(); return false;"
            ResourceString="general.close" EnableViewState="false" />
    </div>
</asp:Content>
