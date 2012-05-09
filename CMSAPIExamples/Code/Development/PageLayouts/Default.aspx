<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Development_PageLayouts_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Layout --%>
    <cms:APIExamplePanel ID="pnlCreateLayout" runat="server" GroupingText="Layout">
        <cms:APIExample ID="apiCreateLayout" runat="server" ButtonText="Create layout" InfoMessage="Layout 'My new layout' was created." />
        <cms:APIExample ID="apiGetAndUpdateLayout" runat="server" ButtonText="Get and update layout" APIExampleType="ManageAdditional" InfoMessage="Layout 'My new layout' was updated." ErrorMessage="Layout 'My new layout' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateLayouts" runat="server" ButtonText="Get and bulk update layouts" APIExampleType="ManageAdditional" InfoMessage="All layouts matching the condition were updated." ErrorMessage="Layouts matching the condition were not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Layout --%>
    <cms:APIExamplePanel ID="pnlDeleteLayout" runat="server" GroupingText="Layout">
        <cms:APIExample ID="apiDeleteLayout" runat="server" ButtonText="Delete layout" APIExampleType="CleanUpMain" InfoMessage="Layout 'My new layout' and all its dependencies were deleted." ErrorMessage="Layout 'My new layout' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
