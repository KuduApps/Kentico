<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Administration_BadWords_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Bad word --%>
    <cms:APIExamplePanel ID="pnlCreateBadWord" runat="server" GroupingText="Bad word">
        <cms:APIExample ID="apiCreateBadWord" runat="server" ButtonText="Create bad word" InfoMessage="Bad word 'testbadword' was created." />
        <cms:APIExample ID="apiGetAndUpdateBadWord" runat="server" ButtonText="Get and update bad word" APIExampleType="ManageAdditional" InfoMessage="Bad word action of 'testbadword' was changed to Replace and its replacement string was set to 'testpoliteword'." ErrorMessage="Bad word 'testbadword' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateBadWords" runat="server" ButtonText="Get and bulk update bad words" APIExampleType="ManageAdditional" InfoMessage="All 'testbadword' bad words were updated to use the Replace action and the 'testpoliteword' replacement string." ErrorMessage="No 'testbadword' bad words were found." />
    </cms:APIExamplePanel>
    <%-- Bad word --%>
    <cms:APIExamplePanel ID="pnlBadWordChecks" runat="server" GroupingText="Performing bad word checks">
        <cms:APIExample ID="apiCheckSingleBadWord" runat="server" ButtonText="Check string for single bad word" InfoMessage="Bad word 'testbadword' was detected in the string." ErrorMessage="Bad word 'testbadword' is not defined." />
        <cms:APIExample ID="apiCheckAllBadWords" runat="server" ButtonText="Check string for all bad words" APIExampleType="ManageAdditional" InfoMessage="Some bad words were detected in the checked string." ErrorMessage="No bad words were detected in the checked string." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Bad word --%>
    <cms:APIExamplePanel ID="pnlDeleteBadWord" runat="server" GroupingText="Bad word">
        <cms:APIExample ID="apiDeleteBadWord" runat="server" ButtonText="Delete word(s)" APIExampleType="CleanUpMain" InfoMessage="All bad words created by the examples on this page and all their dependencies were deleted." ErrorMessage="No 'testbadword' bad word was found." />
    </cms:APIExamplePanel>
</asp:Content>
