<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Manufacturers_Manufacturer_Edit" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Manufacturers - Edit" CodeFile="Manufacturer_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblManufacturerDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtManufacturerDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="200" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" ControlToValidate="txtManufacturerDisplayName:textbox"
                    runat="server" Display="Dynamic" ValidationGroup="Manufacturers" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblManufactureHomepage" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtManufactureHomepage" runat="server" CssClass="TextBoxField" MaxLength="400"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblManufacturerEnabled" EnableViewState="false"
                    ResourceString="general.enabled" DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkManufacturerEnabled" runat="server" CssClass="CheckBoxMovedLeft"
                    EnableViewState="false" Checked="true" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ValidationGroup="Manufacturers" />
            </td>
        </tr>
    </table>
</asp:Content>
