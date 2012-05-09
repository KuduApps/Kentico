<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectValidity.ascx.cs"
    Inherits="CMSAdminControls_UI_Selectors_SelectValidity" %>
<asp:Panel ID="pnlSelectValidity" runat="server">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table cellspacing="0" cellpadding="0">
        <%-- Valid for --%>
        <tr>
            <td>
                <cms:LocalizedRadioButton ID="radDays" runat="server" GroupName="Validity" ResourceString="general.selectvalidity.days"
                    OnCheckedChanged="ValidityRadioGroup_CheckedChanged" />
                <cms:LocalizedRadioButton ID="radWeeks" runat="server" GroupName="Validity" ResourceString="general.selectvalidity.weeks"
                    OnCheckedChanged="ValidityRadioGroup_CheckedChanged" />
                <cms:LocalizedRadioButton ID="radMonths" runat="server" GroupName="Validity" ResourceString="general.selectvalidity.months"
                    OnCheckedChanged="ValidityRadioGroup_CheckedChanged" />
                <cms:LocalizedRadioButton ID="radYears" runat="server" GroupName="Validity" ResourceString="general.selectvalidity.years"
                    OnCheckedChanged="ValidityRadioGroup_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:CMSTextBox ID="txtValidFor" runat="server" CssClass="TextBoxField" />
            </td>
        </tr>
        <%-- Valid until --%>
        <tr>
            <td>
                <cms:LocalizedRadioButton ID="radUntil" runat="server" GroupName="Validity" ResourceString="general.selectvalidity.until"
                    OnCheckedChanged="ValidityRadioGroup_CheckedChanged" Checked="true" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:DateTimePicker ID="untilDateElem" runat="server" EditTime="false" AllowEmptyValue="true"
                    DisplayNA="true" />
            </td>
        </tr>
    </table>
</asp:Panel>
