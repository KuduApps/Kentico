<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Community_Friends_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Friend --%>
    <cms:APIExamplePanel ID="pnlCreateFriend" runat="server" GroupingText="Friend">
        <cms:APIExample ID="apiRequestFriendship" runat="server" ButtonText="Request friendship" InfoMessage="Friendship with user 'My new friend' was requested." />
        <cms:APIExample ID="apiApproveFriendship" runat="server" ButtonText="Approve friendship"  InfoMessage="Friendship was approved." ErrorMessage="Friend 'My new friend' was not found." />
        <cms:APIExample ID="apiRejectFriendship" runat="server" ButtonText="Reject friendship"  InfoMessage="Friendship was rejected." ErrorMessage="Friend 'My new friend' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateFriends" runat="server" ButtonText="Get and bulk update friends" APIExampleType="ManageAdditional" InfoMessage="All friends matching the condition were updated." ErrorMessage="Friends matching the condition were not found." />

    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Friend --%>
    <cms:APIExamplePanel ID="pnlDeleteFriends" runat="server" GroupingText="Friend">
        <cms:APIExample ID="apiDeleteFriends" runat="server" ButtonText="Delete friends" APIExampleType="CleanUpMain" InfoMessage="The user 'My new friend' and all her friends were deleted." ErrorMessage="Friend 'My new friend' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
