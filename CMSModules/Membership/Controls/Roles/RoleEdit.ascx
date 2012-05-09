<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Controls_Roles_RoleEdit" CodeFile="RoleEdit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblRoleDisplayName" runat="server" EnableViewState="false"
                ResourceString="Administration-Role_Edit_General.DisplayName" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtRoleDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100" />&nbsp;<cms:CMSRequiredFieldValidator
                ID="rfvDisplayName" runat="server" EnableViewState="false" ControlToValidate="txtRoleDisplayName:textbox"
                Display="dynamic" ValidationGroup="clickOK" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcCodeName" runat="server">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblRoleCodeName" runat="server" EnableViewState="false" ResourceString="Administration-Role_Edit_General.RoleCodeName" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtRoleCodeName" runat="server" CssClass="TextBoxField" MaxLength="100" />&nbsp;<cms:CMSRequiredFieldValidator
                    ID="rfvCodeName" runat="server" EnableViewState="false" ControlToValidate="txtRoleCodeName"
                    Display="dynamic" ValidationGroup="clickOK" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="FieldLabel" style="vertical-align: top;">
            <cms:LocalizedLabel ID="lblDescription" runat="server" EnableViewState="false" ResourceString="Administration-Role_New.Description" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtDescription" runat="server" CssClass="TextAreaField" TextMode="MultiLine" />
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcIsDomain">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblIsDomain" runat="server" EnableViewState="false" ResourceString="Administration-Role_Edit_General.RoleIsDomain"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkIsDomain" runat="server" CssClass="CheckBoxMovedLeft" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcIsPublic" Visible="false">
        <tr>
            <td class="FieldLabel" style="vertical-align: top">
                <cms:LocalizedLabel ID="lblIsAdmin" runat="server" EnableViewState="false" ResourceString="groups.role.isgroupadmin"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkIsAdmin" CssClass="CheckBoxMovedLeft" runat="server" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
        </td>
        <td>
            <cms:LocalizedButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton"
                EnableViewState="false" ValidationGroup="clickOK" ResourceString="general.ok" />
        </td>
    </tr>
</table>
