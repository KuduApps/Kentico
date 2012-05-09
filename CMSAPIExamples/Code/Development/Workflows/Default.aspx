<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Development_Workflows_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Workflow --%>
    <cms:APIExamplePanel ID="pnlCreateWorkflow" runat="server" GroupingText="Workflow">
        <cms:APIExample ID="apiCreateWorkflow" runat="server" ButtonText="Create workflow" InfoMessage="Workflow 'My new workflow' was created." />
        <cms:APIExample ID="apiGetAndUpdateWorkflow" runat="server" ButtonText="Get and update workflow" APIExampleType="ManageAdditional" InfoMessage="Workflow 'My new workflow' was updated." ErrorMessage="Workflow 'My new workflow' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateWorkflows" runat="server" ButtonText="Get and bulk update workflows" APIExampleType="ManageAdditional" InfoMessage="All workflows matching the condition were updated." ErrorMessage="Workflows matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Workflow step --%>
    <cms:APIExamplePanel ID="pnlCreateStep" runat="server" GroupingText="Workflow step">
        <cms:APIExample ID="apiCreateWorkflowStep" runat="server" ButtonText="Create workflow step" InfoMessage="Workflow step 'My new workflow step' was created." ErrorMessage="Workflow 'My new workflow' was not found." />
        <cms:APIExample ID="apiAddRoleToStep" runat="server" ButtonText="Add role to step" APIExampleType="ManageAdditional" InfoMessage="Role 'CMS Editors' was assigned to the step." ErrorMessage="Workflow 'My new workflow' was not found." />
    </cms:APIExamplePanel>
    <%-- Workflow scope --%>
    <cms:APIExamplePanel ID="pnlCreateWorkflowScope" runat="server" GroupingText="Workflow scope">
        <cms:APIExample ID="apiCreateWorkflowScope" runat="server" ButtonText="Create scope" InfoMessage="Scope 'My new scope' was created." ErrorMessage="Workflow 'My new workflow' was not found" />
        <cms:APIExample ID="apiGetAndUpdateWorkflowScope" runat="server" ButtonText="Get and update scope" APIExampleType="ManageAdditional" InfoMessage="Scope 'My new scope' was updated." ErrorMessage="The workflow 'My new workflow' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Workflow scope --%>
    <cms:APIExamplePanel ID="pnlDeleteWorkflowScope" runat="server" GroupingText="Workflow scope">
        <cms:APIExample ID="apiDeleteWorkflowScope" runat="server" ButtonText="Delete scope" APIExampleType="CleanUpMain" InfoMessage="The workflow scope was deleted." ErrorMessage="The scope was not found." />
    </cms:APIExamplePanel>
    <%-- Workflow step --%>
    <cms:APIExamplePanel ID="pnlDeleteStep" runat="server" GroupingText="Workflow step">
        <cms:APIExample ID="apiRemoveRoleFromStep" runat="server" ButtonText="Remove role from step" APIExampleType="CleanUpMain" InfoMessage="Role 'CMS Editors' was removed from the step." ErrorMessage="Workflow 'My new workflow' was not found." />
        <cms:APIExample ID="apiDeleteWorkflowStep" runat="server" ButtonText="Delete workflow step" APIExampleType="CleanUpMain" InfoMessage="Step 'My new workflow step' was deleted." ErrorMessage="Workflow 'My new workflow' was not found." />
    </cms:APIExamplePanel>
    <%-- Workflow --%>
    <cms:APIExamplePanel ID="pnlDeleteWorkflow" runat="server" GroupingText="Workflow">
        <cms:APIExample ID="apiDeleteWorkflow" runat="server" ButtonText="Delete workflow" APIExampleType="CleanUpMain" InfoMessage="Workflow 'My new workflow' was deleted." ErrorMessage="Workflow 'My new workflow' was not found." />
    </cms:APIExamplePanel>
</asp:Content>

