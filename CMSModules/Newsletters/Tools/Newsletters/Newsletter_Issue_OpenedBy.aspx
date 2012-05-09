<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_OpenedBy"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Tools - Newsletter issue opened emails" CodeFile="Newsletter_Issue_OpenedBy.aspx.cs" %>

<%@ Register Src="~/CMSModules/Newsletters/Controls/OpenedByFilter.ascx" TagPrefix="cms"
    TagName="OpenedByFilter" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagPrefix="cms" TagName="UniGrid" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="server">
    <div class="PageContent">
        <cms:OpenedByFilter runat="server" ID="fltOpenedBy" />
        <cms:UniGrid runat="server" ID="UniGrid" ShortID="g" OrderBy="OpenedWhen DESC" IsLiveSite="false"
            ObjectType="newsletter.openedemaillist" ShowActionsMenu="true">
            <GridColumns>
                <ug:Column Source="SubscriberFullName" ExternalSourceName="subscribername" Caption="$unigrid.subscribers.columns.subscribername$"
                    Wrap="false" Width="100%">
                    <Tooltip Source="SubscriberFullName" />
                </ug:Column>
                <ug:Column Source="SubscriberEmail" ExternalSourceName="subscriberemail" Caption="$general.email$"
                    Wrap="false">
                    <Tooltip Source="SubscriberEmail" />
                </ug:Column>
                <ug:Column Source="OpenedWhen" Caption="$unigrid.newsletter_issue_openedby.columns.openedwhen$"
                    Wrap="false" />
            </GridColumns>
        </cms:UniGrid>
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" EnableViewState="false"
            OnClientClick="window.close(); return false;" ResourceString="general.close" />
    </div>
</asp:Content>
