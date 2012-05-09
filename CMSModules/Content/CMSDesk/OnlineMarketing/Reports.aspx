<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="CMSModules_Content_CMSDesk_OnlineMarketing_Reports"
    Theme="Default" Title="Page's reports" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    EnableEventValidation="false" %>

<%@ Register Src="~/CMSModules/WebAnalytics/Controls/SelectGraphTypeAndPeriod.ascx"
    TagName="GraphType" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/GraphPreLoader.ascx" TagName="GraphPreLoader"
    TagPrefix="cms" %>
<asp:Content runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageHeaderLine">
        <cms:GraphPreLoader runat="server" ID="ucGraphPreLoader" />
        <cms:GraphType runat="server" ID="ucGraphType" />
    </div>
    <div class="PageHeaderLine">
        <cms:LocalizedRadioButton runat="server" ID="rbContent" ResourceString="development.content"
            AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" Checked="true" />
        <cms:LocalizedRadioButton runat="server" ID="rbTraffic" ResourceString="analytics_codename.trafficsources"
            AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
    </div>
    <asp:Panel runat="server" ID="pnlWarning" Visible="false" CssClass="PageHeaderLine">
        <asp:Label runat="server" ID="lblWarning"  />
    </asp:Panel>
    <div class="ReportBody">
        <asp:Panel runat="server" ID="pnlContent">
        </asp:Panel>
    </div>
</asp:Content>
