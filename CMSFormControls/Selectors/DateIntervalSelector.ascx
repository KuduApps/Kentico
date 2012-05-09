<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateIntervalSelector.ascx.cs"
    Inherits="CMSFormControls_Selectors_DateIntervalSelector" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td colspan="3">
            <cms:LocalizedRadioButton runat="server" ID="radToday" GroupName="rads" ResourceString="dateintervalsel.today"
                Checked="true" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <cms:LocalizedRadioButton runat="server" ID="radCurrentWeek" GroupName="rads" ResourceString="dateintervalsel.week" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <cms:LocalizedRadioButton runat="server" ID="radCurrentMonth" GroupName="rads" ResourceString="dateintervalsel.month" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedRadioButton runat="server" ID="radLast" GroupName="rads" ResourceString="dateintervalsel.last" style="white-space:nowrap;" />
        </td>
        <td>
            <cms:CMSTextBox runat="server" ID="txtLast" style="margin: 0px 7px;" />
        </td>
        <td style="width:100%;">
            <cms:LocalizedLabel runat="server" ID="lblDays" ResourceString="dateintervalsel.days"
                EnableViewState="false" />
        </td>
    </tr>
</table>
