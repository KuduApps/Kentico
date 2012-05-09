<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_TagGroups_Controls_TagEdit" CodeFile="TagEdit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<asp:Label ID="lblInfo" runat="server" Visible="false" CssClass="InfoLabel" EnableViewState="false" />
<asp:Label ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
<table>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblDisplayName" runat="server" CssClass="FieldLabel" ResourceString="general.displayname"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtDisplayName" runat="server" MaxLength="250" CssClass="TextBoxField"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                Display="Dynamic" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblCodeName" runat="server" CssClass="FieldLabel" ResourceString="general.codename"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtCodeName" runat="server" MaxLength="250" CssClass="TextBoxField"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtCodeName"
                Display="Dynamic" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            <cms:LocalizedLabel ID="lblDescription" runat="server" CssClass="FieldLabel" ResourceString="general.description"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtDescription" runat="server" CssClass="TextAreaField" TextMode="MultiLine"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <cms:LocalizedButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton"
                ResourceString="general.ok" EnableViewState="false" />
        </td>
    </tr>
</table>
