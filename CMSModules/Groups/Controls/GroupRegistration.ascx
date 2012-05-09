<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Groups_Controls_GroupRegistration" CodeFile="GroupRegistration.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<asp:PlaceHolder ID="plcForm" runat="server">
    <table class="GroupRegistration">
        <tr>
            <td>
                <asp:Label ID="lblDisplayName" runat="server" AssociatedControlID="txtDisplayName" CssClass="FieldLabel" EnableViewState="false" /></td>
            <td>
                <cms:LocalizableTextBox ID="txtDisplayName" runat="server" MaxLength="200" CssClass="TextBoxField" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                    Display="Dynamic" ValidationGroup="GroupEdit" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDescription" AssociatedControlID="txtDescription" runat="server" CssClass="FieldLabel" EnableViewState="false" /></td>
            <td>
                <cms:CMSTextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblApproveMembers" runat="server" CssClass="FieldLabel" EnableViewState="false" /></td>
            <td>
                <asp:RadioButton ID="radMembersAny" runat="server" GroupName="approvemembers" Checked="true" /><br />
                <asp:RadioButton ID="radMembersApproved" runat="server" GroupName="approvemembers" /><br />
                <asp:RadioButton ID="radMembersInvited" runat="server" GroupName="approvemembers" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblContentAccess" runat="server" CssClass="FieldLabel" EnableViewState="false" /></td>
            <td>
                <asp:RadioButton ID="radAnybody" runat="server" GroupName="contentaccess" Checked="true" /><br />
                <asp:RadioButton ID="radSiteMembers" runat="server" GroupName="contentaccess" /><br />
                <asp:RadioButton ID="radGroupMembers" runat="server" GroupName="contentaccess" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <cms:CMSButton ID="btnSave" runat="server" CssClass="ContentButton" OnClick="btnSave_Click"
                    ValidationGroup="GroupEdit" EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:PlaceHolder>
