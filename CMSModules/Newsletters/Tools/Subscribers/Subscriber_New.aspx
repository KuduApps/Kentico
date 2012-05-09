<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Subscribers_Subscriber_New"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Tools - Newsletter subscriber edit" CodeFile="Subscriber_New.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblSubscriberEmail" EnableViewState="false"
                    ResourceString="general.email" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtSubscriberEmail" runat="server" CssClass="TextBoxField" MaxLength="400" />
                <cms:CMSRequiredFieldValidator ID="rfvSubscriberEmail" runat="server" ControlToValidate="txtSubscriberEmail"
                    Display="dynamic" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblSubscriberFirstName" EnableViewState="false" /></td>
            <td>
                <cms:CMSTextBox ID="txtSubscriberFirstName" runat="server" CssClass="TextBoxField" MaxLength="200" /></td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblSubscriberLastName" EnableViewState="false" /></td>
            <td>
                <cms:CMSTextBox ID="txtSubscriberLastName" runat="server" CssClass="TextBoxField" MaxLength="200" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" /></td>
        </tr>
    </table>
</asp:Content>
