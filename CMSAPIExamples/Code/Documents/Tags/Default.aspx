<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSAPIExamples_Code_Documents_Tags_Default" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Tag group --%>
    <cms:APIExamplePanel ID="pnlCreateTagGroup" runat="server" GroupingText="Tag group">
        <cms:APIExample ID="apiCreateTagGroup" runat="server" ButtonText="Create group" InfoMessage="Group 'My new group' was created." />
        <cms:APIExample ID="apiGetAndUpdateTagGroup" runat="server" ButtonText="Get and update group" APIExampleType="ManageAdditional" InfoMessage="Group 'My new group' was updated." ErrorMessage="Group 'My new group' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateTagGroups" runat="server" ButtonText="Get and bulk update groups" APIExampleType="ManageAdditional" InfoMessage="All groups matching the condition were updated." ErrorMessage="Groups matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Tag --%>
    <cms:APIExamplePanel ID="pnlCreateTag" runat="server" GroupingText="Tag on document">
        <cms:APIExample ID="apiAddTagToDocument" runat="server" ButtonText="Add tag to document" InfoMessage="Tag 'My new tag' was added to document." ErrorMessage="Tag group 'My new group' was not found." />
        <cms:APIExample ID="apiGetDocumentAndUpdateItsTags" runat="server" ButtonText="Get document and update its tags" APIExampleType="ManageAdditional" InfoMessage="Tag 'My new tag' was updated." ErrorMessage="Tag 'My new tag' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Tag --%>
    <cms:APIExamplePanel ID="pnlRemoveTagFromDocument" runat="server" GroupingText="Tag">
        <cms:APIExample ID="apiRemoveTagFromDocument" runat="server" ButtonText="Remove tag from document" APIExampleType="CleanUpMain" InfoMessage="Tag 'My new tag' was removed from document." ErrorMessage="Tag 'My new tag', root document or their relationship were not found." />
    </cms:APIExamplePanel>
    <%-- Tag group --%>
    <cms:APIExamplePanel ID="pnlDeleteTagGroup" runat="server" GroupingText="Tag group">
        <cms:APIExample ID="apiDeleteTagGroup" runat="server" ButtonText="Delete group" APIExampleType="CleanUpMain" InfoMessage="Group 'My new group' and all its dependencies were deleted." ErrorMessage="Group 'My new group' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
