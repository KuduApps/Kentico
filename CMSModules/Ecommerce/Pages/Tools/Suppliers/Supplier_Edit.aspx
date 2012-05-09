<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Suppliers_Supplier_Edit" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Suppliers - Edit" CodeFile="Supplier_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblSupplierDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtSupplierDisplayName" runat="server" CssClass="TextBoxField" MaxLength="50"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" ControlToValidate="txtSupplierDisplayName:textbox"
                    runat="server" Display="Dynamic" ValidationGroup="Suppliers" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblSupplierEmail" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtSupplierEmail" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblSupplierPhone" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtSupplierPhone" runat="server" CssClass="TextBoxField" MaxLength="50"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblSupplierFax" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtSupplierFax" runat="server" CssClass="TextBoxField" MaxLength="50"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblSupplierEnabled" EnableViewState="false"
                    ResourceString="general.enabled" DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkSupplierEnabled" runat="server" Checked="true" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ValidationGroup="Suppliers" />
            </td>
        </tr>
    </table>
</asp:Content>
