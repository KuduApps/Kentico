<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Forums_Controls_Forums_ForumNew" CodeFile="ForumNew.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="vertical-align: top" cellpadding="2" class="NewForumForm">
    <tr>
        <td>
            <asp:Label runat="server" ID="lblForumDisplayName" CssClass="FieldLabel" EnableViewState="false" />
        </td>
        <td colspan="2">
            <cms:LocalizableTextBox ID="txtForumDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" />
            <cms:CMSRequiredFieldValidator ID="rfvForumDisplayName" runat="server" ErrorMessage=""
                ControlToValidate="txtForumDisplayName:textbox" Display="Dynamic" ValidationGroup="vgForum" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcCodeName" runat="Server">
        <tr runat="server">
            <td>
                <asp:Label runat="server" ID="lblForumName" CssClass="FieldLabel" EnableViewState="false" />
            </td>
            <td colspan="2">
                <cms:CMSTextBox ID="txtForumName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator Display="Dynamic" ID="rfvForumName" runat="server" ErrorMessage=""
                    ControlToValidate="txtForumName" ValidationGroup="vgForum" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
            <cms:LocalizedLabel runat="server" ID="lblForumDescription" CssClass="FieldLabel"
                EnableViewState="false" ResourceString="general.description" DisplayColon="true" />
        </td>
        <td colspan="2">
            <cms:LocalizableTextBox ID="txtForumDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcBaseAndUnsubUrl" runat="server" Visible="false">
        <tr>
            <td style="vertical-align: top; padding-top: 5px;">
                <asp:Label runat="server" ID="lblBaseUrl" CssClass="FieldLabel" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtBaseUrl" runat="server" CssClass="TextBoxField" MaxLength="200" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkInheritBaseUrl" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; padding-top: 5px;">
                <asp:Label runat="server" ID="lblUnsubscriptionUrl" CssClass="FieldLabel" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtUnsubscriptionUrl" runat="server" CssClass="TextBoxField" MaxLength="200" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkInheritUnsubscribeUrl" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="LeftColumnField">
            <asp:Label runat="server" ID="lblForumRequireEmail" CssClass="FieldLabel" EnableViewState="false" />
        </td>
        <td>
            <asp:CheckBox ID="chkForumRequireEmail" runat="server" CssClass="CheckBoxMovedLeft" />
        </td>
        <td>
            <asp:CheckBox ID="chkInheritForumRequireEmail" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="LeftColumnField">
            <asp:Label runat="server" ID="lblForumDisplayEmails" CssClass="FieldLabel" EnableViewState="false" />
        </td>
        <td>
            <asp:CheckBox ID="chkForumDisplayEmails" runat="server" CssClass="CheckBoxMovedLeft" />
        </td>
        <td>
            <asp:CheckBox ID="chkInheritForumDisplayEmails" runat="server" />
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcUseHtml">
        <tr>
            <td class="LeftColumnField">
                <asp:Label runat="server" ID="lblUseHTML" CssClass="FieldLabel" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox ID="chkUseHTML" runat="server" CssClass="CheckBoxMovedLeft" />
            </td>
            <td>
                <asp:CheckBox ID="chkInheritUseHTML" runat="server" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="LeftColumnField">
            <asp:Label runat="server" ID="lblCaptcha" CssClass="FieldLabel" EnableViewState="false" />
        </td>
        <td>
            <asp:CheckBox ID="chkCaptcha" runat="server" CssClass="CheckBoxMovedLeft" />
        </td>
        <td>
            <asp:CheckBox ID="chkInheritCaptcha" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="LeftColumnField">
            <asp:Label runat="server" ID="lblForumOpen" CssClass="FieldLabel" EnableViewState="false" />
        </td>
        <td colspan="2">
            <asp:CheckBox ID="chkForumOpen" runat="server" CssClass="CheckBoxMovedLeft" Checked="true" />
        </td>
    </tr>
    <tr>
        <td class="LeftColumnField">
            <asp:Label runat="server" ID="lblForumLocked" CssClass="FieldLabel" EnableViewState="false" />
        </td>
        <td colspan="2">
            <asp:CheckBox ID="chkForumLocked" runat="server" CssClass="CheckBoxMovedLeft" />
        </td>
    </tr>
    <tr>
        <td class="LeftColumnField">
            <asp:Label runat="server" ID="lblForumModerated" CssClass="FieldLabel" EnableViewState="false" />
        </td>
        <td colspan="2">
            <asp:CheckBox ID="chkForumModerated" runat="server" CssClass="CheckBoxMovedLeft" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td colspan="2">
            <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                CssClass="SubmitButton" ValidationGroup="vgForum" />
        </td>
    </tr>
</table>
<asp:Literal runat="server" ID="ltrScript" EnableViewState="false" />