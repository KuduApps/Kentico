<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSAPIExamples_Code_Documents_Categories_Default" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Category --%>
    <cms:APIExamplePanel ID="pnlCreateCategory" runat="server" GroupingText="Category">
        <cms:APIExample ID="apiCreateCategory" runat="server" ButtonText="Create category" InfoMessage="Category 'My new category' was created." />
        <cms:APIExample ID="apiGetAndUpdateCategory" runat="server" ButtonText="Get and update category" APIExampleType="ManageAdditional" InfoMessage="Category 'My new category' was updated." ErrorMessage="Category 'My new category' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateCategories" runat="server" ButtonText="Get and bulk update categories" APIExampleType="ManageAdditional" InfoMessage="All categories matching the condition were updated." ErrorMessage="Categories matching the condition were not found." />
    </cms:APIExamplePanel>
    
    <%-- Document in category --%>
    <cms:APIExamplePanel ID="pnlAddDocumentToCategory" runat="server" GroupingText="Document in category">
        <cms:APIExample ID="apiAddDocumentToCategory" runat="server" ButtonText="Add document to category" APIExampleType="ManageAdditional" InfoMessage="Root document was assigned to category 'My new category'." ErrorMessage="Category 'My new category' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Document in category --%>
    <cms:APIExamplePanel ID="pnlRemoveDocumentFromCategory" runat="server" GroupingText="Document in category">
        <cms:APIExample ID="apiRemoveDocumentFromCategory" runat="server" ButtonText="Remove document from category" APIExampleType="CleanUpMain" InfoMessage="Root document was removed from category 'My new category'." ErrorMessage="Category 'My new category' was not found." />
    </cms:APIExamplePanel>
    
    <%-- Category --%>
    <cms:APIExamplePanel ID="pnlDeleteCategory" runat="server" GroupingText="Category">
        <cms:APIExample ID="apiDeleteCategory" runat="server" ButtonText="Delete category" APIExampleType="CleanUpMain" InfoMessage="Category 'My new category' and all its dependencies were deleted." ErrorMessage="Category 'My new category' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
