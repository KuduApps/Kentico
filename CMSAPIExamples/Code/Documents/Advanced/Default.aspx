<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Documents_Advanced_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Example preparation --%>
    <cms:APIExamplePanel ID="pnlPreparation" runat="server" GroupingText="Example preparation">
        <cms:APIExample ID="apiCreateDocumentStructure" runat="server" ButtonText="Create document structure" InfoMessage="Documents prepared successfully." ErrorMessage="Site root node not found." />
    </cms:APIExamplePanel>

    <%-- Organizing docuemnts --%>
    <cms:APIExamplePanel ID="pnlCreateDocument" runat="server" GroupingText="Organizing documents">
        <cms:APIExample ID="apiMoveDocumentUp" runat="server" APIExampleType="ManageAdditional" ButtonText="Move document up" InfoMessage="Document moved up." ErrorMessage="Document not found." />
        <cms:APIExample ID="apiMoveDocumentDown" runat="server" APIExampleType="ManageAdditional" ButtonText="Move document down" InfoMessage="Document moved down." ErrorMessage="Document not found." />
        <cms:APIExample ID="apiSortDocumentsAlphabetically" runat="server" APIExampleType="ManageAdditional" ButtonText="Sort documents alphabetically" InfoMessage="Documents sorted from A to Z." ErrorMessage="API Example folder not found." />
        <cms:APIExample ID="apiSortDocumentsByDate" runat="server" APIExampleType="ManageAdditional" ButtonText="Sort documents by date" InfoMessage="Documents sorted from oldest to newest." ErrorMessage="API Example folder not found." />
    </cms:APIExamplePanel>
    
    <%-- Recycle bin --%>
    <cms:APIExamplePanel ID="pnlRecycleBin" runat="server" GroupingText="Recycle bin">
        <cms:APIExample ID="apiMoveToRecycleBin" runat="server" ButtonText="Move document to recycle bin" InfoMessage="Document moved to the recycle bin." ErrorMessage="Document not found." />
        <cms:APIExample ID="apiRestoreFromRecycleBin" runat="server" ButtonText="Restore document" InfoMessage="Document restored successfully." ErrorMessage="Document not found in the recycle bin." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Deleting documents --%>
    <cms:APIExamplePanel ID="pnlDeleleDocumentStructure" runat="server" GroupingText="Document structure">
        <cms:APIExample ID="apiDeleteDocumentStructure" runat="server" ButtonText="Delete document structure" APIExampleType="CleanUpMain" InfoMessage="Document structure successfully deleted." />
    </cms:APIExamplePanel>
</asp:Content>
