<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Subscribers.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Contact properties" Inherits="CMSModules_ContactManagement_Pages_Tools_Contact_Membership_Subscribers"
    Theme="Default" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/Newsletters/FormControls/NewsletterSubscriberSelector.ascx"
    TagName="SelectSubscriber" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Contact/Membership/SubscribersFilter.ascx"
    TagName="SubscribersFilter" TagPrefix="cms" %>
<asp:Content ID="contentControls" ContentPlaceHolderID="plcActions" runat="server">
    <div class="PageHeaderItem">
        <cms:SelectSubscriber runat="server" ID="selectSubscriber" />
    </div>
    <div class="ClearBoth">
        &nbsp;
    </div>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:SubscribersFilter runat="server" ID="fltElem" runat="server" />
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
        <ContentTemplate>
            <cms:UniGrid runat="server" ID="gridElem" ObjectType="om.membershipsubscriberlist"
                Columns="MembershipID,SubscriberLastName,SubscriberFirstName,SubscriberEmail,ContactSiteID,ContactFullNameJoined,ContactMergedWithContactID"
                IsLiveSite="false" OrderBy="SubscriberLastName" ShowObjectMenu="false">
                <GridActions Parameters="MembershipID">
                    <ug:Action Name="delete" CommandArgument="MembershipID" Caption="$General.Delete$"
                        Icon="Delete.png" ExternalSourceName="delete" Confirmation="$General.ConfirmDelete$" />
                </GridActions>
                <GridColumns>
                    <ug:Column Source="SubscriberFirstName" Caption="$general.firstname$" Wrap="false" />
                    <ug:Column Source="SubscriberLastName" Caption="$general.lastname$" Wrap="false" />
                    <ug:Column Source="SubscriberEmail" Caption="$general.email$" Wrap="false" />
                    <ug:Column Source="ContactFullNameJoined" Caption="$om.contact.name$" Wrap="false"
                        Name="contactname" />
                    <ug:Column Source="ContactSiteID" ExternalSourceName="#sitenameorglobal" Caption="$general.sitename$"
                        Wrap="false" Name="sitename" />
                    <ug:Column Width="100%" />
                </GridColumns>
            </cms:UniGrid>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
