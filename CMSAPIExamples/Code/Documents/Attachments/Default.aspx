<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Documents_Attachments_Default"
    CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection"
    TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Preparation --%>
    <cms:APIExamplePanel ID="pnlPreparation" runat="server" GroupingText="Preparation">
        <cms:APIExample ID="apiCreateExampleDocument" runat="server" ButtonText="Create example document"
            InfoMessage="Example document was created." ErrorMessage="Site root not found." />
    </cms:APIExamplePanel>
    <%-- Inserting attachments --%>
    <cms:APIExamplePanel ID="pnlInsertAttachment" runat="server" GroupingText="Inserting attachments">
        <cms:APIExample ID="apiInsertUnsortedAttachment" runat="server" ButtonText="Insert unsorted attachment"
            InfoMessage="An unsorted attachment was inserted." ErrorMessage="Document was not found." />
        <cms:APIExample ID="apiInsertFieldAttachment" runat="server" ButtonText="Insert field attachment"
            InfoMessage="An attachment was inserted to the MenuItemTeaserImage field." ErrorMessage="An error occurred while inserting the attachment." />
    </cms:APIExamplePanel>
    <%-- Managing attachments --%>
    <cms:APIExamplePanel ID="pnlManageAttachments" runat="server" GroupingText="Managing attachments">
        <cms:APIExample ID="apiMoveAttachmentDown" runat="server" ButtonText="Move attachment down"
            APIExampleType="ManageAdditional" InfoMessage="Attachment was moved down." ErrorMessage="Document was not found." />
        <cms:APIExample ID="apiMoveAttachmentUp" runat="server" ButtonText="Move attachment up"
            APIExampleType="ManageAdditional" InfoMessage="Attachment was moved up." ErrorMessage="Document was not found." />
        <cms:APIExample ID="apiEditMetadata" runat="server" ButtonText="Edit attachment metadata"
            APIExampleType="ManageAdditional" InfoMessage="Attachment metadata was modified."
            ErrorMessage="Document was not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Document alias --%>
    <cms:APIExamplePanel ID="pnlDeleteDocumentAlias" runat="server" GroupingText="Document alias">
        <cms:APIExample ID="apiDeleteAttachments" runat="server" ButtonText="Delete attachments"
            APIExampleType="CleanUpMain" InfoMessage="All attachments have been deleted."
            ErrorMessage="Document was not found." />
        <cms:APIExample ID="apiDeleteExampleDocument" runat="server" ButtonText="Delete example document"
            APIExampleType="CleanUpMain" InfoMessage="The document has been deleted." ErrorMessage="Document was not found." />
    </cms:APIExamplePanel>
</asp:Content>
