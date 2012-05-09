<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Membership_Pages_Users_User_Edit_General" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="User Edit - General" CodeFile="User_Edit_General.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SiteCultureSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/UserName.ascx" TagName="UserName"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:PlaceHolder ID="plcTable" runat="server">
        <table>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblUserName" runat="server" EnableViewState="false" ResourceString="general.username"
                        DisplayColon="true" ShowRequiredMark="true" />
                </td>
                <td>
                    <cms:UserName ID="ucUserName" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblFullName" runat="server" EnableViewState="false" ResourceString="Administration-User_Edit_General.FullName" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtFullName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                    <cms:CMSRequiredFieldValidator ID="rfvFullName" runat="server" EnableViewState="false"
                        ControlToValidate="txtFullName" Display="dynamic" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblFirstName" runat="server" EnableViewState="false" ResourceString="Administration-User_Edit_General.FirstName" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtFirstName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblMiddleName" runat="server" EnableViewState="false" ResourceString="Administration-User_Edit_General.MiddleName" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtMiddleName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblLastName" runat="server" EnableViewState="false" ResourceString="Administration-User_Edit_General.LastName" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtLastName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="LabelEmail" runat="server" EnableViewState="false" ResourceString="general.email"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtEmail" runat="server" CssClass="TextBoxField" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblEnabled" runat="server" EnableViewState="false" ResourceString="general.enabled"
                        DisplayColon="true" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBoxEnabled" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblIsEditor" runat="server" EnableViewState="false" ResourceString="Administration-User_Edit_General.IsEditor" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBoxIsEditor" runat="server" />
                </td>
            </tr>
            <asp:PlaceHolder ID="plcGlobalAdmin" runat="server">
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblIsGlobalAdministrator" runat="server" EnableViewState="false"
                            ResourceString="Administration-User_Edit_General.IsGlobalAdministrator" Visible="true" />
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBoxLabelIsGlobalAdministrator" runat="server" Visible="true" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblIsExternal" runat="server" EnableViewState="false" ResourceString="Administration-User_Edit_General.IsExternal" />
                </td>
                <td>
                    <asp:CheckBox ID="chkIsExternal" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblIsDomain" runat="server" EnableViewState="false" ResourceString="Administration-User_Edit_General.UserIsDomain"
                        DisplayColon="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkIsDomain" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblIsHidden" runat="server" EnableViewState="false" ResourceString="User_Edit_General.IsHidden"
                        DisplayColon="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkIsHidden" runat="server" />
                </td>
            </tr>
            <asp:PlaceHolder ID="plcSiteManagerDisabled" runat="server">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblSiteManagerDisabled" runat="server" ResourceString="adm.user.lblSiteManagerDisabled"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkSiteManagerDisabled" runat="server" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblCulture" runat="server" EnableViewState="false" ResourceString="Administration-User_Edit_General.Culture" />
                </td>
                <td>
                    <cms:SiteCultureSelector runat="server" ID="cultureSelector" IsLiveSite="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblUICulture" runat="server" EnableViewState="false" ResourceString="Administration-User_Edit_General.UICulture" />
                </td>
                <td>
                    <asp:DropDownList ID="lstUICulture" runat="server" CssClass="DropDownField" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblCreated" runat="server" EnableViewState="false" ResourceString="Administration-User_Edit_General.UserCreated" />
                </td>
                <td class="FieldLabel">
                    <asp:Label ID="lblCreatedInfo" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblLastLogon" runat="server" EnableViewState="false" ResourceString="Administration-User_Edit_General.LastLogon" />
                </td>
                <td class="FieldLabel">
                    <asp:Label ID="lblLastLogonTime" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblUserLastLogonInfo" runat="server" EnableViewState="false"
                        ResourceString="adm.user.lblUserLastLogonInfo" />
                </td>
                <td class="FieldLabel">
                    <asp:PlaceHolder runat="server" ID="plcUserLastLogonInfo" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblUserStartingPath" runat="server" EnableViewState="false"
                        ResourceString="Administration-User_Edit_General.UserStartingPath" />
                </td>
                <td class="FieldLabel">
                    <cms:CMSTextBox runat="server" ID="txtUserStartingPath" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <cms:LocalizedButton ID="btnOk" runat="server" OnClick="btnOk_Click" CssClass="SubmitButton"
                        EnableViewState="false" ResourceString="general.ok" />
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
</asp:Content>
