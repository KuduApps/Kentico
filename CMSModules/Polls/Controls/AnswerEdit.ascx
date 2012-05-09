<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Polls_Controls_AnswerEdit" CodeFile="AnswerEdit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<asp:Panel ID="pnlBody" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblAnswerText" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtAnswerText" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvAnswerText" runat="server" ControlToValidate="txtAnswerText:textbox"
                    ValidationGroup="required" EnableViewState="false" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcVotes">
            <tr>
                <td class="FieldLabel">
                    <asp:Label runat="server" ID="lblVotes" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtVotes" runat="server" CssClass="ShortTextBox" MaxLength="9" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblAnswerEnabled" EnableViewState="false"
                        ResourceString="general.enabled" DisplayColon="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkAnswerEnabled" runat="server" Checked="true" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ValidationGroup="required" />
            </td>
        </tr>
    </table>
</asp:Panel>
