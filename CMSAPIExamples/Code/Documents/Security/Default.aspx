<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSAPIExamples_Code_Documents_Security_Default" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Creating documents --%>
    <cms:APIExamplePanel ID="pnlCreateDocument" runat="server" GroupingText="Creating documents">
        <cms:APIExample ID="apiCreateDocumentStructure" runat="server" ButtonText="Create document structure" InfoMessage="Document structure for the API example created successfully." ErrorMessage="Site root node not found." />
    </cms:APIExamplePanel>
    <%-- Setting document level permissions --%>
    <cms:APIExamplePanel ID="pnlSetPermissions" runat="server" GroupingText="Setting document level permissions">
        <cms:APIExample ID="apiSetUserPermissions" runat="server" ButtonText="Set user permissions" InfoMessage="The 'Modify permissions' permission for document 'API Example' was successfully granted to user 'CMSEditor'." ErrorMessage="Document 'API Example' or user 'CMSEditor' not found." />
        <cms:APIExample ID="apiSetRolePermissions" runat="server" ButtonText="Set role permissions" InfoMessage="The 'Modify' permission for document 'API Example' was successfully granted to role 'CMSEditor'." ErrorMessage="Document 'API Example' or role 'CMSEditor' not found." />
    </cms:APIExamplePanel>
    <%-- Permission inheritance --%>
    <cms:APIExamplePanel ID="pnlPermissionInheritance" runat="server" GroupingText="Permission inheritance">
        <cms:APIExample ID="apiBreakPermissionInheritance" runat="server" APIExampleType="ManageAdditional" ButtonText="Break inheritance" InfoMessage="Inheritance of permissions on document 'API Example subpage' broken successfully." ErrorMessage="Document 'API Example subpage' not found." />
        <cms:APIExample ID="apiRestorePermissionInheritance" runat="server" APIExampleType="ManageAdditional" ButtonText="Restore inheritance" InfoMessage="Inheritance of permissions on document 'API Example subpage' restored successfully." ErrorMessage="Document 'API Example subpage' not found." />
    </cms:APIExamplePanel>
    <%-- Checking permissions --%>
    <cms:APIExamplePanel ID="pnlCheckPermissions" runat="server" GroupingText="Checking permissions">
        <cms:APIExample ID="apiCheckContentModulePermissions" runat="server" ButtonText="Check module permissions" APIExampleType="ManageAdditional" InfoMessage="User 'CMSEditor' is allowed to read module 'Content'." ErrorMessage="User 'CMSEditor' not found." />
        <cms:APIExample ID="apiCheckDocTypePermissions" runat="server" ButtonText="Check document type permissions" APIExampleType="ManageAdditional" InfoMessage="User 'CMSEditor' is allowed to read document type 'Menu item'." ErrorMessage="User 'CMSEditor' not found." />
        <cms:APIExample ID="apiCheckDocumentPermissions" runat="server" ButtonText="Check document permissions" APIExampleType="ManageAdditional" InfoMessage="User 'CMSEditor' is allowed to modify permissions for document 'API Example'." ErrorMessage="Document 'API Example' or user 'CMSEditor' not found." />
        <cms:APIExample ID="apiFilterDataSet" runat="server" ButtonText="Filter data set" APIExampleType="ManageAdditional" InfoMessage="Data set with all documents filtered successfully by permission 'Modify permissions' for user 'CMSEditor'. Permission inheritance broken for filtered items." ErrorMessage="User 'CMSEditor' not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Deleting document level permissions --%>
    <cms:APIExamplePanel ID="pnlDelelePermissions" runat="server" GroupingText="Deleting document level permissions">
        <cms:APIExample ID="apiDeletePermissions" runat="server" APIExampleType="CleanUpMain" ButtonText="Delete permissions" InfoMessage="The document level permissions deleted successfully." ErrorMessage="The document structure not found." />
    </cms:APIExamplePanel>
    <%-- Deleting documents --%>
    <cms:APIExamplePanel ID="pnlDeleleDocument" runat="server" GroupingText="Deleting documents">
        <cms:APIExample ID="apiDeleteDocumentStructure" runat="server" APIExampleType="CleanUpMain" ButtonText="Delete document structure" InfoMessage="The document structure deleted successfully." ErrorMessage="The document structure not found." />
    </cms:APIExamplePanel>
</asp:Content>
