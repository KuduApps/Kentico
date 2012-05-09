<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_OrderStatus_OrderStatus_Edit"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="E-commerce Configuration - Order status properties"
    CodeFile="OrderStatus_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblStatusDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtStatusDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvStatusDisplayName" runat="server" ErrorMessage=""
                    ControlToValidate="txtStatusDisplayName:textbox" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblStatusName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtStatusName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvStatusName" runat="server" ErrorMessage="" ControlToValidate="txtStatusName"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblStatusColor" EnableViewState="false" />
            </td>
            <td>
                <cms:ColorPicker ID="clrPicker" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblStatusEmail" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox ID="chkStatusEmail" runat="server" CssClass="CheckBoxMovedLeft" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblStatusEnabled" EnableViewState="false"
                    ResourceString="general.enabled" DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkStatusEnabled" runat="server" CssClass="CheckBoxMovedLeft" Checked="true"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ResourceString="OrderStatus_Edit.MarkOrderAsPaid"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkMarkOrderAsPaid" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" />
            </td>
        </tr>
    </table>
</asp:Content>
