<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Polls_Controls_PollNew" CodeFile="PollNew.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<asp:Panel ID="pnlBody" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" AssociatedControlID="txtDisplayName" ID="lblDisplayName"
                    EnableViewState="false" ResourceString="general.displayname" DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                    ValidationGroup="Required" EnableViewState="false" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcCodeName" runat="Server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" AssociatedControlID="txtCodeName" ID="lblCodeName"
                        EnableViewState="false" ResourceString="general.codename" DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtCodeName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                    <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtCodeName"
                        ValidationGroup="Required" EnableViewState="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblTitle" AssociatedControlID="txtTitle" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtTitle" runat="server" CssClass="TextBoxField" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblQuestion" AssociatedControlID="txtQuestion" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtQuestion" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                    MaxLength="450" />
                <cms:CMSRequiredFieldValidator Display="Dynamic" ID="rfvQuestion" runat="server" ControlToValidate="txtQuestion:textbox"
                    ValidationGroup="Required" EnableViewState="false" />
                <cms:CMSRegularExpressionValidator ID="rfvMaxLength" Display="Dynamic" ControlToValidate="txtQuestion:textbox"
                    runat="server" ValidationExpression="^[\s\S]{0,450}$" ValidationGroup="Required"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ValidationGroup="Required" />
            </td>
        </tr>
    </table>
</asp:Panel>
