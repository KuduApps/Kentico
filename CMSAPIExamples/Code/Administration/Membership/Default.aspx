<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Administration_Membership_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- User --%>
    <cms:APIExamplePanel ID="pnlCreateUser" runat="server" GroupingText="User">
        <cms:APIExample ID="apiCreateUser" runat="server" ButtonText="Create user" InfoMessage="User 'My new user' was created." />
        <cms:APIExample ID="apiGetAndUpdateUser" runat="server" ButtonText="Get and update user" APIExampleType="ManageAdditional" InfoMessage="User 'My new user' was updated." ErrorMessage="User 'My new user' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateUsers" runat="server" ButtonText="Get and bulk update users" APIExampleType="ManageAdditional" InfoMessage="All users matching the condition were updated." ErrorMessage="Users matching the condition were not found." />
        <cms:APIExample ID="apiAuthenticateUser" runat="server" ButtonText="Authenticate user" APIExampleType="ManageAdditional" InfoMessage="User 'My new user' was authenticated." ErrorMessage="User 'My new user' was not found or wasn't authenticated." />
    </cms:APIExamplePanel>
    <%-- User on site --%>
    <cms:APIExamplePanel ID="pnlAddUserToSite" runat="server" GroupingText="User on site">
        <cms:APIExample ID="apiAddUserToSite" runat="server" ButtonText="Add user to site" APIExampleType="ManageAdditional" InfoMessage="User 'My new user' was added to site." ErrorMessage="User 'My new user' was not found." />
    </cms:APIExamplePanel>
    <%-- Role --%>
    <cms:APIExamplePanel ID="pnlCreateRole" runat="server" GroupingText="Role">
        <cms:APIExample ID="apiCreateRole" runat="server" ButtonText="Create role" InfoMessage="Role 'My new role' was created." />
        <cms:APIExample ID="apiGetAndUpdateRole" runat="server" ButtonText="Get and update role" APIExampleType="ManageAdditional" InfoMessage="Role 'My new role' was updated." ErrorMessage="Role 'My new role' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateRoles" runat="server" ButtonText="Get and bulk update roles" APIExampleType="ManageAdditional" InfoMessage="All roles matching the condition were updated." ErrorMessage="Roles matching the condition were not found." />
    </cms:APIExamplePanel>
     <%-- User role --%>
    <cms:APIExamplePanel ID="pnlCreateUserRole" runat="server" GroupingText="User in role">
        <cms:APIExample ID="apiCreateUserRole" runat="server" ButtonText="Add user to role" APIExampleType="ManageAdditional" InfoMessage="User was added to role 'My new role'." ErrorMessage="User or role were not found." />
    </cms:APIExamplePanel>
    <%-- Session management --%>
    <cms:APIExamplePanel ID="pnlOnlineUsers" runat="server" GroupingText="Session management - requires 'Monitor on-line users' setting enabled.">
        <cms:APIExample ID="apiGetOnlineUsers" runat="server" ButtonText="Get and bulk update on-line users" APIExampleType="ManageAdditional" InfoMessage="All on-line users matching the condition were updated." ErrorMessage="On-line users matching the condition were not found." />
        <cms:APIExample ID="apiIsUserOnline" runat="server" ButtonText="Is user on-line?" APIExampleType="ManageAdditional" InfoMessage="Current user is on-line." ErrorMessage="User is not on-line or 'Monitor on-line users' setting is not enabled." />
        <cms:APIExample ID="apiKickUser" runat="server" ButtonText="Kick user" APIExampleType="ManageAdditional" InfoMessage="User was kicked." ErrorMessage="User was not found." />
    </cms:APIExamplePanel>
    
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- User role --%>
    <cms:APIExamplePanel ID="pnlDeleteUserRole" runat="server" GroupingText="User in role">
        <cms:APIExample ID="apiDeleteUserRole" runat="server" ButtonText="Remove user from role" APIExampleType="CleanUpMain" InfoMessage="Role 'My new role' and all its dependencies were deleted." ErrorMessage="Role 'My new role' was not found." />
    </cms:APIExamplePanel>
    <%-- User on site --%>
    <cms:APIExamplePanel ID="pnlRemoveUserFromSite" runat="server" GroupingText="User on site">
        <cms:APIExample ID="apiRemoveUserFromSite" runat="server" ButtonText="Remove user from site" APIExampleType="CleanUpMain" InfoMessage="User 'My new user' was removed from site." ErrorMessage="User 'My new user' was not found." />
    </cms:APIExamplePanel>
    <%-- User --%>
    <cms:APIExamplePanel ID="pnlDeleteUser" runat="server" GroupingText="User">
        <cms:APIExample ID="apiDeleteUser" runat="server" ButtonText="Delete user" APIExampleType="CleanUpMain" InfoMessage="User 'My new user' and all its dependencies were deleted." ErrorMessage="User 'My new user' was not found." />
    </cms:APIExamplePanel>
     <%-- Role --%>
    <cms:APIExamplePanel ID="pnlDeleteRole" runat="server" GroupingText="Role">
        <cms:APIExample ID="apiDeleteRole" runat="server" ButtonText="Delete role" APIExampleType="CleanUpMain" InfoMessage="Role 'My new role' and all its dependencies were deleted." ErrorMessage="Role 'My new role' was not found." />
    </cms:APIExamplePanel>
    
</asp:Content>
