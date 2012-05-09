<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_TimeZones_Controls_TimeZoneRuleEdior" CodeFile="TimeZoneRuleEdior.ascx.cs" %>
<cms:CMSUpdatePanel ID="updPanel" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td><asp:Label ID="lblMonth" runat="server" /></td>
                <td><asp:Label ID="lblCondition" runat="server" /></td>
                <td><asp:Label ID="lblDay" runat="server" /></td>
                <td><asp:Label ID="lblTime" runat="server" /></td>
                <td><asp:Label ID="lblValue" runat="server" /></td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="drpMonth" runat="server" CssClass="DropDownFieldSmall" /></td>
                <td>
                    <asp:DropDownList ID="drpCondition" runat="server" AutoPostBack="true" CssClass="DropDownFieldSmall"
                        OnSelectedIndexChanged="drpCondition_SelectedIndexChanged">
                        <asp:ListItem Value="FIRST" Text="FIRST" />
                        <asp:ListItem Value="LAST" Text="LAST" />
                        <asp:ListItem Value=">=" Text=">=" />
                        <asp:ListItem Value="<=" Text="<=" />
                        <asp:ListItem Value="=" Text="=" />
                    </asp:DropDownList></td>
                <td>
                    <asp:DropDownList ID="drpDay" runat="server" CssClass="DropDownFieldSmall" />
                    <asp:DropDownList ID="drpDayValue" CssClass="DropDownFieldShort" runat="server" />
                    </td>
                <td>
                    <cms:CMSTextBox ID="txtAtHour" runat="server" CssClass="SuperSmallTextBox" />
                    <strong>:</strong>
                    <cms:CMSTextBox ID="txtAtMinute" runat="server" CssClass="SuperSmallTextBox" /></td>
                <td>
                    <cms:CMSTextBox ID="txtValue" runat="server" CssClass="SuperSmallTextBox" /></td>
            </tr>
        </table>
    </ContentTemplate>
</cms:CMSUpdatePanel>
