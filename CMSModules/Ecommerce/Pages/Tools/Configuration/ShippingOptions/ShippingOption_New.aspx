<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_New"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="ShippingOption_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/Ecommerce/Controls/UI/PriceSelector.ascx" TagName="PriceSelector"
    TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblShippingOptionDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtShippingOptionDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="200" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtShippingOptionDisplayName:textbox"
                    Display="Dynamic" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblShippingOptionName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtShippingOptionName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtShippingOptionName"
                    Display="Dynamic" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblShippingOptionCharge" EnableViewState="false" />
            </td>
            <td>
                <cms:PriceSelector ID="txtShippingOptionCharge" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblShippingOptionEnabled" EnableViewState="false"
                    ResourceString="general.enabled" DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkShippingOptionEnabled" runat="server" CssClass="CheckBoxMovedLeft"
                    Checked="True" EnableViewState="false" />
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
