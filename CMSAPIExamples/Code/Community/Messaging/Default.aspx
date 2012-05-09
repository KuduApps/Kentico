<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Community_Messaging_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Message --%>
    <cms:APIExamplePanel ID="pnlCreateMessage" runat="server" GroupingText="Message">
        <cms:APIExample ID="apiCreateMessage" runat="server" ButtonText="Create message" InfoMessage="Message 'API example message' was created." ErrorMessage="Message 'API example message' was not created because sender or recipient doesn't exist." />
        <cms:APIExample ID="apiGetAndUpdateMessage" runat="server" ButtonText="Get and update message" APIExampleType="ManageAdditional" InfoMessage="Message 'API example message' was updated." ErrorMessage="Message 'API example message' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateMessages" runat="server" ButtonText="Get and bulk update messages" APIExampleType="ManageAdditional" InfoMessage="All messages matching the condition were updated." ErrorMessage="Messages matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Contact list --%>
    <cms:APIExamplePanel ID="pnlAddUserToContactList" runat="server" GroupingText="Contact list">
        <cms:APIExample ID="apiAddUserToContactList" runat="server" ButtonText="Add user to contact list" InfoMessage="User 'cmseditor' was successfully added to your contact list. If the user was present in your Ignore list prior to this action, he was removed from it." ErrorMessage="User 'cmseditor' already is in your contact list." />
    </cms:APIExamplePanel>
        <%-- Ignore list --%>
    <cms:APIExamplePanel ID="pnlAddUserToIgnoreList" runat="server" GroupingText="Ignore list">
        <cms:APIExample ID="apiAddUserToIgnoreList" runat="server" ButtonText="Add user to ignore list" InfoMessage="User 'cmseditor' was successfully added to your ignore list. If the user was present in your Contact list prior to this action, he was removed from it." ErrorMessage="User 'cmseditor' already is in your ignore list." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Message --%>
    <cms:APIExamplePanel ID="pnlDeleteMessage" runat="server" GroupingText="Message">
        <cms:APIExample ID="apiDeleteMessage" runat="server" ButtonText="Delete message(s)" APIExampleType="CleanUpMain" InfoMessage="All 'API example message' messages and all their dependencies were deleted." ErrorMessage="No 'API example message' was found." />
    </cms:APIExamplePanel>
    <%-- Contact list --%>
    <cms:APIExamplePanel ID="pnlRemoveUserFromContactList" runat="server" GroupingText="Contact list">
        <cms:APIExample ID="apiRemoveUserFromContactList" runat="server" ButtonText="Remove user from contact list" APIExampleType="CleanUpMain" InfoMessage="User 'cmseditor' was removed from your contact list." ErrorMessage="User 'cmseditor' is not present in your contact list." />
    </cms:APIExamplePanel>
        <%-- Ignore list --%>
    <cms:APIExamplePanel ID="pnlRemoveUserFromIgnoreList" runat="server" GroupingText="Ignore list">
        <cms:APIExample ID="apiRemoveUserFromIgnoreList" runat="server" ButtonText="Remove user from ignore list" APIExampleType="CleanUpMain" InfoMessage="User 'cmseditor' was removed from your ignore list." ErrorMessage="User 'cmseditor' is not present in your ignore list." />
    </cms:APIExamplePanel>
</asp:Content>
