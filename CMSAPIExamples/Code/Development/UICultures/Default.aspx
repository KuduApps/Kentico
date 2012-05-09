<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSAPIExamples_Code_Development_UICultures_Default" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- UI Culture --%>
    <cms:APIExamplePanel ID="pnlCreateUICulture" runat="server" GroupingText="UI Culture">
        <cms:APIExample ID="apiCreateUICulture" runat="server" ButtonText="Create culture" InfoMessage="Culture 'My new culture' was created." />
        <cms:APIExample ID="apiGetAndUpdateUICulture" runat="server" ButtonText="Get and update culture" APIExampleType="ManageAdditional" InfoMessage="Culture 'My new culture' was updated." ErrorMessage="Culture 'My new culture' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateUICultures" runat="server" ButtonText="Get and bulk update cultures" APIExampleType="ManageAdditional" InfoMessage="All cultures matching the condition were updated." ErrorMessage="Cultures matching the condition were not found." />
    </cms:APIExamplePanel>
    
    <%-- Resource string --%>
    <cms:APIExamplePanel ID="pnlCreateResourceString" runat="server" GroupingText="Resource string">
        <cms:APIExample ID="apiCreateResourceString" runat="server" ButtonText="Create string" InfoMessage="String 'My new resource string' was created." />
        <cms:APIExample ID="apiGetAndUpdateResourceString" runat="server" ButtonText="Get and update string" APIExampleType="ManageAdditional" InfoMessage="String 'My new resource string' was updated." ErrorMessage="String 'My new resource string' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Resource string --%>
    <cms:APIExamplePanel ID="pnlDeleteResourceString" runat="server" GroupingText="Resource string">
        <cms:APIExample ID="apiDeleteResourceString" runat="server" ButtonText="Delete string" APIExampleType="CleanUpMain" InfoMessage="String 'My new resource string' and all its dependencies were deleted." ErrorMessage="String 'My new resource string' was not found." />
    </cms:APIExamplePanel>

    <%-- UI Culture --%>
    <cms:APIExamplePanel ID="pnlDeleteUICulture" runat="server" GroupingText="UI Culture">
        <cms:APIExample ID="apiDeleteUICulture" runat="server" ButtonText="Delete culture" APIExampleType="CleanUpMain" InfoMessage="Culture 'My new culture' and all its dependencies were deleted." ErrorMessage="Culture 'My new culture' was not found." />
    </cms:APIExamplePanel>
</asp:Content>