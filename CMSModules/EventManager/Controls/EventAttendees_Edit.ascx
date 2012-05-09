<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/EventManager/Controls/EventAttendees_Edit.ascx.cs"
    Inherits="CMSModules_EventManager_Controls_EventAttendees_Edit" %>
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
<asp:Panel ID="pnlContent" runat="server">
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblFirstName" AssociatedControlID="txtFirstName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtFirstName" runat="server" CssClass="TextBoxField" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblLastName" AssociatedControlID="txtLastName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtLastName" runat="server" CssClass="TextBoxField" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblEmail" AssociatedControlID="txtEmail" EnableViewState="false"
                    ResourceString="general.email" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtEmail" runat="server" CssClass="TextBoxField" MaxLength="250" />
                <cms:CMSRequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" AssociatedControlID="txtPhone" ID="lblPhone" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtPhone" runat="server" CssClass="TextBoxField" MaxLength="50" />&nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" EnableViewState="false" CssClass="SubmitButton"
                    OnClick="btnOK_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hdnDuplicit" runat="server" />
