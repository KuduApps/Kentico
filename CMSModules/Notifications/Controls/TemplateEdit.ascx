<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Notifications_Controls_TemplateEdit" CodeFile="TemplateEdit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table>
    <tr>
        <td>
            <cms:LocalizedLabel runat="server" ID="lblDisplayName" CssClass="ContentLabel" DisplayColon="true"
                EnableViewState="false" />
        </td>
        <td>
            <cms:LocalizableTextBox runat="server" ID="txtDisplayName" CssClass="TextBoxField" MaxLength="250"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator runat="server" ControlToValidate="txtDisplayName:textbox" ID="valDisplayName"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel runat="server" ID="lblCodeName" CssClass="ContentLabel" DisplayColon="true"
                EnableViewState="false" />
        </td>
        <td>
            <cms:CMSTextBox runat="server" ID="txtCodeName" CssClass="TextBoxField" MaxLength="250"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator runat="server" ControlToValidate="txtCodeName" ID="valCodeName"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:LocalizedButton runat="server" ID="btnOk" Style="text-align: center;" CssClass="SubmitButton"
                OnClick="btnOK_Click" EnableViewState="false" />
        </td>
    </tr>
</table>
