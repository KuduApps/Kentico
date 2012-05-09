<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_PaymentOptions_PaymentOption_Edit"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="PaymentOption_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/FormControls/OrderStatusSelector.ascx" TagName="OrderStatusSelector"
    TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <%-- Display name --%>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblPaymentOptionDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtPaymentOptionDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="200" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" ControlToValidate="txtPaymentOptionDisplayName:textbox"
                    runat="server" Display="Dynamic" ValidationGroup="PaymentOptions" EnableViewState="false" />
            </td>
        </tr>
        <%-- Code name --%>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblPaymentOptionName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtPaymentOptionName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvCodeName" ControlToValidate="txtPaymentOptionName"
                    runat="server" Display="Dynamic" ValidationGroup="PaymentOptions" EnableViewState="false" />
            </td>
        </tr>
        <%-- Allow if no shipping is supplied --%>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblAllowIfNoShipping" ResourceString="paymentoption_edit.allowifnoshipping"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkAllowIfNoShipping" Checked="false" />
            </td>
        </tr>
        <%-- Enabled --%>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblPaymentOptionEnabled" EnableViewState="false"
                    ResourceString="general.enabled" DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkPaymentOptionEnabled" runat="server" CssClass="CheckBoxMovedLeft"
                    Checked="true" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-top: 10px; font-weight: bold;">
                <asp:Label runat="server" ID="lblPaymentGateway" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblGateUrl" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtGateUrl" runat="server" CssClass="TextBoxField" MaxLength="500"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblPaymentAssemblyName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtPaymentAssemblyName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblPaymentClassName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtPaymentClassName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblStatusSucceed" EnableViewState="false" />
            </td>
            <td colspan="2">
                <cms:OrderStatusSelector runat="server" ID="succeededElem" AddAllItemsRecord="false"
                    AddNoneRecord="true" IsLiveSite="false" UseStatusNameForSelection="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblStatusFailed" EnableViewState="false" />
            </td>
            <td colspan="2">
                <cms:OrderStatusSelector runat="server" ID="failedElem" AddAllItemsRecord="false"
                    AddNoneRecord="true" IsLiveSite="false" UseStatusNameForSelection="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ValidationGroup="PaymentOptions" />
            </td>
        </tr>
    </table>
</asp:Content>
