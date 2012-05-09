<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSAPIExamples_Code_OnlineMarketing_Newsletters_Default" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection"
    TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <asp:Panel ID="newslettersLeft" runat="server" Visible="false">
        <cms:APIExampleSection ID="secEmailTemplates" runat="server" Title="Email templates" />
        <%-- Subscription template --%>
        <cms:APIExamplePanel ID="pnlCreateSubscriptionTemplate" runat="server" GroupingText="Subscription template">
            <cms:APIExample ID="apiCreateSubscriptionTemplate" runat="server" ButtonText="Create template"
                InfoMessage="Template 'My new subscription template' was created." />
        </cms:APIExamplePanel>
        <%-- Unsubscription template --%>
        <cms:APIExamplePanel ID="pnlCreateUnsubscriptionTemplate" runat="server" GroupingText="Unsubscription template">
            <cms:APIExample ID="apiCreateUnsubscriptionTemplate" runat="server" ButtonText="Create template"
                InfoMessage="Template 'My new unsubscription template' was created." />
        </cms:APIExamplePanel>
        <%-- Issue template --%>
        <cms:APIExamplePanel ID="pnlCreateIssueTemplate" runat="server" GroupingText="Issue template">
            <cms:APIExample ID="apiCreateIssueTemplate" runat="server" ButtonText="Create template"
                InfoMessage="Template 'My new issue template' was created." />
            <cms:APIExample ID="apiGetAndUpdateIssueTemplate" runat="server" ButtonText="Get and update template"
                APIExampleType="ManageAdditional" InfoMessage="Template 'My new issue template' was updated."
                ErrorMessage="Template 'My new issue template' was not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateIssueTemplates" runat="server" ButtonText="Get and bulk update templates"
                APIExampleType="ManageAdditional" InfoMessage="All templates matching the condition were updated."
                ErrorMessage="Templates matching the condition were not found." />
        </cms:APIExamplePanel>
        <cms:APIExampleSection ID="secNewsletters" runat="server" Title="Newsletters" />
        <%-- Static newsletter --%>
        <cms:APIExamplePanel ID="pnlCreateStaticNewsletter" runat="server" GroupingText="Static newsletter">
            <cms:APIExample ID="apiCreateStaticNewsletter" runat="server" ButtonText="Create newsletter"
                InfoMessage="Newsletter 'My new static newsletter' was created." ErrorMessage="Template 'My new subscription template', template 'My new unsubscription template' or template 'My new issue template' were not found." />
            <cms:APIExample ID="apiGetAndUpdateStaticNewsletter" runat="server" ButtonText="Get and update newsletter"
                APIExampleType="ManageAdditional" InfoMessage="Newsletter 'My new static newsletter' was updated."
                ErrorMessage="Newsletter 'My new static newsletter' was not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateStaticNewsletters" runat="server" ButtonText="Get and bulk update newsletters"
                APIExampleType="ManageAdditional" InfoMessage="All newsletters matching the condition were updated."
                ErrorMessage="Newsletters matching the condition were not found." />
        </cms:APIExamplePanel>
        <%-- Dynamic newsletter --%>
        <cms:APIExamplePanel ID="pnlCreateDynamicNewsletter" runat="server" GroupingText="Dynamic newsletter">
            <cms:APIExample ID="apiCreateDynamicNewsletter" runat="server" ButtonText="Create newsletter"
                InfoMessage="Newsletter 'My new dynamic newsletter' was created." ErrorMessage="Template 'My new subscription template' or template 'My new unsubscription template' were not found." />
            <cms:APIExample ID="apiGetAndUpdateDynamicNewsletter" runat="server" ButtonText="Get and update newsletter"
                APIExampleType="ManageAdditional" InfoMessage="Newsletter 'My new dynamic newsletter' was updated."
                ErrorMessage="Newsletter 'My new dynamic newsletter' was not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateDynamicNewsletters" runat="server" ButtonText="Get and bulk update newsletters"
                APIExampleType="ManageAdditional" InfoMessage="All newsletters matching the condition were updated."
                ErrorMessage="Newsletters matching the condition were not found." />
        </cms:APIExamplePanel>
        <cms:APIExampleSection ID="secSubscribers" runat="server" Title="Subscribers" />
        <%-- Subscriber --%>
        <cms:APIExamplePanel ID="pnlCreateSubscriber" runat="server" GroupingText="Subscriber">
            <cms:APIExample ID="apiCreateSubscriber" runat="server" ButtonText="Create subscriber"
                InfoMessage="Subscriber 'subscriber@localhost.local' was created." />
            <cms:APIExample ID="apiGetAndUpdateSubscriber" runat="server" ButtonText="Get and update subscriber"
                APIExampleType="ManageAdditional" InfoMessage="Subscriber 'subscriber@localhost.local' was updated."
                ErrorMessage="Subscriber 'subscriber@localhost.local' was not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateSubscribers" runat="server" ButtonText="Get and bulk update subscribers"
                APIExampleType="ManageAdditional" InfoMessage="All subscribers matching the condition were updated."
                ErrorMessage="Subscribers matching the condition were not found." />
        </cms:APIExamplePanel>
        <%-- Newsletter subscriber --%>
        <cms:APIExamplePanel ID="pnlSubscribeToNewsletter" runat="server" GroupingText="Newsletter subscriber">
            <cms:APIExample ID="apiSubscribeToNewsletter" runat="server" ButtonText="Subscribe to newsletter"
                InfoMessage="Subscriber 'subscriber@localhost.local' was subscribed to 'My new static newsletter'."
                ErrorMessage="Subscriber 'subscriber@localhost.local' or newsletter 'My new static newsletter' were not found." />
        </cms:APIExamplePanel>
        <cms:APIExampleSection ID="secIssues" runat="server" Title="Issues" />
        <%-- Static issue --%>
        <cms:APIExamplePanel ID="pnlCreateStaticIssue" runat="server" GroupingText="Static issue">
            <cms:APIExample ID="apiCreateStaticIssue" runat="server" ButtonText="Create issue"
                InfoMessage="Issue 'My new static issue' was created." ErrorMessage="Newsletter 'My new static newsletter' was not found." />
            <cms:APIExample ID="apiGetAndUpdateStaticIssue" runat="server" ButtonText="Get and update issue"
                APIExampleType="ManageAdditional" InfoMessage="Issue 'My new static issue' was updated."
                ErrorMessage="Issue 'My new static issue' or newsletter 'My new static newsletter' were not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateStaticIssues" runat="server" ButtonText="Get and bulk update issues"
                APIExampleType="ManageAdditional" InfoMessage="All issues matching the condition were updated."
                ErrorMessage="Issues matching the condition or newsletter 'My new static newsletter' were not found." />
        </cms:APIExamplePanel>
        <%-- Dynamic issue --%>
        <cms:APIExamplePanel ID="pnlCreateDynamicIssue" runat="server" GroupingText="Dynamic issue">
            <cms:APIExample ID="apiCreateDynamicIssue" runat="server" ButtonText="Create issue"
                InfoMessage="Issue 'My new dynamic issue' was created." ErrorMessage="Newsletter 'My new dynamic newsletter' was not found" />
            <cms:APIExample ID="apiGetAndUpdateDynamicIssue" runat="server" ButtonText="Get and update issue"
                APIExampleType="ManageAdditional" InfoMessage="Issue 'My new dynamic issue' was updated."
                ErrorMessage="Issue 'My new dynamic issue' or newsletter 'My new dynamic newsletter' were not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateDynamicIssues" runat="server" ButtonText="Get and bulk update issues"
                APIExampleType="ManageAdditional" InfoMessage="All issues matching the condition were updated."
                ErrorMessage="Issues matching the condition or newsletter 'My new dynamic newsletter' were not found." />
        </cms:APIExamplePanel>
    </asp:Panel>
    <asp:Panel ID="contactManagementLeft" runat="server" Visible="false">
        <%-- Section: Configuration --%>
        <cms:APIExampleSection ID="secManConfiguration" runat="server" Title="Configuration" />
        <%-- Contact role --%>
        <cms:APIExamplePanel ID="pnlCreateContactRole" runat="server" GroupingText="Contact role">
            <cms:APIExample ID="apiCreateContactRole" runat="server" ButtonText="Create role"
                InfoMessage="Role 'My new role' was created." />
            <cms:APIExample ID="apiGetAndUpdateContactRole" runat="server" ButtonText="Get and update role"
                APIExampleType="ManageAdditional" InfoMessage="Role 'My new role' was updated."
                ErrorMessage="Role 'My new role' was not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateContactRoles" runat="server" ButtonText="Get and bulk update roles"
                APIExampleType="ManageAdditional" InfoMessage="All roles matching the condition were updated."
                ErrorMessage="Roles matching the condition were not found." />
        </cms:APIExamplePanel>
        <%-- Contact status --%>
        <cms:APIExamplePanel ID="pnlCreateContactStatus" runat="server" GroupingText="Contact status">
            <cms:APIExample ID="apiCreateContactStatus" runat="server" ButtonText="Create status"
                InfoMessage="Status 'My new status' was created." />
            <cms:APIExample ID="apiGetAndUpdateContactStatus" runat="server" ButtonText="Get and update status"
                APIExampleType="ManageAdditional" InfoMessage="Status 'My new status' was updated."
                ErrorMessage="Status 'My new status' was not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateContactStatuses" runat="server" ButtonText="Get and bulk update statuses"
                APIExampleType="ManageAdditional" InfoMessage="All statuses matching the condition were updated."
                ErrorMessage="Statuses matching the condition were not found." />
        </cms:APIExamplePanel>
        <%-- Account status --%>
        <cms:APIExamplePanel ID="pnlCreateAccountStatus" runat="server" GroupingText="Account status">
            <cms:APIExample ID="apiCreateAccountStatus" runat="server" ButtonText="Create status"
                InfoMessage="Status 'My new status' was created." />
            <cms:APIExample ID="apiGetAndUpdateAccountStatus" runat="server" ButtonText="Get and update status"
                APIExampleType="ManageAdditional" InfoMessage="Status 'My new status' was updated."
                ErrorMessage="Status 'My new status' was not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateAccountStatuses" runat="server" ButtonText="Get and bulk update statuses"
                APIExampleType="ManageAdditional" InfoMessage="All statuses matching the condition were updated."
                ErrorMessage="Statuses matching the condition were not found." />
        </cms:APIExamplePanel>
        <%-- Section: Contact Management --%>
        <cms:APIExampleSection ID="secManContactManagement" runat="server" Title="Contact Management" />
        <%-- Contact --%>
        <cms:APIExamplePanel ID="pnlCreateContact" runat="server" GroupingText="Contact">
            <cms:APIExample ID="apiCreateContact" runat="server" ButtonText="Create contact"
                InfoMessage="Contact 'My new contact' was created." />
            <cms:APIExample ID="apiGetAndUpdateContact" runat="server" ButtonText="Get and update contact"
                APIExampleType="ManageAdditional" InfoMessage="Contact 'My new contact' was updated."
                ErrorMessage="Contact 'My new contact' was not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateContacts" runat="server" ButtonText="Get and bulk update contacts"
                APIExampleType="ManageAdditional" InfoMessage="All contacts matching the condition were updated."
                ErrorMessage="Contacts matching the condition were not found." />
        </cms:APIExamplePanel>
        <%-- Contact status --%>
        <cms:APIExamplePanel ID="pnlAddContactStatusToContact" runat="server" GroupingText="Contact status">
            <cms:APIExample ID="apiAddContactStatusToContact" runat="server" ButtonText="Add status to contact"
                InfoMessage="Status 'My new status' was assigned to contact." ErrorMessage="Contact 'My new contact' or contact status 'My new status' were not found or relationship already exists." />
        </cms:APIExamplePanel>
        <%-- Contact membership --%>
        <cms:APIExamplePanel ID="pnlAddMembership" runat="server" GroupingText="Contact membership">
            <cms:APIExample ID="apiAddMembership" runat="server" ButtonText="Add membership to contact"
                InfoMessage="Current user was assigned to contact." ErrorMessage="Contact 'My new contact' was not found." />
        </cms:APIExamplePanel>
        <%-- Contact IP address--%>
        <cms:APIExamplePanel ID="pnlAddIPAddress" runat="server" GroupingText="Contact IP address">
            <cms:APIExample ID="apiAddIPAddress" runat="server" ButtonText="Add IP to contact"
                InfoMessage="IP address was assigned to contact." ErrorMessage="Contact 'My new contact' was not found." />
        </cms:APIExamplePanel>
        <%-- Contact user agent info--%>
        <cms:APIExamplePanel ID="pnlAddUserAgent" runat="server" GroupingText="Contact user agent">
            <cms:APIExample ID="apiAddUserAgent" runat="server" ButtonText="Add user agent to contact"
                InfoMessage="User agent was assigned to contact." ErrorMessage="Contact 'My new contact' was not found." />
        </cms:APIExamplePanel>
        <%-- Account --%>
        <cms:APIExamplePanel ID="pnlCreateAccount" runat="server" GroupingText="Account">
            <cms:APIExample ID="apiCreateAccount" runat="server" ButtonText="Create account"
                InfoMessage="Account 'My new account' was created." />
            <cms:APIExample ID="apiGetAndUpdateAccount" runat="server" ButtonText="Get and update account"
                APIExampleType="ManageAdditional" InfoMessage="Account 'My new account' was updated."
                ErrorMessage="Account 'My new account' was not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateAccounts" runat="server" ButtonText="Get and bulk update accounts"
                APIExampleType="ManageAdditional" InfoMessage="All accounts matching the condition were updated."
                ErrorMessage="Accounts matching the condition were not found." />
        </cms:APIExamplePanel>
        <%-- Account status --%>
        <cms:APIExamplePanel ID="pnlAddAccountStatusToAccount" runat="server" GroupingText="Account status">
            <cms:APIExample ID="apiAddAccountStatusToAccount" runat="server" ButtonText="Add status to account"
                InfoMessage="Status 'My new status' was assigned to account." ErrorMessage="Account 'My new contact' or account status 'My new status' were not found or relationship already exists." />
        </cms:APIExamplePanel>
        <%-- Account contacts --%>
        <cms:APIExamplePanel ID="pnlAddContactToAccount" runat="server" GroupingText="Account contacts">
            <cms:APIExample ID="apiAddContactToAccount" runat="server" ButtonText="Add contact to account"
                InfoMessage="Contact 'My new contact' was assigned to account." ErrorMessage="Contact 'My new contact' or account 'My new account' were not found." />
        </cms:APIExamplePanel>
        <%-- Section: Segmentation --%>
        <cms:APIExampleSection ID="secManSegmentation" runat="server" Title="Segmentation" />
        <%-- Contact group --%>
        <cms:APIExamplePanel ID="pnlCreateContactGroup" runat="server" GroupingText="Contact group">
            <cms:APIExample ID="apiCreateContactGroup" runat="server" ButtonText="Create group"
                InfoMessage="Group 'My new group' was created." />
            <cms:APIExample ID="apiGetAndUpdateContactGroup" runat="server" ButtonText="Get and update group"
                APIExampleType="ManageAdditional" InfoMessage="Group 'My new group' was updated."
                ErrorMessage="Group 'My new group' was not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateContactGroups" runat="server" ButtonText="Get and bulk update groups"
                APIExampleType="ManageAdditional" InfoMessage="All groups matching the condition were updated."
                ErrorMessage="Groups matching the condition were not found." />
        </cms:APIExamplePanel>
        <%-- Contact in contact group --%>
        <cms:APIExamplePanel ID="pnlAddContactToGroup" runat="server" GroupingText="Contact in group">
            <cms:APIExample ID="apiAddContactToGroup" runat="server" ButtonText="Add contact to group"
                InfoMessage="Contact 'My new contact' was assigned to group." ErrorMessage="Contact 'My new contact' or group 'My new group' were not found." />
        </cms:APIExamplePanel>
        <%-- Account in contact group --%>
        <cms:APIExamplePanel ID="pnlAddAccountToGroup" runat="server" GroupingText="Account in group">
            <cms:APIExample ID="apiAddAccountToGroup" runat="server" ButtonText="Add account to group"
                InfoMessage="Account 'My new account' was assigned to account." ErrorMessage="Account 'My new account' or group 'My new group' were not found." />
        </cms:APIExamplePanel>
        <%-- Section: Activities --%>
        <cms:APIExampleSection ID="secManActivities" runat="server" Title="Activities" />
        <%-- Activity --%>
        <cms:APIExamplePanel ID="pnlCreateActivity" runat="server" GroupingText="Activity">
            <cms:APIExample ID="apiCreateActivity" runat="server" ButtonText="Create activity"
                ErrorMessage="Contact 'My new contact' was not found" InfoMessage="Activity 'My new activity' was created." />
            <cms:APIExample ID="apiGetAndUpdateActivity" runat="server" ButtonText="Get and update activity"
                APIExampleType="ManageAdditional" InfoMessage="Activity 'My new activity' was updated."
                ErrorMessage="Activity 'My new activity' was not found." />
            <cms:APIExample ID="apiGetAndBulkUpdateActivities" runat="server" ButtonText="Get and bulk update activities"
                APIExampleType="ManageAdditional" InfoMessage="All activities matching the condition were updated."
                ErrorMessage="Activities matching the condition were not found." />
        </cms:APIExamplePanel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <asp:Panel ID="newslettersRight" runat="server" Visible="false">
        <cms:APIExampleSection ID="secNewsIssues" runat="server" Title="Issues" />
        <%-- Dynamic issue --%>
        <cms:APIExamplePanel ID="pnlDeleteDynamicIssue" runat="server" GroupingText="Dynamic issue">
            <cms:APIExample ID="apiDeleteDynamicIssue" runat="server" ButtonText="Delete issue"
                APIExampleType="CleanUpMain" InfoMessage="Issue 'My new dynamic issue' and all its dependencies were deleted."
                ErrorMessage="Issue 'My new dynamic issue' or newsletter 'My new dynamic newsletter' were not found." />
        </cms:APIExamplePanel>
        <%-- Static issue --%>
        <cms:APIExamplePanel ID="pnlDeleteStaticIssue" runat="server" GroupingText="Static issue">
            <cms:APIExample ID="apiDeleteStaticIssue" runat="server" ButtonText="Delete issue"
                APIExampleType="CleanUpMain" InfoMessage="Issue 'My new static issue' and all its dependencies were deleted."
                ErrorMessage="Issue 'My new static issue' or newsletter 'My new static newsletter' were not found." />
        </cms:APIExamplePanel>
        <cms:APIExampleSection ID="APIExampleSection2" runat="server" Title="Subscribers" />
        <%-- Newsletter subscriber --%>
        <cms:APIExamplePanel ID="pnlUnsubscribeSubscriber" runat="server" GroupingText="Newsletter subscriber">
            <cms:APIExample ID="apiUnsubscribeFromNewsletter" runat="server" ButtonText="Unsubscribe from newsletter"
                APIExampleType="CleanUpMain" InfoMessage="Subscriber 'subscriber@localhost.local' was unsubscribed from 'My new static newsletter'."
                ErrorMessage="Subscriber 'subscriber@localhost.local' or newsletter 'My new static newsletter' were not found." />
        </cms:APIExamplePanel>
        <%-- Subscriber --%>
        <cms:APIExamplePanel ID="pnlDeleteSubscriber" runat="server" GroupingText="Subscriber">
            <cms:APIExample ID="apiDeleteSubscriber" runat="server" ButtonText="Delete subscriber"
                APIExampleType="CleanUpMain" InfoMessage="Subscriber 'subscriber@localhost.local' and all its dependencies were deleted."
                ErrorMessage="Subscriber 'subscriber@localhost.local' was not found." />
        </cms:APIExamplePanel>
        <cms:APIExampleSection ID="APIExampleSection3" runat="server" Title="Newsletters" />
        <%-- Dynamic newsletter --%>
        <cms:APIExamplePanel ID="pnlDeleteDynamicNewsletter" runat="server" GroupingText="Dynamic newsletter">
            <cms:APIExample ID="apiDeleteDynamicNewsletter" runat="server" ButtonText="Delete newsletter"
                APIExampleType="CleanUpMain" InfoMessage="Newsletter 'My new dynamic newsletter' and all its dependencies were deleted."
                ErrorMessage="Newsletter 'My new dynamic newsletter' was not found." />
        </cms:APIExamplePanel>
        <%-- Static newsletter --%>
        <cms:APIExamplePanel ID="pnlDeleteStaticNewsletter" runat="server" GroupingText="Static newsletter">
            <cms:APIExample ID="apiDeleteStaticNewsletter" runat="server" ButtonText="Delete newsletter"
                APIExampleType="CleanUpMain" InfoMessage="Newsletter 'My new static newsletter' and all its dependencies were deleted."
                ErrorMessage="Newsletter 'My new static newsletter' was not found." />
        </cms:APIExamplePanel>
        <cms:APIExampleSection ID="APIExampleSection4" runat="server" Title="Email templates" />
        <%-- Subscription template --%>
        <cms:APIExamplePanel ID="pnlDeleteSubscriptionTemplate" runat="server" GroupingText="Subscription template">
            <cms:APIExample ID="apiDeleteSubscriptionTemplate" runat="server" ButtonText="Delete template"
                APIExampleType="CleanUpMain" InfoMessage="Template 'My new subscription template' and all its dependencies were deleted."
                ErrorMessage="Template 'My new subscription template' was not found." />
        </cms:APIExamplePanel>
        <%-- Unsubscription template --%>
        <cms:APIExamplePanel ID="pnlDeleteUnsubscriptionTemplate" runat="server" GroupingText="Unsubscription template">
            <cms:APIExample ID="apiDeleteUnsubscriptionTemplate" runat="server" ButtonText="Delete template"
                APIExampleType="CleanUpMain" InfoMessage="Template 'My new unsubscription template' and all its dependencies were deleted."
                ErrorMessage="Template 'My new unsubscription template' was not found." />
        </cms:APIExamplePanel>
        <%-- Issue template --%>
        <cms:APIExamplePanel ID="pnlDeleteIssueTemplate" runat="server" GroupingText="Issue template">
            <cms:APIExample ID="apiDeleteIssueTemplate" runat="server" ButtonText="Delete template"
                APIExampleType="CleanUpMain" InfoMessage="Template 'My new issue template' and all its dependencies were deleted."
                ErrorMessage="Template 'My new issue template' was not found." />
        </cms:APIExamplePanel>
    </asp:Panel>
    <asp:Panel ID="contactManagementRight" runat="server" Visible="false">
        <%-- Section: Activities --%>
        <cms:APIExampleSection ID="secClearActivities" runat="server" Title="Activities" />
        <%-- Activity --%>
        <cms:APIExamplePanel ID="pnlDeleteActivity" runat="server" GroupingText="Activity">
            <cms:APIExample ID="apiDeleteActivity" runat="server" ButtonText="Delete activity"
                APIExampleType="CleanUpMain" InfoMessage="Activity 'My new activity' and all its dependencies were deleted."
                ErrorMessage="Activity 'My new activity' was not found." />
        </cms:APIExamplePanel>
        <%-- Section: Segmentation --%>
        <cms:APIExampleSection ID="secClearSegmentation" runat="server" APIExampleType="CleanUpMain"
            Title="Segmentation" />
        <%-- Account in contact group --%>
        <cms:APIExamplePanel ID="pnlRemoveAccountFromGroup" runat="server" GroupingText="Account in group">
            <cms:APIExample ID="apiRemoveAccountFromGroup" runat="server" ButtonText="Remove account from group"
                APIExampleType="CleanUpMain" InfoMessage="Account 'My new account' was removed from the group."
                ErrorMessage="Account 'My new account', group 'My new group' or their relationship were not found." />
        </cms:APIExamplePanel>
        <%-- Contact in contact group --%>
        <cms:APIExamplePanel ID="pnlRemoveContactFromGroup" runat="server" GroupingText="Contact in group">
            <cms:APIExample ID="apiRemoveContactFromGroup" runat="server" APIExampleType="CleanUpMain"
                ButtonText="Remove contact from group" InfoMessage="Contact 'My new contact' was removed form the group."
                ErrorMessage="Contact 'My new contact', group 'My new group' or their relationship were not found." />
        </cms:APIExamplePanel>
        <%-- Contact group --%>
        <cms:APIExamplePanel ID="pnlDeleteContactGroup" runat="server" GroupingText="Contact group">
            <cms:APIExample ID="apiDeleteContactGroup" runat="server" APIExampleType="CleanUpMain"
                ButtonText="Delete group" InfoMessage="Group 'My new group' was deleted." ErrorMessage="Group 'My new group' was not found." />
        </cms:APIExamplePanel>
        <%-- Section: Contact Management --%>
        <cms:APIExampleSection ID="secClearContactManagement" runat="server" Title="Contact Management" />
        <%-- Account contacts --%>
        <cms:APIExamplePanel ID="pnlRemoveContactFromAccount" runat="server" GroupingText="Account contacts">
            <cms:APIExample ID="apiRemoveContactFromAccount" runat="server" APIExampleType="CleanUpMain"
                ButtonText="Remove contact from account" InfoMessage="Contact 'My new contact' was removed from account."
                ErrorMessage="Contact 'My new contact', account 'My new account' or their relationship were not found." />
        </cms:APIExamplePanel>
        <%-- Account status --%>
        <cms:APIExamplePanel ID="pnlRemoveAccountStatusFromAccount" runat="server" GroupingText="Account status">
            <cms:APIExample ID="apiRemoveAccountStatusFromAccount" runat="server" APIExampleType="CleanUpMain"
                ButtonText="Remove status from account" InfoMessage="Status 'My new status' was removed from account."
                ErrorMessage="Account 'My new contact', account status 'My new status' or their relationship were not found." />
        </cms:APIExamplePanel>
        <%-- Account --%>
        <cms:APIExamplePanel ID="pnlDeleteAccount" runat="server" GroupingText="Account">
            <cms:APIExample ID="apiDeleteAccount" runat="server" ButtonText="Delete account"
                APIExampleType="CleanUpMain" InfoMessage="Account 'My new account' and all its dependencies were deleted."
                ErrorMessage="Account 'My new account' was not found." />
        </cms:APIExamplePanel>
        <%-- Contact IP address--%>
        <cms:APIExamplePanel ID="pnlRemoveIPAddress" runat="server" GroupingText="Contact IP address">
            <cms:APIExample ID="apiRemoveIPAddress" runat="server" APIExampleType="CleanUpMain"
                ButtonText="Remove IP from contact" InfoMessage="IP address was removed from contact."
                ErrorMessage="Contact 'My new contact' or its IP address were not found." />
        </cms:APIExamplePanel>
        <%-- Contact user agent--%>
        <cms:APIExamplePanel ID="pnlRemoveUserAgent" runat="server" GroupingText="Contact user agent">
            <cms:APIExample ID="apiRemoveUserAgent" runat="server" APIExampleType="CleanUpMain"
                ButtonText="Remove contact's user agent" InfoMessage="User agent was removed from contact."
                ErrorMessage="Contact 'My new contact' or its user agent address were not found." />
        </cms:APIExamplePanel>
        <%-- Contact membership --%>
        <cms:APIExamplePanel ID="pnlRemoveMembership" runat="server" GroupingText="Contact membership">
            <cms:APIExample ID="apiRemoveMembership" runat="server" APIExampleType="CleanUpMain"
                ButtonText="Remove contact's membership" InfoMessage="Current user was removed from contact."
                ErrorMessage="Membership, contact or their relationship were not found." />
        </cms:APIExamplePanel>
        <%-- Contact status --%>
        <cms:APIExamplePanel ID="pnlRemoveContactStatusFromContact" runat="server" GroupingText="Contact status">
            <cms:APIExample ID="apiRemoveContactStatusFromContact" APIExampleType="CleanUpMain"
                runat="server" ButtonText="Remove status from contact" InfoMessage="Status 'My new status' was removed from contact."
                ErrorMessage="Contact 'My new contact', contact status 'My new status' or their relationship were not found." />
        </cms:APIExamplePanel>
        <%-- Contact --%>
        <cms:APIExamplePanel ID="pnlDeleteContact" runat="server" GroupingText="Contact">
            <cms:APIExample ID="apiDeleteContact" runat="server" ButtonText="Delete contact"
                APIExampleType="CleanUpMain" InfoMessage="Contact 'My new contact' and all its dependencies were deleted."
                ErrorMessage="Contact 'My new contact' was not found." />
        </cms:APIExamplePanel>
        <%-- Section: Configuration --%>
        <cms:APIExampleSection ID="secCleanConfiguration" runat="server" Title="Configuration" />
        <%-- Account status --%>
        <cms:APIExamplePanel ID="pnlDeleteAccountStatus" runat="server" GroupingText="Account status">
            <cms:APIExample ID="apiDeleteAccountStatus" runat="server" ButtonText="Delete status"
                APIExampleType="CleanUpMain" InfoMessage="Status 'My new status' and all its dependencies were deleted."
                ErrorMessage="Status 'My new status' was not found." />
        </cms:APIExamplePanel>
        <%-- Contact status --%>
        <cms:APIExamplePanel ID="pnlDeleteContactStatus" runat="server" GroupingText="Contact status">
            <cms:APIExample ID="apiDeleteContactStatus" runat="server" ButtonText="Delete status"
                APIExampleType="CleanUpMain" InfoMessage="Status 'My new status' and all its dependencies were deleted."
                ErrorMessage="Status 'My new status' was not found." />
        </cms:APIExamplePanel>
        <%-- Contact role --%>
        <cms:APIExamplePanel ID="pnlDeleteContactRole" runat="server" GroupingText="Contact role">
            <cms:APIExample ID="apiDeleteContactRole" runat="server" ButtonText="Delete role"
                APIExampleType="CleanUpMain" InfoMessage="Role 'My new role' and all its dependencies were deleted."
                ErrorMessage="Role 'My new role' was not found." />
        </cms:APIExamplePanel>
    </asp:Panel>
</asp:Content>
