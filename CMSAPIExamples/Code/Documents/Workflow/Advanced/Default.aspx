<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="True" Inherits="CMSAPIExamples_Code_Documents_Workflow_Advanced_Default"
    CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection"
    TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Preparation --%>
    <cms:APIExamplePanel ID="pnlPreparation" runat="server" GroupingText="Creating documents">
        <cms:APIExample ID="apiCreateExampleObjects" runat="server" ButtonText="Create example objects"
            InfoMessage="Document and objects needed for the example were created." ErrorMessage="Site root not found." />
    </cms:APIExamplePanel>
    <%-- Editing document --%>
    <cms:APIExamplePanel ID="pnlEditDocument" runat="server" GroupingText="Editing documents">
        <cms:APIExample ID="apiCheckOut" runat="server" ButtonText="Check out document" APIExampleType="ManageAdditional"
            InfoMessage="Document was checked out." ErrorMessage="The document was not found." />
        <cms:APIExample ID="apiEditDocument" runat="server" ButtonText="Edit document" APIExampleType="ManageAdditional"
            InfoMessage="Document version was modified." ErrorMessage="The document was not founf." />
        <cms:APIExample ID="apiCheckIn" runat="server" ButtonText="Check in document" APIExampleType="ManageAdditional"
            InfoMessage="Document was checked in." ErrorMessage="The document was not found." />
        <cms:APIExample ID="apiUndoCheckout" runat="server" ButtonText="Undo check-out" APIExampleType="ManageAdditional"
            InfoMessage="All changes were discarded and the document was checked in." ErrorMessage="The document was not found." />
    </cms:APIExamplePanel>
    <%-- Workflow process --%>
    <cms:APIExamplePanel ID="pnlWorkflowProcess" runat="server" GroupingText="Workflow process">
        <cms:APIExample ID="apiMoveToNextStep" runat="server" ButtonText="Move to next step"
            APIExampleType="ManageAdditional" InfoMessage="The document was moved to next step."
            ErrorMessage="The document was not found." />
        <cms:APIExample ID="apiMoveToPreviousStep" runat="server" ButtonText="Move to previous step"
            APIExampleType="ManageAdditional" InfoMessage="The document was moved to previous step."
            ErrorMessage="The document was not found." />
        <cms:APIExample ID="apiArchiveDocument" runat="server" ButtonText="Archive document"
            APIExampleType="ManageAdditional" InfoMessage="The document was archived." ErrorMessage="The document was not found." />
    </cms:APIExamplePanel>
    <%-- Versions --%>
    <cms:APIExamplePanel ID="pnlVersioning" runat="server" GroupingText="Versioning">
        <cms:APIExample ID="apiRollbackVersion" runat="server" ButtonText="Rollback version"
            APIExampleType="ManageAdditional" InfoMessage="The document was rolled back to latest version."
            ErrorMessage="The document was not found." />
        <cms:APIExample ID="apiDeleteVersion" runat="server" ButtonText="Delete version"
            APIExampleType="ManageAdditional" InfoMessage="Document oldest version was deleted."
            ErrorMessage="The document was not found." />
        <cms:APIExample ID="apiDestroyHistory" runat="server" ButtonText="Destroy version history"
            APIExampleType="ManageAdditional" InfoMessage="Document version history was deleted."
            ErrorMessage="The document was not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <cms:APIExamplePanel ID="pnlCleanUp" runat="server" GroupingText="Cleanup">
        <cms:APIExample ID="apiDeleteExampleObjects" runat="server" ButtonText="Delete example objects"
            APIExampleType="CleanUpMain" InfoMessage="All example objects were deleted." />
    </cms:APIExamplePanel>
</asp:Content>
