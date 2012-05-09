<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Pages_Users_User_New"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Users - New User" CodeFile="User_New.aspx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/UserName.ascx" TagName="UserName" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Passwords/PasswordStrength.ascx" TagName="PasswordStrength"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:localizedlabel id="lblUserName" runat="server" enableviewstate="false" resourcestring="general.username"
                    displaycolon="true" showrequiredmark="true" />
            </td>
            <td>
                <cms:UserName ID="ucUserName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="LabelFullName" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="TextBoxFullName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorFullName" runat="server" EnableViewState="false"
                    ControlToValidate="TextBoxFullName" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:localizedlabel id="LabelEmail" runat="server" enableviewstate="false" resourcestring="general.email"
                    displaycolon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="TextBoxEmail" runat="server" CssClass="TextBoxField" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:localizedlabel id="lblEnabled" runat="server" enableviewstate="false" resourcestring="general.enabled"
                    displaycolon="true" />
            </td>
            <td>
                <asp:CheckBox ID="CheckBoxEnabled" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="LabelIsEditor" runat="server" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox ID="CheckBoxIsEditor" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel FieldLabelTop">
                <asp:Label ID="LabelPassword" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:PasswordStrength runat="server" ID="passStrength" AllowEmpty="true" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="LabelConfirmPassword" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="TextBoxConfirmPassword" runat="server" TextMode="password" CssClass="TextBoxField"
                    MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton ID="ButtonOK" runat="server" OnClick="ButtonOK_Click" CssClass="SubmitButton"
                    EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Content>
