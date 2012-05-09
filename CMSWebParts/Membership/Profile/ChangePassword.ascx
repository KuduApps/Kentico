<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSWebParts_Membership_Profile_ChangePassword" CodeFile="~/CMSWebParts/Membership/Profile/ChangePassword.ascx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Passwords/PasswordStrength.ascx" TagName="PasswordStrength"
    TagPrefix="cms" %>    
<asp:Panel ID="pnlWebPart" runat="server" DefaultButton="btnOk">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table class="ChangePasswordTable">
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblOldPassword" AssociatedControlID="txtOldPassword" runat="server" />
            </td>
            <td class="FieldInput">
                <cms:CMSTextBox ID="txtOldPassword" runat="server" TextMode="Password" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel FieldLabelTop" >
                <cms:LocalizedLabel ID="lblNewPassword" runat="server" />
            </td>
            <td class="FieldInput">            
                <cms:PasswordStrength runat="server" ID="passStrength" TextBoxClass="" AllowEmpty="true"/> 
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblConfirmPassword" AssociatedControlID="txtConfirmPassword" runat="server" />
            </td>
            <td class="FieldInput">
                <cms:CMSTextBox ID="txtConfirmPassword" runat="server" TextMode="Password" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="ChangeButton">
                <br />
                <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOk_Click" CssClass="LongSubmitButton" ValidationGroup="PasswordChange" />
            </td>
        </tr>
    </table>
</asp:Panel>
