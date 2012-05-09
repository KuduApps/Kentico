<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_DiscountCoupons_DiscountCoupon_New"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Discount - New"
    CodeFile="DiscountCoupon_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDiscountCouponDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtDiscountCouponDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="200" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" Display="Dynamic"
                    ValidationGroup="Discount" ControlToValidate="txtDiscountCouponDisplayName:textbox" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDiscountCouponCode" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtDiscountCouponCode" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvCouponCode" runat="server" Display="Dynamic" ValidationGroup="Discount"
                    ControlToValidate="txtDiscountCouponCode" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:RadioButton runat="server" ID="radFixed" GroupName="radValues" Checked="true" />
                <asp:RadioButton runat="server" ID="radPercentage" GroupName="radValues" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDiscountCouponValue" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtDiscountCouponValue" runat="server" CssClass="TextBoxField" MaxLength="10"
                    EnableViewState="false" />&nbsp;<asp:Label ID="lblCurrency" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDiscountCouponValidFrom" EnableViewState="false" />
            </td>
            <td>
                <cms:DateTimePicker ID="dtPickerDiscountCouponValidFrom" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDiscountCouponValidTo" EnableViewState="false" />
            </td>
            <td>
                <cms:DateTimePicker ID="dtPickerDiscountCouponValidTo" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ValidationGroup="Discount" />
            </td>
        </tr>
    </table>
</asp:Content>
