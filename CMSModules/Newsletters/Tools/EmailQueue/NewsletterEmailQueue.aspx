<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_EmailQueue_NewsletterEmailQueue"
    Theme="Default" MaintainScrollPositionOnPostback="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Newsletter - E-mail queue" CodeFile="NewsletterEmailQueue.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <span class="InfoLabel">
        <cms:LocalizedLabel ID="lblText" runat="server" EnableViewState="false" ResourceString="NewsletterEmailQueue_List.Intro" /><br />
        <cms:LocalizedLabel runat="server" ID="lblDisabled" EnableViewState="false" Visible="false"
            ResourceString="NewsletterEmailQueue_List.EmailsDisabled" CssClass="FieldLabel" />
    </span>
    <br />
    <cms:UniGrid runat="server" ID="gridElem" ShortID="g" OrderBy="EmailID" IsLiveSite="false"
        ObjectType="newsletter.emailslist" Columns="EmailID, NewsletterDisplayName, IssueSubject, SubscriberEmail, EmailLastSendResult, SiteDisplayName, EmailLastSendAttempt"
        HideControlForZeroRows="false">
        <GridActions>
            <ug:Action Name="resend" Caption="$Unigrid.NewsletterEmailQueue.Actions.Resend$"
                Icon="ResendEmail.png" />
            <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" />
        </GridActions>
        <GridColumns>
            <ug:Column Source="NewsletterDisplayName" Caption="$unigrid.newsletteremailqueue.columns.newsletter$"
                Wrap="false">
                <Filter Type="text" />
            </ug:Column>
            <ug:Column Source="IssueSubject" ExternalSourceName="subject" Caption="$general.subject$"
                Wrap="false">
                <Filter Type="text" />
                <Tooltip Source="IssueSubject" ExternalSourceName="subjecttooltip" />
            </ug:Column>
            <ug:Column Source="SubscriberEmail" Caption="$general.email$" Wrap="false">
                <Filter Type="text" />
            </ug:Column>
            <ug:Column Source="EmailLastSendResult" ExternalSourceName="result" Caption="$Unigrid.NewsletterEmailQueue.Columns.ErrorMessage$"
                Width="100%" Wrap="false">
                <Filter Type="text" />
                <Tooltip Source="EmailLastSendResult" ExternalSourceName="resulttooltip" />
            </ug:Column>
            <ug:Column Source="SiteDisplayName" Caption="$general.site$" Wrap="false">
                <Filter Type="text" />
            </ug:Column>
            <ug:Column Source="EmailLastSendAttempt" Caption="$Unigrid.NewsletterEmailQueue.Columns.LastSendAttempt$"
                Wrap="false" />
        </GridColumns>
        <GridOptions DisplayFilter="true" />
    </cms:UniGrid>
</asp:Content>
