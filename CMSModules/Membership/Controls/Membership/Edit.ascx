<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Controls_Membership_Edit"
    CodeFile="Edit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="vertical-align: top">
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblMembershipName" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="membership.membershipname" AssociatedControlID="txtMembershipName" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtMembershipName" runat="server" CssClass="TextBoxField" MaxLength="200"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvMembershipName" runat="server" Display="Dynamic"
                ControlToValidate="txtMembershipName:textbox" ValidationGroup="vgMembership" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblMembershipCodeName" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="membership.membershipcodename" AssociatedControlID="txtMembershipName" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtMembershipCodeName" runat="server" CssClass="TextBoxField" MaxLength="200"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvMembershipCodeName" runat="server" Display="Dynamic"
                ControlToValidate="txtMembershipCodeName" ValidationGroup="vgMembership" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblMembershipDescription" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="membership.description" AssociatedControlID="txtMembershipDescription" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtMembershipDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton runat="server" ID="btnOk" EnableViewState="false" CssClass="SubmitButton"
                OnClick="btnOk_Click" ValidationGroup="vgMembership" />
        </td>
    </tr>
</table>
