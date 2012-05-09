<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Membership_Registration_RegistrationForm"
    CodeFile="~/CMSWebParts/Membership/Registration/RegistrationForm.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Inputs/SecurityCode.ascx" TagName="SecurityCode"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Passwords/PasswordStrength.ascx" TagName="PasswordStrength"
    TagPrefix="cms" %>
<asp:Label ID="lblError" runat="server" ForeColor="red" EnableViewState="false" />
<asp:Label ID="lblText" runat="server" Visible="false" EnableViewState="false" />
<asp:Panel ID="pnlForm" runat="server" DefaultButton="btnOK">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName" EnableViewState="false" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtFirstName" EnableEncoding="true" runat="server" CssClass="LogonTextBox"
                    MaxLength="100" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                    Display="Dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblLastName" runat="server" AssociatedControlID="txtLastName" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtLastName" EnableEncoding="true" runat="server" CssClass="LogonTextBox"
                    MaxLength="100" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                    Display="Dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" EnableViewState="false" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtEmail" runat="server" CssClass="LogonTextBox" MaxLength="100" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    Display="Dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblPassword" runat="server" EnableViewState="false" />
            </td>
            <td>               
                <cms:PasswordStrength runat="server" ID="passStrength" ShowValidationOnNewLine="true" TextBoxClass="LogonTextBox" />               
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConfirmPassword" runat="server" AssociatedControlID="txtConfirmPassword"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="LogonTextBox"
                    MaxLength="100" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                    Display="Dynamic" EnableViewState="false" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcCaptcha">
            <tr>
                <td>
                    <asp:Label ID="lblCaptcha" runat="server" AssociatedControlID="scCaptcha" EnableViewState="false" />
                </td>
                <td>
                    <cms:SecurityCode ID="scCaptcha" GenerateNumberEveryTime="false" runat="server" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td style="height: 26px">
            </td>
            <td style="height: 26px">
                <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="ContentButton"
                    EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Panel>
