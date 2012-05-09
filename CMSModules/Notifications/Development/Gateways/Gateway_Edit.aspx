<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Notifications_Development_Gateways_Gateway_Edit"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Title="Gateway - edit" CodeFile="Gateway_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblDisplayName" runat="server" CssClass="FieldLabel" ResourceString="general.displayname"
                    DisplayColon="true" EnableViewState="false" />
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
                <cms:LocalizedLabel ID="lblCodeName" runat="server" CssClass="FieldLabel" ResourceString="general.codename"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtCodeName" CssClass="TextBoxField" MaxLength="250"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator runat="server" ControlToValidate="txtCodeName" ID="valCodeName"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <cms:LocalizedLabel ID="lblDescription" runat="server" CssClass="FieldLabel" ResourceString="general.description"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox runat="server" ID="txtDescription" CssClass="TextAreaField" TextMode="MultiLine"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblEnabled" runat="server" CssClass="FieldLabel" ResourceString="general.enabled"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkEnabled" CssClass="CheckBoxMovedLeft" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-top: 10px; font-weight: bold;">
                <asp:Label runat="server" ID="lblSettings" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblAssemblyName" runat="server" CssClass="FieldLabel" ResourceString="notifications.gateway.assemblyname"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtAssemblyName" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator runat="server" ControlToValidate="txtAssemblyName" ID="valAssemblyName"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblClassName" runat="server" CssClass="FieldLabel" ResourceString="notifications.gateway.classname"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtClassName" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator runat="server" ControlToValidate="txtClassName" ID="valClassName"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblSupportsSubject" runat="server" CssClass="FieldLabel"
                    ResourceString="notifications.gateway.supportssubject" DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkSupportsSubject" CssClass="CheckBoxMovedLeft"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblSupportsHTML" runat="server" CssClass="FieldLabel" ResourceString="notifications.gateway.supportshtml"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkSupportsHTML" CssClass="CheckBoxMovedLeft" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblSupportsPlain" runat="server" CssClass="FieldLabel" ResourceString="notifications.gateway.supportsplain"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkSupportsPlain" CssClass="CheckBoxMovedLeft" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" Style="text-align: center;" CssClass="SubmitButton"
                    OnClick="btnOK_Click" EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Content>
