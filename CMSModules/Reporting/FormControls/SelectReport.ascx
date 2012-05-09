<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_FormControls_SelectReport"
    CodeFile="SelectReport.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="usReports" runat="server" ObjectType="reporting.report" SelectionMode="SingleTextBox"
            AllowEditTextBox="true" DisplayNameFormat="{%ReportDisplayName%}" ReturnColumnName="ReportName" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
