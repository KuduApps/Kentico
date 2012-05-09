<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Administration_WebFarm_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Web farm server --%>
    <cms:APIExamplePanel ID="pnlCreateWebFarmServer" runat="server" GroupingText="Web farm server">
        <cms:APIExample ID="apiCreateWebFarmServer" runat="server" ButtonText="Create server" InfoMessage="Server 'My new server' was created." />
        <cms:APIExample ID="apiGetAndUpdateWebFarmServer" runat="server" ButtonText="Get and update server" APIExampleType="ManageAdditional" InfoMessage="Server 'My new server' was updated." ErrorMessage="Server 'My new server' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateWebFarmServers" runat="server" ButtonText="Get and bulk update servers" APIExampleType="ManageAdditional" InfoMessage="All servers matching the condition were updated." ErrorMessage="Servers matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Web farm task --%>
    <cms:APIExamplePanel ID="pnlWebFarmTasks" runat="server" GroupingText="Web farm tasks">
        <cms:APIExample ID="apiCreateTask" runat="server" ButtonText="Create task" APIExampleType="ManageAdditional" InfoMessage="Task was created." />
        <cms:APIExample ID="apiRunMyTasks" runat="server" ButtonText="Run my tasks" APIExampleType="ManageAdditional" InfoMessage="All servers matching the condition were updated." ErrorMessage="Servers matching the condition were not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Web farm server --%>
    <cms:APIExamplePanel ID="pnlDeleteWebFarmServer" runat="server" GroupingText="Web farm server">
        <cms:APIExample ID="apiDeleteWebFarmServer" runat="server" ButtonText="Delete server" APIExampleType="CleanUpMain" InfoMessage="Server 'My new server' and all its dependencies were deleted." ErrorMessage="Server 'My new server' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
