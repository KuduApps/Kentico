<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Tools_ProjectManagement_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Project --%>
    <cms:APIExamplePanel ID="pnlCreateProject" runat="server" GroupingText="Project">
        <cms:APIExample ID="apiCreateProject" runat="server" ButtonText="Create project" InfoMessage="Project 'My new project' was created." ErrorMessage="Project status not found." />
        <cms:APIExample ID="apiGetAndUpdateProject" runat="server" ButtonText="Get and update project" APIExampleType="ManageAdditional" InfoMessage="Project 'My new project' was updated." ErrorMessage="Project 'My new project' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateProjects" runat="server" ButtonText="Get and bulk update projects" APIExampleType="ManageAdditional" InfoMessage="All projects matching the condition were updated." ErrorMessage="Projects matching the condition were not found." />
    </cms:APIExamplePanel>
     <%-- Project task --%>
    <cms:APIExamplePanel ID="pnlCreateProjectTask" runat="server" GroupingText="Project task">
        <cms:APIExample ID="apiCreateProjectTask" runat="server" ButtonText="Create task" InfoMessage="Task 'My new task' was created." />
        <cms:APIExample ID="apiGetAndUpdateProjectTask" runat="server" ButtonText="Get and update task" APIExampleType="ManageAdditional" InfoMessage="Task 'My new task' was updated." ErrorMessage="Task 'My new task' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateProjectTasks" runat="server" ButtonText="Get and bulk update tasks" APIExampleType="ManageAdditional" InfoMessage="All tasks matching the condition were updated." ErrorMessage="Tasks matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Project security --%>
    <cms:APIExamplePanel ID="pnlProjectSecurity" runat="server" GroupingText="Project's security">
        <cms:APIExample ID="apiSetSecurity" runat="server" ButtonText="Set project's security" APIExampleType="ManageAdditional" InfoMessage="Project 'My new project' was updated." ErrorMessage="Project 'My new project' was not found." />
        <cms:APIExample ID="apiAddAuthorizedRole" runat="server" ButtonText="Add authorized role" APIExampleType="ManageAdditional" InfoMessage="Project 'My new project' was updated." ErrorMessage="Project 'My new project' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
   <%-- Project security --%>
    <cms:APIExamplePanel ID="pnlRemoveProjectSecurity" runat="server" GroupingText="Project's security">
        <cms:APIExample ID="apiRemoveAuthorizedRole" runat="server" ButtonText="Remove authorized role" APIExampleType="CleanUpMain" InfoMessage="Role was removed from project 'My new project'." ErrorMessage="Project 'My new project' was not found." />
    </cms:APIExamplePanel>
   <%-- Project task --%>
    <cms:APIExamplePanel ID="pnlDeleteProjectTask" runat="server" GroupingText="Project task">
        <cms:APIExample ID="apiDeleteProjectTask" runat="server" ButtonText="Delete task" APIExampleType="CleanUpMain" InfoMessage="Task 'My new task' and all its dependencies were deleted." ErrorMessage="Task 'My new task' was not found." />
    </cms:APIExamplePanel>
    <%-- Project --%>
    <cms:APIExamplePanel ID="pnlDeleteProject" runat="server" GroupingText="Project">
        <cms:APIExample ID="apiDeleteProject" runat="server" ButtonText="Delete project" APIExampleType="CleanUpMain" InfoMessage="Project 'My new project' and all its dependencies were deleted." ErrorMessage="Project 'My new project' was not found." />
    </cms:APIExamplePanel>
    
    
</asp:Content>
