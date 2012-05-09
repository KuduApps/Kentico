<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Tools_Staging_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Staging server --%>
    <cms:APIExamplePanel ID="pnlCreateStagingServer" runat="server" GroupingText="Staging server">
        <cms:APIExample ID="apiCreateStagingServer" runat="server" ButtonText="Create server" InfoMessage="Server 'My new server' was created." />
        <cms:APIExample ID="apiGetAndUpdateStagingServer" runat="server" ButtonText="Get and update server" APIExampleType="ManageAdditional" InfoMessage="Server 'My new server' was updated." ErrorMessage="Server 'My new server' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateStagingServers" runat="server" ButtonText="Get and bulk update servers" APIExampleType="ManageAdditional" InfoMessage="All servers matching the condition were updated." ErrorMessage="Servers matching the condition were not found." />
    </cms:APIExamplePanel>
    <cms:APIExamplePanel ID="pnlStagingTasksDocuments" runat="server" GroupingText="Staging task">
        <cms:APIExample ID="apiGetAndSynchronizeTasks" runat="server" ButtonText="Get and synchronize tasks" InfoMessage="The tasks were synchronized." ErrorMessage="Server 'My new server' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Staging server --%>
    <cms:APIExamplePanel ID="pnlDeleteStagingServer" runat="server" GroupingText="Staging server">
        <cms:APIExample ID="apiDeleteTasks" runat="server" ButtonText="Delete tasks" APIExampleType="CleanUpMain" InfoMessage="The tasks were deleted." ErrorMessage="Server 'My new server' was not found." />
        <cms:APIExample ID="apiDeleteStagingServer" runat="server" ButtonText="Delete server" APIExampleType="CleanUpMain" InfoMessage="Server 'My new server' and all its dependencies were deleted." ErrorMessage="Server 'My new server' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
