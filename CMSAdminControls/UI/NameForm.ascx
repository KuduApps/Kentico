<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_NameForm" CodeFile="NameForm.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<table>
    <tr>
        <td class="FieldLabel">
            <asp:Label ID="lblDisplayName" runat="server" EnableViewState="false" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100" />
            <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ErrorMessage="" ControlToValidate="txtDisplayName:textbox"
                EnableViewState="false"></cms:CMSRequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <asp:Label ID="lblCodeName" runat="server" EnableViewState="false" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtCodeName" runat="server" CssClass="TextBoxField" MaxLength="100" />
            <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ErrorMessage="" ControlToValidate="txtCodeName"
                EnableViewState="false"></cms:CMSRequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton ID="btnOk" runat="server" Visible="false" CssClass="SubmitButton" EnableViewState="false" />
        </td>
    </tr>
</table>
