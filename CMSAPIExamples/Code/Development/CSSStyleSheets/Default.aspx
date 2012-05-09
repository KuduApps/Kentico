<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSAPIExamples_Code_Development_CSSStyleSheets_Default" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Css stylesheet --%>
    <cms:APIExamplePanel ID="pnlCreateCssStylesheet" runat="server" GroupingText="Css stylesheet">
        <cms:APIExample ID="apiCreateCssStylesheet" runat="server" ButtonText="Create stylesheet" InfoMessage="Stylesheet 'My new stylesheet' was created." />
        <cms:APIExample ID="apiGetAndUpdateCssStylesheet" runat="server" ButtonText="Get and update stylesheet" APIExampleType="ManageAdditional" InfoMessage="Stylesheet 'My new stylesheet' was updated." ErrorMessage="Stylesheet 'My new stylesheet' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateCssStylesheets" runat="server" ButtonText="Get and bulk update stylesheets" APIExampleType="ManageAdditional" InfoMessage="All stylesheets matching the condition were updated." ErrorMessage="Stylesheets matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Css stylesheet on site --%>
    <cms:APIExamplePanel ID="pnlAddCssStylesheetToSite" runat="server" GroupingText="Css stylesheet on site">
        <cms:APIExample ID="apiAddCssStylesheetToSite" runat="server" ButtonText="Add stylesheet to site" APIExampleType="ManageAdditional" InfoMessage="Stylesheet 'My new stylesheet' was added to site." ErrorMessage="Stylesheet 'My new stylesheet' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Css stylesheet on site --%>
    <cms:APIExamplePanel ID="pnlRemoveCssStylesheetFromSite" runat="server" GroupingText="Css stylesheet on site">
        <cms:APIExample ID="apiRemoveCssStylesheetFromSite" runat="server" ButtonText="Remove stylesheet from site" APIExampleType="CleanUpMain" InfoMessage="Stylesheet 'My new stylesheet' was removed from site." ErrorMessage="Stylesheet 'My new stylesheet' was not found." />
    </cms:APIExamplePanel>
    <%-- Css stylesheet --%>
    <cms:APIExamplePanel ID="pnlDeleteCssStylesheet" runat="server" GroupingText="Css stylesheet">
        <cms:APIExample ID="apiDeleteCssStylesheet" runat="server" ButtonText="Delete stylesheet" APIExampleType="CleanUpMain" InfoMessage="Stylesheet 'My new stylesheet' and all its dependencies were deleted." ErrorMessage="Stylesheet 'My new stylesheet' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
