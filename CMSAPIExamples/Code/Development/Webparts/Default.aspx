<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Development_Webparts_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
     <%-- Web part category --%>
    <cms:APIExamplePanel ID="pnlCreateWebPartCategory" runat="server" GroupingText="Web part category">
        <cms:APIExample ID="apiCreateWebPartCategory" runat="server" ButtonText="Create category" InfoMessage="Category 'My new category' was created." />
        <cms:APIExample ID="apiGetAndUpdateWebPartCategory" runat="server" ButtonText="Get and update category" APIExampleType="ManageAdditional" InfoMessage="Category 'My new category' was updated." ErrorMessage="Category 'My new category' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateWebPartCategories" runat="server" ButtonText="Get and bulk update categories" APIExampleType="ManageAdditional" InfoMessage="All categories matching the condition were updated." ErrorMessage="Categories matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Web part --%>
    <cms:APIExamplePanel ID="pnlCreateWebPart" runat="server" GroupingText="Web part">
        <cms:APIExample ID="apiCreateWebPart" runat="server" ButtonText="Create web part" InfoMessage="Webpart 'My new web part' was created." ErrorMessage="Category 'My new category' was not found." />
        <cms:APIExample ID="apiGetAndUpdateWebPart" runat="server" ButtonText="Get and update web part" APIExampleType="ManageAdditional" InfoMessage="Webpart 'My new web part' was updated." ErrorMessage="Webpart 'My new web part' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateWebParts" runat="server" ButtonText="Get and bulk update web parts" APIExampleType="ManageAdditional" InfoMessage="All parts matching the condition were updated." ErrorMessage="Webparts matching the condition were not found." />
    </cms:APIExamplePanel>
     <%-- Web part layout --%>
    <cms:APIExamplePanel ID="pnlCreateWebPartLayout" runat="server" GroupingText="Web part layout"  >
        <cms:APIExample ID="apiCreateWebPartLayout" runat="server" ButtonText="Create layout" InfoMessage="Layout 'My new layout' was created."  ErrorMessage="Webpart 'My new web part' was not found." />
        <cms:APIExample ID="apiGetAndUpdateWebPartLayout" runat="server" ButtonText="Get and update layout" APIExampleType="ManageAdditional" InfoMessage="Layout 'My new layout' was updated." ErrorMessage="Layout 'My new layout' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateWebPartLayouts" runat="server" ButtonText="Get and bulk update layouts" APIExampleType="ManageAdditional" InfoMessage="All layouts matching the condition were updated." ErrorMessage="Layouts matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Web part container --%>
    <cms:APIExamplePanel ID="pnlCreateWebPartContainer" runat="server" GroupingText="Web part container">
        <cms:APIExample ID="apiCreateWebPartContainer" runat="server" ButtonText="Create container" InfoMessage="Container 'My new container' was created." />
        <cms:APIExample ID="apiGetAndUpdateWebPartContainer" runat="server" ButtonText="Get and update container" APIExampleType="ManageAdditional" InfoMessage="Container 'My new container' was updated." ErrorMessage="Container 'My new container' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateWebPartContainers" runat="server" ButtonText="Get and bulk update containers" APIExampleType="ManageAdditional" InfoMessage="All containers matching the condition were updated." ErrorMessage="Containers matching the condition were not found." />
    </cms:APIExamplePanel>
     <%-- Web part container on site --%>
    <cms:APIExamplePanel ID="pnlAddWebPartContainerToSite" runat="server" GroupingText="Web part container on site">
        <cms:APIExample ID="apiAddWebPartContainerToSite" runat="server" ButtonText="Add container to site" InfoMessage="Container 'My new container' was added to site." ErrorMessage="Container 'My new container' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
     <%-- Web part container on site --%>
    <cms:APIExamplePanel ID="pnlRemoveWebPartContainerFromSite" runat="server" GroupingText="Web part container on site">
        <cms:APIExample ID="apiRemoveWebPartContainerFromSite" runat="server" ButtonText="Remove container from site" APIExampleType="CleanUpMain" InfoMessage="Container 'My new container' was removed from site." ErrorMessage="Container 'My new container' was not found." />
    </cms:APIExamplePanel>
    <%-- Web part container --%>
    <cms:APIExamplePanel ID="pnlDeleteWebPartContainer" runat="server" GroupingText="Web part container">
        <cms:APIExample ID="apiDeleteWebPartContainer" runat="server" ButtonText="Delete container" APIExampleType="CleanUpMain" InfoMessage="Container 'My new container' and all its dependencies were deleted." ErrorMessage="Container 'My new container' was not found." />
    </cms:APIExamplePanel>
    <%-- Web part layout --%>
    <cms:APIExamplePanel ID="pnlDeleteWebPartLayout" runat="server" GroupingText="Web part layout">
        <cms:APIExample ID="apiDeleteWebPartLayout" runat="server" ButtonText="Delete layout" APIExampleType="CleanUpMain" InfoMessage="Layout 'My new layout' and all its dependencies were deleted." ErrorMessage="Layout 'My new layout' was not found." />
    </cms:APIExamplePanel>
    <%-- Web part --%>
    <cms:APIExamplePanel ID="pnlDeleteWebPart" runat="server" GroupingText="Web web part">
        <cms:APIExample ID="apiDeleteWebPart" runat="server" ButtonText="Delete web part" APIExampleType="CleanUpMain" InfoMessage="Webpart 'My new web part' and all its dependencies were deleted." ErrorMessage="Webpart 'My new web part' was not found." />
    </cms:APIExamplePanel>
     <%-- Web part category --%>
    <cms:APIExamplePanel ID="pnlDeleteWebPartCategory" runat="server" GroupingText="Web part category">
        <cms:APIExample ID="apiDeleteWebPartCategory" runat="server" ButtonText="Delete category" APIExampleType="CleanUpMain" InfoMessage="Category 'My new category' and all its dependencies were deleted." ErrorMessage="Category 'My new category' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
