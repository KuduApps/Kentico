<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Forums_Controls_Groups_GroupNew" CodeFile="GroupNew.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="vertical-align: top" class="NewGroupForm">
    <tr>
        <td class="FieldLabel">
            <asp:Label runat="server" ID="lblGroupDisplayName" EnableViewState="false" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtGroupDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" />
            <cms:CMSRequiredFieldValidator ID="rfvGroupDisplayName" runat="server" ControlToValidate="txtGroupDisplayName:textbox"
                ValidationGroup="vgForumGroup" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcCodeName" runat="server">
        <tr runat="server">
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblGroupName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtGroupName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvGroupName" runat="server" ControlToValidate="txtGroupName"
                    ValidationGroup="vgForumGroup" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td style="vertical-align: top; padding-top: 5px" class="FieldLabel">
            <cms:LocalizedLabel runat="server" ID="lblDescription" EnableViewState="false" ResourceString="general.description"
                DisplayColon="true" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtGroupDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcBaseAndUnsubUrl" runat="server" Visible="false">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblForumBaseUrl" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtForumBaseUrl" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:LocalizedCheckBox runat="server" ID="chkInheritBaseUrl" Checked="true" ResourceString="Forums.InheritBaseUrl" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblUnsubscriptionUrl" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtUnsubscriptionUrl" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:LocalizedCheckBox runat="server" ID="chkInheritUnsubUrl" Checked="true" ResourceString="Forums.InheritUnsubsUrl" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton ID="btnOk" runat="server" EnableViewState="false" OnClick="btnOK_Click"
                CssClass="SubmitButton" ValidationGroup="vgForumGroup" />
        </td>
    </tr>
</table>
