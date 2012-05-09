<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Membership_FormControls_Passwords_PasswordConfirmator" CodeFile="PasswordConfirmator.ascx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Passwords/PasswordStrength.ascx" TagName="PasswordStrength"
    TagPrefix="cms" %>
<div> 
     <cms:PasswordStrength runat="server" ID="passStrength" AllowEmpty="true" TextBoxClass="LogonTextBox" /> 
    <div class="ConfirmationSeparator">
    </div>
    <cms:LocalizedLabel ID="lblConfirmPassword" runat="server" ResourceString="general.confirmpassword" AssociatedControlID="txtConfirmPassword" EnableViewState="false" Display="false" />
    <cms:CMSTextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="LogonTextBox"
        MaxLength="100"></cms:CMSTextBox > <br /> 
    <cms:CMSRequiredFieldValidator ID="rfvConfirmPassword" ValidationGroup="ConfirmRegForm" runat="server"
        ControlToValidate="txtConfirmPassword" Display="Dynamic"></cms:CMSRequiredFieldValidator>
</div>
