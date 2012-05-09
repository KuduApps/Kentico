<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Documents_Relationships_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Relationship name --%>
    <cms:APIExamplePanel ID="pnlCreateRelationshipName" runat="server" GroupingText="Relationship name">
        <cms:APIExample ID="apiCreateRelationshipName" runat="server" ButtonText="Create name" InfoMessage="Relationship name 'My new relationship name' was created." />
        <cms:APIExample ID="apiGetAndUpdateRelationshipName" runat="server" ButtonText="Get and update name" APIExampleType="ManageAdditional" InfoMessage="Relationship name 'My new relationship name' was updated." ErrorMessage="Relationship name 'My new relationship name' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateRelationshipNames" runat="server" ButtonText="Get and bulk update names" APIExampleType="ManageAdditional" InfoMessage="All names matching the condition were updated." ErrorMessage="Relationship names matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Relationship name on site --%>
    <cms:APIExamplePanel ID="pnlAddRelationshipNameToSite" runat="server" GroupingText="Relationship name on site">
        <cms:APIExample ID="apiAddRelationshipNameToSite" runat="server" ButtonText="Add name to site" InfoMessage="Relationship name 'My new relationship name' was added to site." ErrorMessage="Relationship name 'My new relationship name' was not found." />
    </cms:APIExamplePanel>
    <%-- Relationship --%>
    <cms:APIExamplePanel ID="pnlCreateRelationship" runat="server" GroupingText="Documents in relationship">
        <cms:APIExample ID="apiCreateRelationship" runat="server" ButtonText="Create relationship" InfoMessage="Relationship between documents was created." ErrorMessage="Relationship name 'My new relationship name' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Relationship --%>
    <cms:APIExamplePanel ID="pnlDeleteRelationship" runat="server" GroupingText="Documents in relationship">
        <cms:APIExample ID="apiDeleteRelationship" runat="server" ButtonText="Delete relationship" APIExampleType="CleanUpMain" InfoMessage="Relationship between documents was deleted." ErrorMessage="Relationship name 'My new relationship name' was not found." />
    </cms:APIExamplePanel>
    <%-- Relationship name on site --%>
    <cms:APIExamplePanel ID="pnlRemoveRelationshipNameFromSite" runat="server" GroupingText="Relationship name on site">
        <cms:APIExample ID="apiRemoveRelationshipNameFromSite" runat="server" ButtonText="Remove name from site" APIExampleType="CleanUpMain" InfoMessage="Relationship name 'My new relationship name' was removed from site." ErrorMessage="Relationship name 'My new relationship name' was not found." />
    </cms:APIExamplePanel>
    <%-- Relationship name --%>
    <cms:APIExamplePanel ID="pnlDeleteRelationshipName" runat="server" GroupingText="Relationship name">
        <cms:APIExample ID="apiDeleteRelationshipName" runat="server" ButtonText="Delete name" APIExampleType="CleanUpMain" InfoMessage="Relationship name 'My new relationship name' and all its dependencies were deleted." ErrorMessage="Relationship name 'My new relationship name' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
