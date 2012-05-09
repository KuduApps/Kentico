<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ResetPassword.ascx.cs"
    Inherits="CMSModules_Membership_Controls_ResetPassword" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Passwords/PasswordStrength.ascx" TagName="PasswordStrength"
    TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<asp:Panel runat="server" ID="pnlReset">    
    <table>
        <tr>
            <td class="FieldLabel FieldLabelTop">
                <cms:LocalizedLabel ID="lblPassword" runat="server" EnableViewState="false" ResourceString="general.password" DisplayColon="true"></cms:LocalizedLabel>
            </td>
            <td>
                <cms:PasswordStrength runat="server" ID="passStrength" TextBoxClass="LogonTextBox" ValidationGroup="PasswordReset" /> 
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblConfirmPassword" runat="server" EnableViewState="false"
                    ResourceString="general.confirmpassword" DisplayColon="true" ></cms:LocalizedLabel>
            </td>
            <td>
                <cms:CMSTextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="200" CssClass="LogonTextBox"></cms:CMSTextBox >
                <cms:CMSRequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ValidationGroup="PasswordReset"
                    Display="Dynamic"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnReset" EnableViewState="false" CssClass="SubmitButton"
                    OnClick="btnReset_Click" ValidationGroup="PasswordReset" />
            </td>
        </tr>
    </table>
</asp:Panel>
