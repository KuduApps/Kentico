<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Administration_SmartSearch_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Search index --%>
    <cms:APIExamplePanel ID="pnlCreateSearchIndex" runat="server" GroupingText="Search index">
        <cms:APIExample ID="apiCreateSearchIndex" runat="server" ButtonText="Create index" InfoMessage="Index 'My new index' was created." />
        <cms:APIExample ID="apiCreateIndexSettings" runat="server" ButtonText="Create index settings" InfoMessage="Index settings were created." ErrorMessage="Index 'My new index' was not found."/>
        <cms:APIExample ID="apiGetAndUpdateSearchIndex" runat="server" ButtonText="Get and update index" APIExampleType="ManageAdditional" InfoMessage="Index 'My new index' was updated." ErrorMessage="Index 'My new index' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateSearchIndexes" runat="server" ButtonText="Get and bulk update indexes" APIExampleType="ManageAdditional" InfoMessage="All indexes matching the condition were updated." ErrorMessage="Indexes matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Search index on site --%>
    <cms:APIExamplePanel ID="pnlAddSearchIndexToSite" runat="server" GroupingText="Search index on site">
        <cms:APIExample ID="apiAddSearchIndexToSite" runat="server" ButtonText="Add index to site" APIExampleType="ManageAdditional" InfoMessage="Index 'My new index' was added to site." ErrorMessage="Index 'My new index' was not found." />
    </cms:APIExamplePanel>
    <%-- Culture on search index --%>
    <cms:APIExamplePanel ID="pnlCultureOnIndex" runat="server" GroupingText="Culture on search index">
        <cms:APIExample ID="apiAddCultureToSearchIndex" runat="server" ButtonText="Add culture to index" APIExampleType="ManageAdditional" InfoMessage="Culture was added to index 'My new index'." ErrorMessage="Index 'My new index' was not found." />
    </cms:APIExamplePanel>
    <%-- Search actions --%>
    <cms:APIExamplePanel ID="pnlSearchActions" runat="server" GroupingText="Search actions">
        <cms:APIExample ID="apiRebuildIndex" runat="server" ButtonText="Rebuild index" APIExampleType="ManageAdditional" InfoMessage="Smart search index 'My new index' was rebuilded." ErrorMessage="Index 'My new index' was not found." />
        <cms:APIExample ID="apiSearchText" runat="server" ButtonText="Search text" APIExampleType="ManageAdditional" InfoMessage="Sample text 'home' was found." ErrorMessage="Index 'My new index' or sample text 'home' was not found." />
        <cms:APIExample ID="apiUpdateIndex" runat="server" ButtonText="Create update task" APIExampleType="ManageAdditional" InfoMessage="Search index 'My new index' was updated." ErrorMessage="Index 'My new index' or sample text 'test' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Culture on search index --%>
    <cms:APIExamplePanel ID="pnlRemoveCultureFromIndex" runat="server" GroupingText="Culture on search index">
        <cms:APIExample ID="apiRemoveCultureFromSearchIndex" runat="server" ButtonText="Remove culture from index" APIExampleType="CleanUpMain" InfoMessage="Culture was removed from search index 'My new index'." ErrorMessage="Index 'My new index' was not found." />
    </cms:APIExamplePanel>
    <%-- Search index on site --%>
    <cms:APIExamplePanel ID="pnlRemoveSearchIndexFromSite" runat="server" GroupingText="Search index on site">
        <cms:APIExample ID="apiRemoveSearchIndexFromSite" runat="server" ButtonText="Remove index from site" APIExampleType="CleanUpMain" InfoMessage="Index 'My new index' was removed from site." ErrorMessage="Index 'My new index' was not found." />
    </cms:APIExamplePanel>
    <%-- Search index --%>
    <cms:APIExamplePanel ID="pnlDeleteSearchIndex" runat="server" GroupingText="Search index">
        <cms:APIExample ID="apiDeleteSearchIndex" runat="server" ButtonText="Delete index" APIExampleType="CleanUpMain" InfoMessage="Index 'My new index' and all its dependencies were deleted." ErrorMessage="Index 'My new index' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
