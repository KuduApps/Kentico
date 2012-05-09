<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RangeModalCalendar.ascx.cs"
    Inherits="CMSAdminControls_ModalCalendar_RangeModalCalendar" %>
<div id="rangeCalendar" runat="server" class="datetime-ui-range-header datetime-ui-corner-all datetime-ui-range-div">
    <table>
        <tr>
            <td>
                <div id="dateFrom" runat="server" />
            </td>
            <td>
                <div id="dateTo" runat="server" />
            </td>
        </tr>
    </table>
    <div class="datetime-ui-range-button-panel TextRight">
        <cms:LocalizedButton runat="server" ID="btnNA" ResourceString="general.na" onMouseOver="$j(this).addClass('ui-state-hover')"
            onMouseOut="$j(this).removeClass('ui-state-hover')" CssClass=" datetime-ui-buttonOK datetime-ui-datepicker-close datetime-ui-state-default datetime-ui-priority-primary datetime-ui-corner-all"
            Visible="false" />
        <cms:LocalizedButton runat="server" ID="btnOK" ResourceString="general.ok" onMouseOver="$j(this).addClass('datetime-ui-state-hover')"
            onMouseOut="$j(this).removeClass('datetime-ui-state-hover')" CssClass=" datetime-ui-buttonOK datetime-ui-datepicker-close datetime-ui-state-default datetime-ui-priority-primary datetime-ui-corner-all" />
    </div>
</div>
<asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
