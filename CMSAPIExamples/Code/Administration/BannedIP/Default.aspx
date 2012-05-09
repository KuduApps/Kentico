<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" Inherits="CMSAPIExamples_Code_Administration_BannedIP_Default" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Banned ip --%>
    <cms:APIExamplePanel ID="pnlCreateBannedIp" runat="server" GroupingText="Banned IP">
        <cms:APIExample ID="apiCreateBannedIp" runat="server" ButtonText="Create IP" InfoMessage="Banned IP 'MyNewIp' was created." />
        <cms:APIExample ID="apiGetAndUpdateBannedIp" runat="server" ButtonText="Get and update IP" APIExampleType="ManageAdditional" InfoMessage="Banned IP 'MyNewIp' was updated." ErrorMessage="Banned IP 'MyNewIp' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateBannedIps" runat="server" ButtonText="Get and bulk update IPs" APIExampleType="ManageAdditional" InfoMessage="All Banned IPs matching the condition were updated." ErrorMessage="Banned IPs matching the condition were not found." />
        <cms:APIExample ID="apiCheckBannedIp" runat="server" ButtonText="Check banned IP for action" APIExampleType="ManageAdditional" InfoMessage="Your IP can login or register user." ErrorMessage="Action for banned IP was denied or Banned IPs module is disabled." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Banned ip --%>
    <cms:APIExamplePanel ID="pnlDeleteBannedIp" runat="server" GroupingText="Banned IP">
        <cms:APIExample ID="apiDeleteBannedIp" runat="server" ButtonText="Delete IP" APIExampleType="CleanUpMain" InfoMessage="IP 'MyNewIp' and all its dependencies were deleted." ErrorMessage="IP 'MyNewIp' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
