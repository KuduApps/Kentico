<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Forums_Controls_SubscriptionForm" CodeFile="SubscriptionForm.ascx.cs" %>
<asp:Panel runat="server" ID="pnlPadding" CssClass="FormPadding" DefaultButton="btnOK">
    <asp:Label runat="server" ID="lblError"  CssClass="ErrorLabel" ForeColor="Red" EnableViewState="false"
        Visible="false" />
    <table class="PostForm">
        <tr>
            <td class="ItemLabel">
                <cms:LocalizedLabel ID="lblEmail" runat="server" EnableViewState="false" ResourceString="general.email"
                    DisplayColon="true" AssociatedControlID="txtEmail" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtEmail" runat="server" CssClass="TextboxItemShort" MaxLength="100" />
                <cms:CMSRegularExpressionValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    Display="Dynamic" ValidationGroup="NewSubscription" />
                <cms:CMSRequiredFieldValidator ID="rfvEmailRequired" runat="server" ControlToValidate="txtEmail"
                    Display="Dynamic" ValidationGroup="NewSubscription" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" ValidationGroup="NewSubscription"
                    OnClick="btnOK_Click" />
                <cms:CMSButton ID="btnCancel" runat="server" CssClass="SubmitButton" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
