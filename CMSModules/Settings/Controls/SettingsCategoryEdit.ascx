<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SettingsCategoryEdit.ascx.cs"
    Inherits="CMSModules_Settings_Controls_SettingsCategoryEdit" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Settings/FormControls/SelectSettingsCategory.ascx"
    TagName="SettingsCategorySelector" TagPrefix="cms" %>
<asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="vertical-align: top">
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblCategoryDisplayName" runat="server" EnableViewState="false"
                ResourceString="general.displayname" AssociatedControlID="txtCategoryDisplayName"
                DisplayColon="true" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtCategoryDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200"
                ValidationGroup="vgCategory" EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvCategoryDisplayName" runat="server" ControlToValidate="txtCategoryDisplayName:textbox"
                ValidationGroup="vgCategory" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblCategoryName" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.codename" AssociatedControlID="txtCategoryName" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtCategoryName" runat="server" CssClass="TextBoxField" MaxLength="100"
                ValidationGroup="vgCategory" EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvCategoryName" runat="server" ControlToValidate="txtCategoryName"
                ValidationGroup="vgCategory" EnableViewState="false" />
        </td>
    </tr>
    <tr id="trIconPath" runat="server">
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblCategoryIconPath" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="settings.iconpath" AssociatedControlID="txtIconPath" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtIconPath" runat="server" CssClass="TextBoxField" MaxLength="200"
                EnableViewState="false" />
        </td>
    </tr>
    <tr id="trParentCategory" runat="server">
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblKeyCategory" runat="server" EnableViewState="false" ResourceString="settings.parentcategoryname"
                DisplayColon="true" AssociatedControlID="drpCategory:drpCategories" />
        </td>
        <td>
            <cms:SettingsCategorySelector ID="drpCategory" runat="server" DisplayOnlyCategories="false">
            </cms:SettingsCategorySelector>
        </td>
    </tr>
    <tr id="trIsCustom" runat="server">
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblIsCustom" runat="server" EnableViewState="false" ResourceString="settings.iscustom"
                DisplayColon="true" AssociatedControlID="chkIsCustom" />
        </td>
        <td>
            <cms:LocalizedCheckBox ID="chkIsCustom" runat="server" Checked="false" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:LocalizedButton ID="btnOk" runat="server" EnableViewState="false" CssClass="SubmitButton"
                ValidationGroup="vgCategory" ResourceString="General.OK" OnClick="btnOk_Click" />
        </td>
    </tr>
</table>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
