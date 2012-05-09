<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Documents_Workflow_Basics_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Creating documents --%>
    <cms:APIExamplePanel ID="pnlCreateDocument" runat="server" GroupingText="Creating documents">
        <cms:APIExample ID="apiCreateExampleObjects" runat="server" ButtonText="Create example objects" InfoMessage="Objects needed for the example were created." ErrorMessage="Site root not found." />
        <cms:APIExample ID="apiCreateDocument" runat="server" ButtonText="Create document" InfoMessage="Document was created." ErrorMessage="Example folder not found." />
        <cms:APIExample ID="apiCreateNewCultureVersion" runat="server" ButtonText="Create new culture version" InfoMessage="New culture version of the document was created." ErrorMessage="Document 'My new document' was not found." />
        <cms:APIExample ID="apiCreateLinkedDocument" runat="server" ButtonText="Create linked document" InfoMessage="Link to the document was created." ErrorMessage="Site root not found." />
    </cms:APIExamplePanel>
    <%-- Managing documents --%>
    <cms:APIExamplePanel ID="pnlEditDocument" runat="server" GroupingText="Editing documents">
        <cms:APIExample ID="apiGetAndUpdateDocuments" runat="server" ButtonText="Get and update documents" APIExampleType="ManageAdditional" InfoMessage="All menu items under the API Example folder were updated." ErrorMessage="No documents were found." />
        <cms:APIExample ID="apiCopyDocument" runat="server" ButtonText="Copy document" APIExampleType="ManageAdditional" InfoMessage="Document 'My new document' successfully copied under 'API Example/Source'." ErrorMessage="The document to be copied or the target document could not be found." />
        <cms:APIExample ID="apiMoveDocument" runat="server" ButtonText="Move document" APIExampleType="ManageAdditional" InfoMessage="Document '/API Example/Source/My new document' successfully moved under 'API Example/Target'." ErrorMessage="The document to be moved or the target document could not be found." />    
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <cms:APIExamplePanel ID="pnlCleanUp" runat="server" GroupingText="Cleanup">
        <cms:APIExample ID="apiDeleteDocuments" runat="server" ButtonText="Delete documents" APIExampleType="CleanUpMain" InfoMessage="All example documents were deleted." ErrorMessage="API example folder not found." />
        <cms:APIExample ID="apiDeleteObjects" runat="server" ButtonText="Delete objects" APIExampleType="CleanUpMain" InfoMessage="All example objects were deleted." ErrorMessage="" />
    </cms:APIExamplePanel>
</asp:Content>