<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectGraphType.ascx.cs"
    Inherits="CMSModules_WebAnalytics_Controls_SelectGraphType" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<table cellpadding="0" cellspacing="0"  >
    <tr align="center" >
        <td id="pnlHour" runat="server">
            <asp:Image ID="imgHour" runat="server" Visible="false" />
            <cms:LocalizedLabel runat="server" ID="lblHour" ResourceString="reports_hour.header"
                Visible="false" />
        </td>
        <td id="pnlDay" runat="server">
            <asp:Image ID="imgDay" runat="server" Visible="false" />
            <cms:LocalizedLabel runat="server" ID="lblDay" ResourceString="reports_day.header"
                Visible="false" />
        </td>
        <td id="pnlWeek" runat="server">
            <asp:Image ID="imgWeek" runat="server" Visible="false" />
            <cms:LocalizedLabel runat="server" ID="lblWeek" ResourceString="reports_week.header"
                Visible="false" />
        </td>
        <td id="pnlMonth" runat="server">
            <asp:Image ID="imgMonth" runat="server" Visible="false" />
            <cms:LocalizedLabel runat="server" ID="lblMonth" ResourceString="reports_month.header"
                Visible="false" />
        </td>
        <td id="pnlYear" runat="server">
            <asp:Image ID="imgYear" runat="server" Visible="false" />
            <cms:LocalizedLabel runat="server" ID="lblYear" ResourceString="reports_year.header"
                Visible="false" />
        </td>
    </tr>
</table>
