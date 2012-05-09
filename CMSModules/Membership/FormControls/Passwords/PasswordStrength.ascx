<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PasswordStrength.ascx.cs"
    Inherits="CMSModules_Membership_FormControls_Passwords_PasswordStrength" %>
<cms:CMSTextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="LogonTextBox" /><asp:Label
    ID="lblRequiredFieldMark" runat="server" Text="" Visible="false" />
<div class="PasswordStrengthText">
    <cms:LocalizedLabel runat="server" ID="lblPasswStregth" CssClass="PasswordStrengthHint"
        ResourceString="Membership.PasswordStrength" />
    <asp:Label runat="server" ID="lblEvaluation" EnableViewState="false" />
</div>
<asp:Panel runat="server" ID="pnlPasswStrengthIndicator" CssClass="PasswStrenghtIndicator">
    <asp:Panel runat="server" ID="pnlPasswIndicator">
        &nbsp;
    </asp:Panel>
</asp:Panel>
<cms:CMSRequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
    Display="Dynamic" EnableViewState="false" />
