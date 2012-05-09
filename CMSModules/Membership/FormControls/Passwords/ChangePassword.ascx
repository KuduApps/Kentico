<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Membership_FormControls_Passwords_ChangePassword" CodeFile="ChangePassword.ascx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Passwords/PasswordStrength.ascx" TagName="PasswordStrength"
    TagPrefix="cms" %>
<asp:Panel ID="pnlChangePassword" runat="server" DefaultButton="btnOK" CssClass="PasswordPanel">
    <div>
        <asp:Label runat="server" ID="lblInfo" EnableViewState="false" />
        <asp:Label runat="server" ID="lblError" EnableViewState="false" ForeColor="Red" />
    </div>
    <table>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblExistingPassword" AssociatedControlID="txtExistingPassword"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtExistingPassword" runat="server" TextMode="Password" MaxLength="100"
                    CssClass="TextBoxField" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel FieldLabelTop">
                <asp:Label runat="server" ID="lblPassword1" AssociatedControlID="passStrength" EnableViewState="false" />
            </td>
            <td>
                <cms:PasswordStrength runat="server" ID="passStrength" />     
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblPassword2" AssociatedControlID="txtPassword2" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtPassword2" runat="server" TextMode="Password" MaxLength="100"
                    CssClass="TextBoxField" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="LongSubmitButton" ValidationGroup="ChangePassword" />
            </td>
        </tr>
    </table>
</asp:Panel>
