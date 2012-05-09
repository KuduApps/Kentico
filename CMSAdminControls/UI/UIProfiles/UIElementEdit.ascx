<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_UIProfiles_UIElementEdit"
    CodeFile="UIElementEdit.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/SelectUIElement.ascx" TagName="SelectUIElement"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/SelectCMSVersion.ascx" TagName="SelectCMSVersion"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextbox"
    TagPrefix="cms" %>
<cms:LocalizedLabel ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblDisplayName" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="resource.ui.displayname" />
        </td>
        <td>
            <cms:LocalizableTextbox ID="txtDisplayName" runat="server" CssClass="TextBoxField"
                MaxLength="200" EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                ValidationGroup="UIProfile" Display="Dynamic" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblName" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="resource.ui.name" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtName" runat="server" CssClass="TextBoxField" MaxLength="200"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                ValidationGroup="UIProfile" Display="Dynamic" EnableViewState="false" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcParentElem" runat="server">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblParentElem" runat="server" EnableViewState="false" DisplayColon="true"
                    ResourceString="resource.ui.parent" />
            </td>
            <td>
                <cms:SelectUIElement ID="elemSelector" runat="server" CssClass="ContentDropdown"
                    UseElementNameForSelection="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblCustomElem" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="resource.ui.customelem" />
        </td>
        <td>
            <asp:CheckBox ID="chkCustom" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <strong>
                <cms:LocalizedLabel ID="lblMenuItems" runat="server" EnableViewState="false" ResourceString="uiprofiles.menuitem" />
            </strong>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblCaption" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="resource.ui.caption" />
        </td>
        <td>
            <cms:LocalizableTextbox ID="txtCaption" runat="server" CssClass="TextBoxField" MaxLength="200"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblTargetURL" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="resource.ui.targeturl" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtTargetURL" runat="server" CssClass="TextBoxField" MaxLength="650"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblIconPath" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="resource.ui.iconpath" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtIconPath" runat="server" CssClass="TextBoxField" MaxLength="200"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td valign="top">
            <cms:LocalizedLabel ID="lblDescription" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.description" />
        </td>
        <td>
            <cms:LocalizableTextbox ID="txtDescription" runat="server" CssClass="TextAreaField" TextMode="MultiLine"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblSize" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.size" />
        </td>
        <td>
            <cms:LocalizedRadioButton runat="server" ID="radLarge" ResourceString="resource.ui.sizelarge"
                EnableViewState="false" Checked="true" GroupName="ElementSize" />
            <cms:LocalizedRadioButton runat="server" ID="radRegular" ResourceString="resource.ui.sizeregular"
                EnableViewState="false" Checked="false" GroupName="ElementSize" />
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcCMSVersion">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblVersion" runat="server" EnableViewState="false" DisplayColon="true"
                    ResourceString="resource.ui.fromversion" />
            </td>
            <td>
                <cms:SelectCMSVersion runat="server" ID="versionSelector" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
        </td>
        <td>
            <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.ok" CssClass="SubmitButton"
                OnClick="btnOk_Click" ValidationGroup="UIProfile" EnableViewState="false" />
        </td>
    </tr>
</table>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />