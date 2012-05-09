<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true"
    Inherits="CMSModules_CustomTables_CustomTable_Edit_General" Title="Custom table edit - General"
    Theme="Default" CodeFile="CustomTable_Edit_General.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <table>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblInfo" runat="server" Visible="false" CssClass="InfoLabel" EnableViewState="false" />
                <asp:Label ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblDisplayName" runat="server" CssClass="FieldLabel" EnableViewState="false"
                    ResourceString="customtable.newwizzard.DisplayName" />
            </td>
            <td colspan="3">
                <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" ControlToValidate="txtDisplayName:textbox"
                    runat="server" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblCodeName" runat="server" CssClass="FieldLabel" EnableViewState="false"
                    ResourceString="customtable.newwizzard.FullCodeName" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCodeNameNamespace" runat="server" CssClass="TextBoxField" MaxLength="49" />
            </td>
            <td>
                .
            </td>
            <td>
                <cms:CMSTextBox ID="txtCodeNameName" runat="server" CssClass="TextBoxField" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <cms:LocalizedLabel ID="lblNamespace" runat="server" ResourceString="customtable.newwizzard.NamespaceName"
                    EnableViewState="false" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvCodeNameNamespace" ControlToValidate="txtCodeNameNamespace"
                    runat="server" Display="Dynamic" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <cms:LocalizedLabel ID="lblName" runat="server" ResourceString="customtable.newwizzard.CodeName"
                    EnableViewState="false" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvCodeNameName" ControlToValidate="txtCodeNameName"
                    runat="server" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="3">
                <cms:CMSRegularExpressionValidator ID="revNameNamespace" runat="server" EnableViewState="false"
                    Display="dynamic" ControlToValidate="txtCodeNameNamespace" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="3">
                <cms:CMSRegularExpressionValidator ID="revCodeNameName" runat="server" EnableViewState="false"
                    Display="dynamic" ControlToValidate="txtCodeNameName" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblTableName" EnableViewState="false" ResourceString="class.TableName" />
            </td>
            <td colspan="3">
                <asp:Label runat="server" ID="lblTableNameValue" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblNewPage" EnableViewState="false" ResourceString="customtable.edit.NewPage" />
            </td>
            <td colspan="3">
                <cms:CMSTextBox runat="server" ID="txtNewPage" CssClass="TextBoxField" MaxLength="200" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblEditingPage" EnableViewState="false" ResourceString="customtable.edit.EditingPage" />
            </td>
            <td colspan="3">
                <cms:CMSTextBox runat="server" ID="txtEditingPage" CssClass="TextBoxField" MaxLength="200" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblViewPage" EnableViewState="false" ResourceString="customtable.edit.ViewPage" />
            </td>
            <td colspan="3">
                <cms:CMSTextBox runat="server" ID="txtViewPage" CssClass="TextBoxField" MaxLength="200" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblListPage" EnableViewState="false" ResourceString="customtable.edit.ListPage" />
            </td>
            <td colspan="3">
                <cms:CMSTextBox runat="server" ID="txtListPage" CssClass="TextBoxField" MaxLength="200" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="3">
                <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Click"
                    ResourceString="general.ok" EnableViewState="false" />
            </td>
        </tr>
    </table>
    <asp:Label ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
