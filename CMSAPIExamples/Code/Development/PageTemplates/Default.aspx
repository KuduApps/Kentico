<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Development_PageTemplates_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
     <%-- Page template category --%>
    <cms:APIExamplePanel ID="pnlCreatePageTemplateCategory" runat="server" GroupingText="Page template category">
        <cms:APIExample ID="apiCreatePageTemplateCategory" runat="server" ButtonText="Create category" InfoMessage="Category 'My new category' was created." />
        <cms:APIExample ID="apiGetAndUpdatePageTemplateCategory" runat="server" ButtonText="Get and update category" APIExampleType="ManageAdditional" InfoMessage="Category 'My new category' was updated." ErrorMessage="Category 'My new category' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdatePageTemplateCategories" runat="server" ButtonText="Get and bulk update categories" APIExampleType="ManageAdditional" InfoMessage="All categories matching the condition were updated." ErrorMessage="Categories matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Page template --%>
    <cms:APIExamplePanel ID="pnlCreatePageTemplate" runat="server" GroupingText="Page template">
        <cms:APIExample ID="apiCreatePageTemplate" runat="server" ButtonText="Create template" InfoMessage="Template 'My new template' was created." ErrorMessage="Category 'My new category' was not found."/>
        <cms:APIExample ID="apiGetAndUpdatePageTemplate" runat="server" ButtonText="Get and update template" APIExampleType="ManageAdditional" InfoMessage="Template 'My new template' was updated." ErrorMessage="Template 'My new template' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdatePageTemplates" runat="server" ButtonText="Get and bulk update templates" APIExampleType="ManageAdditional" InfoMessage="All templates matching the condition were updated." ErrorMessage="Templates matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Page template on site --%>
    <cms:APIExamplePanel ID="pnlAddPageTemplateToSite" runat="server" GroupingText="Page template on site">
        <cms:APIExample ID="apiAddPageTemplateToSite" runat="server" ButtonText="Add template to site" APIExampleType="ManageAdditional" InfoMessage="Template 'My new template' was added to site." ErrorMessage="Template 'My new template' was not found." />
    </cms:APIExamplePanel>
    <%-- Page template scope --%>
    <cms:APIExamplePanel ID="pnlCreatePageTemplateScope" runat="server" GroupingText="Page template scope">
        <cms:APIExample ID="apiCreatePageTemplateScope" runat="server" ButtonText="Create scope" InfoMessage="Scope 'My new scope' was created." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Page template scope --%>
    <cms:APIExamplePanel ID="pnlDeletePageTemplateScope" runat="server" GroupingText="Page template scope">
        <cms:APIExample ID="apiDeletePageTemplateScope" runat="server" ButtonText="Delete scope" APIExampleType="CleanUpMain" InfoMessage="Scope 'My new scope' and all its dependencies were deleted." ErrorMessage="Scope 'My new scope' was not found." />
    </cms:APIExamplePanel>
    <%-- Page template on site --%>
    <cms:APIExamplePanel ID="pnlRemovePageTemplateFromSite" runat="server" GroupingText="Page template on site">
        <cms:APIExample ID="apiRemovePageTemplateFromSite" runat="server" ButtonText="Remove template from site" APIExampleType="CleanUpMain" InfoMessage="Template 'My new template' was removed from site." ErrorMessage="Template 'My new template' was not found." />
    </cms:APIExamplePanel>
    <%-- Page template --%>
    <cms:APIExamplePanel ID="pnlDeletePageTemplate" runat="server" GroupingText="Page template">
        <cms:APIExample ID="apiDeletePageTemplate" runat="server" ButtonText="Delete template" APIExampleType="CleanUpMain" InfoMessage="Template 'My new template' and all its dependencies were deleted." ErrorMessage="Template 'My new template' was not found." />
    </cms:APIExamplePanel>
     <%-- Page template category --%>
    <cms:APIExamplePanel ID="pnlDeletePageTemplateCategory" runat="server" GroupingText="Page template category">
        <cms:APIExample ID="apiDeletePageTemplateCategory" runat="server" ButtonText="Delete category" APIExampleType="CleanUpMain" InfoMessage="Category 'My new category' and all its dependencies were deleted." ErrorMessage="Category 'My new category' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
