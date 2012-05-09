<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AnalyticsReportViewer.ascx.cs"
    Inherits="CMSModules_WebAnalytics_Controls_AnalyticsReportViewer" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/SelectGraphTypeAndPeriod.ascx"
    TagName="GraphTypeAndPeriod" TagPrefix="cms" %>
<asp:Panel runat="server" ID="pnlPeriodSelectors" CssClass="PageHeaderLine">
    <cms:GraphTypeAndPeriod runat="server" ID="ucGraphTypePeriod" />
</asp:Panel>
<div class="ReportBody ReportBodyAnalytics" runat="server" id="divGraphArea">
    <asp:Label CssClass="ErrorLabel" ID="lblError" runat="server" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />        
</div>
