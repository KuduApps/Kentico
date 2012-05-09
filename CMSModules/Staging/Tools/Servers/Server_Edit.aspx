<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Servers_Server_Edit"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Server edit" Theme="Default"
    CodeFile="Server_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSAdminControls/UI/System/ServerChecker.ascx" TagPrefix="cms"
    TagName="ServerChecker" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Passwords/EncryptedPassword.ascx" TagPrefix="cms" TagName="EncryptedPassword" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Panel runat="server" ID="pnlContent" DefaultButton="btnOk">
        <table style="vertical-align: top;">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblServerDisplayName" EnableViewState="false"
                        ResourceString="Server_Edit.ServerDisplayNameLabel" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtServerDisplayName" runat="server" CssClass="TextBoxField" MaxLength="440" />
                    <cms:CMSRequiredFieldValidator ID="rfvServerDisplayName" runat="server" ControlToValidate="txtServerDisplayName:textbox"
                        EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblServerName" EnableViewState="false" ResourceString="Server_Edit.ServerNameLabel" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtServerName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                    <cms:CMSRequiredFieldValidator ID="rfvServerName" runat="server" ControlToValidate="txtServerName"
                        EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblServerURL" EnableViewState="false" ResourceString="Server_Edit.ServerURLLabel" />
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <cms:CMSTextBox ID="txtServerURL" runat="server" CssClass="TextBoxField" MaxLength="450" />
                            </td>
                            <td>
                                <cms:CMSRequiredFieldValidator ID="rfvServerURL" runat="server" ControlToValidate="txtServerURL"
                                    EnableViewState="false" Display="Dynamic" />
                            </td>
                            <td>
                                &nbsp;<cms:ServerChecker runat="server" ID="serverChecker" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblServerEnabled" EnableViewState="false"
                        ResourceString="general.enabled" DisplayColon="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkServerEnabled" runat="server" CssClass="CheckBoxMovedLeft" Checked="true" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel" colspan="2">
                    <cms:LocalizedLabel runat="server" ID="lblServerAuthentication" EnableViewState="false"
                        ResourceString="Server_Edit.ServerAuthenticationLabel" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <cms:LocalizedRadioButton ID="radUserName" runat="server" GroupName="ServerAuthentication"
                        AutoPostBack="true" Checked="true" ResourceString="Server_Edit.Authentication_UserName" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <cms:LocalizedLabel runat="server" ID="lblServerUsername" EnableViewState="false"
                        ResourceString="general.username" DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtServerUsername" runat="server" CssClass="TextBoxField" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <cms:LocalizedLabel runat="server" ID="lblServerPassword" EnableViewState="false"
                        ResourceString="Server_Edit.ServerPasswordLabel" />
                </td>
                <td>
                    <cms:EncryptedPassword ID="encryptedPassword" runat="server" MayLength="100"/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <cms:LocalizedRadioButton ID="radX509" runat="server" GroupName="ServerAuthentication"
                        AutoPostBack="true" ResourceString="Server_Edit.Authentication_X509" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <cms:LocalizedLabel runat="server" ID="lblServerX509ClientKeyID" EnableViewState="false"
                        ResourceString="Server_Edit.ServerX509ClientKeyIDLabel" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtServerX509ClientKeyID" runat="server" CssClass="TextBoxField"
                        MaxLength="200" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <cms:LocalizedLabel runat="server" ID="lblServerX509ServerKeyID" EnableViewState="false"
                        ResourceString="Server_Edit.ServerX509ServerKeyIDLabel" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtServerX509ServerKeyID" runat="server" CssClass="TextBoxField"
                        MaxLength="200" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <cms:LocalizedButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                        CssClass="SubmitButton" ResourceString="General.OK" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
