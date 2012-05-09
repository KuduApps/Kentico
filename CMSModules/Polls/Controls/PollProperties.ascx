<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Polls_Controls_PollProperties"
    CodeFile="PollProperties.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<asp:Panel ID="pnlBody" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblDisplayName" EnableViewState="false" ResourceString="general.displayname"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                    ValidationGroup="required" Display="Dynamic" EnableViewState="false" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcCodeName" runat="Server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblCodeName" EnableViewState="false" ResourceString="general.codename"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtCodeName" runat="server" CssClass="TextBoxField" MaxLength="200" /><br />
                    <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtCodeName"
                        ValidationGroup="required" Display="Dynamic" EnableViewState="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblTitle" EnableViewState="false" ResourceString="Polls_Edit.PollTitleLabel"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtTitle" runat="server" CssClass="TextBoxField" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblQuestion" EnableViewState="false" ResourceString="Polls_Edit.PollQuestionLabel"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtQuestion" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                    MaxLength="450" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvQuestion" runat="server" ControlToValidate="txtQuestion:textbox"
                    ValidationGroup="required" Display="Dynamic" EnableViewState="false" />
                <cms:CMSRegularExpressionValidator ID="rfvMaxLength" Display="Dynamic" ControlToValidate="txtQuestion:textbox"
                    runat="server" ValidationExpression="^[\s\S]{0,450}$" ValidationGroup="required"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblOpenFrom" EnableViewState="false" ResourceString="Polls_Edit.PollOpenFromLabel"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:DateTimePicker ID="dtPickerOpenFrom" runat="server" SupportFolder="~/CMSAdminControls/Calendar">
                </cms:DateTimePicker>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblOpenTo" EnableViewState="false" ResourceString="Polls_Edit.PollOpenToLabel"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:DateTimePicker ID="dtPickerOpenTo" runat="server" SupportFolder="~/CMSAdminControls/Calendar">
                </cms:DateTimePicker>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblResponseMessage" EnableViewState="false"
                    ResourceString="Polls_Edit.PollResponseMessageLabel" DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtResponseMessage" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                    MaxLength="450" />
                <cms:CMSRegularExpressionValidator ID="rfvMaxLengthResponse" Display="Dynamic" ControlToValidate="txtResponseMessage:textbox"
                    runat="server" ValidationExpression="^[\s\S]{0,450}$" ValidationGroup="required"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblAllowMultipleAnswers" EnableViewState="false"
                    ResourceString="Polls_Edit.PollAllowMultipleAnswersLabel" DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkAllowMultipleAnswers" runat="server" CssClass="CheckBoxMovedLeft"
                    AutoPostBack="false" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcOnline" Visible="false">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="LocalizedLabel1" EnableViewState="false" ResourceString="Polls_Edit.PollLogActivity"
                        DisplayColon="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkLogActivity" runat="server" CssClass="CheckBoxMovedLeft" AutoPostBack="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    ResourceString="General.OK" CssClass="SubmitButton" ValidationGroup="required" />
            </td>
        </tr>
    </table>
</asp:Panel>
