<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SettingsKeyEdit.ascx.cs"
    Inherits="CMSModules_Settings_Controls_SettingsKeyEdit" %>
<%@ Register Src="~/CMSAdminControls/UI/Selectors/LoadGenerationSelector.ascx" TagName="LoadGenerationSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Settings/FormControls/SelectSettingsCategory.ascx"
    TagName="SettingsCategorySelector" TagPrefix="cms" %>
<asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblKeyDisplayName" runat="server" EnableViewState="false"
                ResourceString="general.displayname" DisplayColon="true" AssociatedControlID="txtKeyDisplayName" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtKeyDisplayName" runat="server" CssClass="TextBoxField"
                MaxLength="200" ValidationGroup="vgKey" EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvKeyDisplayName" runat="server" Display="Dynamic"
                ControlToValidate="txtKeyDisplayName:textbox" ValidationGroup="vgKey" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblKeyName" runat="server" EnableViewState="false" ResourceString="general.codename"
                DisplayColon="true" AssociatedControlID="txtKeyName" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtKeyName" runat="server" CssClass="TextBoxField" MaxLength="100"
                ValidationGroup="vgKey" EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvKeyName" runat="server" Display="Dynamic" ControlToValidate="txtKeyName"
                ValidationGroup="vgKey" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblKeyDescription" runat="server" EnableViewState="false"
                ResourceString="general.description" DisplayColon="true" AssociatedControlID="txtKeyDescription" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtKeyDescription" runat="server" CssClass="TextAreaField"
                TextMode="MultiLine" EnableViewState="false" />
        </td>
    </tr>
    <tr id="trCategory" runat="server">
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblKeyCategory" runat="server" EnableViewState="false" ResourceString="settings.groupname"
                DisplayColon="true" AssociatedControlID="drpCategory:drpCategories" />
        </td>
        <td>
            <cms:SettingsCategorySelector ID="drpCategory" runat="server" DisplayOnlyCategories="false"
                IsKeyEdit="true"></cms:SettingsCategorySelector>
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblKeyType" runat="server" EnableViewState="false" ResourceString="settings.datatype"
                DisplayColon="true" AssociatedControlID="drpKeyType" />
        </td>
        <td>
            <asp:DropDownList ID="drpKeyType" runat="server" AutoPostBack="true" CssClass="DropDownField"
                OnSelectedIndexChanged="drpKeyType_SelectedIndexChanged" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblKeyValue" runat="server" EnableViewState="false" ResourceString="settings.defaultvalue"
                DisplayColon="true" AssociatedControlID="txtKeyValue" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtKeyValue" runat="server" CssClass="TextBoxField" EnableViewState="false" />
            <asp:CheckBox ID="chkKeyValue" runat="server" EnableViewState="false" />
            <asp:Label ID="lblDefValueError" runat="server" CssClass="ErrorLabel" Style="display: inline;"
                EnableViewState="false" Visible="false" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblKeyValidation" runat="server" EnableViewState="false"
                ResourceString="settings.validationexpresion" DisplayColon="true" AssociatedControlID="txtKeyValidation" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtKeyValidation" runat="server" MaxLength="255" CssClass="TextBoxField"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblFormControl" runat="server" EnableViewState="false" ResourceString="settings.controlpath"
                DisplayColon="true" AssociatedControlID="txtFormControl" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtFormControl" runat="server" CssClass="TextBoxField" MaxLength="200"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblKeyIsGlobal" runat="server" EnableViewState="false" ResourceString="settings.keyisglobal"
                DisplayColon="true" AssociatedControlID="chkKeyIsGlobal" />
        </td>
        <td>
            <asp:CheckBox ID="chkKeyIsGlobal" runat="server" />
        </td>
    </tr>
    <tr id="trKeyIsHidden" runat="server">
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblKeyIsHidden" runat="server" EnableViewState="false" ResourceString="settings.keyishidden"
                DisplayColon="true" AssociatedControlID="chkKeyIsHidden" />
        </td>
        <td>
            <asp:CheckBox ID="chkKeyIsHidden" runat="server" />
        </td>
    </tr>
    <tr id="trLoadGeneration" runat="server">
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblLoadGeneration" runat="server" EnableViewState="false"
                ResourceString="settings.cacheobjecton" DisplayColon="true" AssociatedControlID="drpGeneration:drpGeneration" />
        </td>
        <td>
            <cms:LoadGenerationSelector ID="drpGeneration" runat="server" NoChangeOption="true" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:LocalizedButton ID="btnOk" runat="server" EnableViewState="false" CssClass="SubmitButton"
                ValidationGroup="vgKey" ResourceString="General.OK" OnClick="btnOK_Click" />
        </td>
    </tr>
</table>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
