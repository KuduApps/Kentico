<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Documents_Basics_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Creating documents --%>
    <cms:APIExamplePanel ID="pnlCreateDocument" runat="server" GroupingText="Creating documents">
        <cms:APIExample ID="apiCreateDocumentStructure" runat="server" ButtonText="Create document structure" InfoMessage="Document structure for the API example created successfully." ErrorMessage="Site root node not found." />
        <cms:APIExample ID="apiCreateDocument" runat="server" ButtonText="Create document" InfoMessage="Document 'My new document' created successfully." ErrorMessage="Parent document not found." />
        <cms:APIExample ID="apiCreateNewCultureVersion" runat="server" ButtonText="Create new culture version" InfoMessage="Document 'My new document' translated successfully." ErrorMessage="Document 'My new document' was not found." />
        <cms:APIExample ID="apiCreateLinkedDocument" runat="server" ButtonText="Create linked document" InfoMessage="Link to document 'My new document' was created successfully." ErrorMessage="Parent document not found." />
    </cms:APIExamplePanel>
    <%-- Managing documents --%>
    <cms:APIExamplePanel ID="pnlManageDocuments" runat="server" GroupingText="Managing documents">
        <cms:APIExample ID="apiGetAndUpdateDocuments" runat="server" ButtonText="Get and update documents" APIExampleType="ManageAdditional" InfoMessage="All menu items under the API Example folder were updated." ErrorMessage="No documents were found." />
        <cms:APIExample ID="apiCopyDocument" runat="server" ButtonText="Copy document" APIExampleType="ManageAdditional" InfoMessage="Document 'My new document' successfully copied under 'API Example/Source'." ErrorMessage="The document to be copied or the target document could not be found." />
        <cms:APIExample ID="apiMoveDocument" runat="server" ButtonText="Move document" APIExampleType="ManageAdditional" InfoMessage="Document '/API Example/Source/My new document' successfully moved under 'API Example/Target'." ErrorMessage="The document to be moved or the target document could not be found." />
    </cms:APIExamplePanel>
    
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Deleting documents --%>
    <cms:APIExamplePanel ID="pnlDeleleDocument" runat="server" GroupingText="Documents">
        <cms:APIExample ID="apiDeleteLinkedDocuments" runat="server" ButtonText="Delete linked documents" APIExampleType="CleanUpMain" InfoMessage="All links to document 'My new document' deleted successfully." ErrorMessage="The document could not be found." />
        <cms:APIExample ID="apiDeleteCultureVersion" runat="server" ButtonText="Delete culture version" APIExampleType="CleanUpMain" InfoMessage="Culture version of the document deleted successfully." ErrorMessage="The document could not be found." />
        <cms:APIExample ID="apiDeleteDocument" runat="server" ButtonText="Delete document" APIExampleType="CleanUpMain" InfoMessage="Document 'My new document' deleted successfully." ErrorMessage="The document could not be found." />
        <cms:APIExample ID="apiDeleteDocumentStructure" runat="server" ButtonText="Delete document structure" APIExampleType="CleanUpMain" InfoMessage="The document structure deleted successfully." />
    </cms:APIExamplePanel>
</asp:Content>
