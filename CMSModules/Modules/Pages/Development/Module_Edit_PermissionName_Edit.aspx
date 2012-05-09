<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Modules_Pages_Development_Module_Edit_PermissionName_Edit" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Module Edit - Permission Name Edit" CodeFile="Module_Edit_PermissionName_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent" EnableViewState="false">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lbPermissionDisplayName" runat="server" EnableViewState="false" DisplayColon="false"
                    AssociatedControlID="tbPermissionDisplayName" ResourceString="Administration-Module_Edit_PermissionName_Edit.PermissionDisplayName" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="tbPermissionDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="100" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvPermissionDisplayName" runat="server" ControlToValidate="tbPermissionDisplayName:textbox"
                    Display="dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lbPermissionCodeName" runat="server" EnableViewState="false" DisplayColon="false" 
                 AssociatedControlID="tbPermissionCodeName" ResourceString="Administration-Module_Edit_PermissionName_Edit.PermissionCodeName" />
            </td>
            <td>
                <cms:CMSTextBox ID="tbPermissionCodeName" runat="server" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvPermissionCodeName" runat="server" ControlToValidate="tbPermissionCodeName"
                    Display="dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel" style="vertical-align: top">
                <cms:LocalizedLabel ID="lblPermissionDescription" runat="server" EnableViewState="false" DisplayColon="false"
                    AssociatedControlID="txtPermissionDescription" ResourceString="Administration-Module_Edit_PermissionName_Edit.PermissionDescription" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtPermissionDescription" runat="server" EnableViewState="false"
                    TextMode="MultiLine" CssClass="TextAreaField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel" style="vertical-align: top">
                <cms:LocalizedLabel ID="lblPermissionDisplayInMatrix" runat="server" DisplayColon="true"
                    ResourceString="Administration-Module_Edit_PermissionName_Edit.DisplayInMatrix"
                    EnableViewState="false" AssociatedControlID="chkPermissionDisplayInMatrix" />
            </td>
            <td>
                <asp:CheckBox ID="chkPermissionDisplayInMatrix" runat="server" CssClass="CheckBoxMovedLeft"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel" style="vertical-align: top">
                <cms:LocalizedLabel ID="lblGlobalAdmin" runat="server" DisplayColon="true"
                    ResourceString="Administration-Module_Edit_PermissionName_Edit.GlobalAdmin"
                    EnableViewState="false" AssociatedControlID="chkGlobalAdmin" />
            </td>
            <td>
                <asp:CheckBox ID="chkGlobalAdmin" runat="server" CssClass="CheckBoxMovedLeft"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton"
                    EnableViewState="false" ResourceString="general.ok" />
            </td>
        </tr>
    </table>
</asp:Content>
