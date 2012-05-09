<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Documents_DocumentAliases_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Document alias --%>
    <cms:APIExamplePanel ID="pnlCreateDocumentAlias" runat="server" GroupingText="Document alias">
        <cms:APIExample ID="apiCreateDocumentAlias" runat="server" ButtonText="Create alias" InfoMessage="Alias 'My new alias' was created." />
        <cms:APIExample ID="apiGetAndUpdateDocumentAlias" runat="server" ButtonText="Get and update alias" APIExampleType="ManageAdditional" InfoMessage="Alias 'My new alias' was updated." ErrorMessage="Alias 'My new alias' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateDocumentAliases" runat="server" ButtonText="Get and bulk update aliases" APIExampleType="ManageAdditional" InfoMessage="All aliases matching the condition were updated." ErrorMessage="Aliases matching the condition were not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Document alias --%>
    <cms:APIExamplePanel ID="pnlDeleteDocumentAlias" runat="server" GroupingText="Document alias">
        <cms:APIExample ID="apiDeleteDocumentAlias" runat="server" ButtonText="Delete alias" APIExampleType="CleanUpMain" InfoMessage="Alias 'My new alias' and all its dependencies were deleted." ErrorMessage="Alias 'My new alias' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
