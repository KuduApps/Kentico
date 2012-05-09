<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectGraphPeriod.ascx.cs"
    Inherits="CMSModules_WebAnalytics_Controls_SelectGraphPeriod" %>
<asp:Panel ID="pnlRange" runat="server">
    <cms:RangeDateTimePicker runat="server" ID="ucRangeDatePicker" />
    &nbsp;&nbsp;
    <cms:LocalizedButton runat="server" ID="btnUpdate" ResourceString="general.update"
       CssClass="LongWebAnalyticsButton XLongButton" />      
</asp:Panel>
