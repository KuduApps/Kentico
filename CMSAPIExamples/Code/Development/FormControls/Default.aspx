<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSAPIExamples_Code_Development_FormControls_Default" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Form control --%>
    <cms:APIExamplePanel ID="pnlCreateFormControl" runat="server" GroupingText="Form control">
        <cms:APIExample ID="apiCreateFormControl" runat="server" ButtonText="Create control" InfoMessage="Control 'My new control' was created." />
        <cms:APIExample ID="apiGetAndUpdateFormControl" runat="server" ButtonText="Get and update control" APIExampleType="ManageAdditional" InfoMessage="Control 'My new control' was updated." ErrorMessage="Control 'My new control' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateFormControls" runat="server" ButtonText="Get and bulk update controls" APIExampleType="ManageAdditional" InfoMessage="All controls matching the condition were updated." ErrorMessage="Controls matching the condition were not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Form control --%>
    <cms:APIExamplePanel ID="pnlDeleteFormControl" runat="server" GroupingText="Form control">
        <cms:APIExample ID="apiDeleteFormControl" runat="server" ButtonText="Delete control" APIExampleType="CleanUpMain" InfoMessage="Control 'My new control' and all its dependencies were deleted." ErrorMessage="Control 'My new control' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
