<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSAdminControls_UI_Selectors_ScheduleInterval" CodeFile="ScheduleInterval.ascx.cs" %>

<asp:Panel runat="server" ID="pnlBody" CssClass="EditingFormControl">
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" Visible="false" />
    <%--Period--%>
    <table cellpadding="0" cellspacing="3">
        <asp:PlaceHolder runat="server" ID="pnlPeriod">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblPeriod" runat="server" EnableViewState="false" ResourceString="scheduleinterval.period"
                        AssociatedControlID="drpPeriod" />
                </td>
                <td>
                    <asp:DropDownList ID="drpPeriod" runat="server" OnSelectedIndexChanged="DrpPeriod_OnSelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblStart" runat="server" EnableViewState="false" ResourceString="scheduleinterval.start"
                        AssociatedControlID="dateTimePicker" />
                </td>
                <td>
                    <cms:DateTimePicker ID="dateTimePicker" runat="server" SupportFolder="~/CMSAdminControls/Calendar"
                        EditTime="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </asp:PlaceHolder>
        <%--Every--%>
        <asp:PlaceHolder runat="server" ID="pnlEvery">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblEvery" runat="server" EnableViewState="false" ResourceString="scheduleinterval.every"
                        AssociatedControlID="txtEvery" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtEvery" runat="server" Style="width: 40px" MaxLength="5" />&nbsp;
                    <asp:Label ID="lblEveryPeriod" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <cms:CMSRequiredFieldValidator ID="rfvEvery" runat="server" ControlToValidate="txtEvery"
                        Display="dynamic" />
                    <cms:CMSRangeValidator ID="rvEvery" runat="server" ControlToValidate="txtEvery" Type="integer"
                        Display="dynamic" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <%--Between--%>
        <asp:PlaceHolder runat="server" ID="pnlBetween">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblBetween" runat="server" EnableViewState="false" ResourceString="scheduleinterval.between"
                        AssociatedControlID="txtFromHours" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtFromHours" runat="server" Style="width: 20px" MaxLength="2" />
                    :
                    <cms:CMSTextBox ID="txtFromMinutes" runat="server" Style="width: 20px" MaxLength="2" />
                    &nbsp;<cms:LocalizedLabel ID="lblAnd" runat="server" EnableViewState="false" ResourceString="scheduleinterval.and"
                        AssociatedControlID="txtToHours" />&nbsp;
                    <cms:CMSTextBox ID="txtToHours" runat="server" Style="width: 20px" MaxLength="2" />
                    :
                    <cms:CMSTextBox ID="txtToMinutes" runat="server" Style="width: 20px" MaxLength="2" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <cms:CMSRequiredFieldValidator ID="rfvFromHours" runat="server" ControlToValidate="txtFromHours"
                        Display="dynamic" />
                    <cms:CMSRangeValidator ID="rvFromHours" runat="server" ControlToValidate="txtFromHours"
                        Type="integer" Display="dynamic" />
                    <cms:CMSRequiredFieldValidator ID="rfvFromMinutes" runat="server" ControlToValidate="txtFromMinutes"
                        Display="dynamic" />
                    <cms:CMSRangeValidator ID="rvFromMinutes" runat="server" ControlToValidate="txtFromMinutes"
                        Type="integer" Display="dynamic" />
                    <cms:CMSRequiredFieldValidator ID="rfvToHours" runat="server" ControlToValidate="txtToHours"
                        Display="dynamic" />
                    <cms:CMSRangeValidator ID="rvToHours" runat="server" ControlToValidate="txtToHours"
                        Type="integer" Display="dynamic" />
                    <cms:CMSRequiredFieldValidator ID="rfvToMinutes" runat="server" ControlToValidate="txtToMinutes"
                        Display="dynamic" />
                    <cms:CMSRangeValidator ID="rvToMinutes" runat="server" ControlToValidate="txtToMinutes"
                        Type="integer" Display="dynamic" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <%--Days--%>
        <asp:PlaceHolder runat="server" ID="pnlDays">
            <tr>
                <td style="vertical-align: top;">
                    <cms:LocalizedLabel ID="lblDays" runat="server" EnableViewState="false" ResourceString="scheduleinterval.days"
                        AssociatedControlID="chkWeek" />
                </td>
                <td>
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:CheckBoxList ID="chkWeek" runat="server" />
                            </td>
                            <td style="vertical-align: text-top">
                                <asp:CheckBoxList ID="chkWeekEnd" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </asp:PlaceHolder>
        <%--Month--%>
        <asp:PlaceHolder runat="server" ID="pnlMonth" Visible="false">
            <tr>
                <td>
                    <cms:LocalizedRadioButton ID="radMonth1" runat="server" AutoPostBack="true" OnCheckedChanged="radMonth1_CheckedChanged"
                        Checked="True" ResourceString="scheduleinterval.period.day" />
                </td>
                <td>
                    <asp:DropDownList ID="drpMonth1" runat="server" />&nbsp;
                    <cms:LocalizedLabel ID="lblMonth1" runat="server" EnableViewState="false" ResourceString="scheduleinterval.months.ofthemonth(s)" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedRadioButton ID="radMonth2" runat="server" AutoPostBack="true" OnCheckedChanged="radMonth2_CheckedChanged"
                        ResourceString="scheduleinterval.months.the" />
                </td>
                <td>
                    <asp:DropDownList ID="drpMonth2" runat="server" />
                    &nbsp;
                    <asp:DropDownList ID="drpMonth3" runat="server" />&nbsp;
                    <cms:LocalizedLabel ID="lblMonth2" runat="server" EnableViewState="false" ResourceString="scheduleinterval.months.ofthemonth(s)" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</asp:Panel>
