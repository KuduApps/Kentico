<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_General"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Title="Customer properties"
    CodeFile="Customer_Edit_General.aspx.cs" %>

<%@ Register Src="~/CMSModules/ECommerce/FormControls/PaymentSelector.ascx" TagName="PaymentSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ECommerce/FormControls/ShippingSelector.ascx" TagName="ShippingSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ECommerce/FormControls/CurrencySelector.ascx" TagName="CurrencySelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/CountrySelector.ascx" TagName="CountrySelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ECommerce/FormControls/DiscountLevelSelector.ascx"
    TagName="DiscountLevelSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Passwords/PasswordStrength.ascx" TagName="PasswordStrength"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <div style="width: 700px;">
        <%-- General information --%>
        <asp:Panel ID="pnlGeneral" runat="server">
            <table style="vertical-align: top">
                <tr>
                    <td class="FieldLabel" style="width: 175px;">
                        <asp:Label runat="server" ID="lblCustomerFirstName" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtCustomerFirstName" runat="server" CssClass="TextBoxField" MaxLength="200"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label runat="server" ID="lblCustomerLastName" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtCustomerLastName" runat="server" CssClass="TextBoxField" MaxLength="200"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label runat="server" ID="lblCustomerCompany" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtCustomerCompany" runat="server" CssClass="TextBoxField" MaxLength="200"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label runat="server" ID="lblOrganizationID" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtOraganizationID" runat="server" CssClass="TextBoxField" MaxLength="50"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label runat="server" ID="lblTaxRegistrationID" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtTaxRegistrationID" runat="server" CssClass="TextBoxField" MaxLength="50"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel" style="vertical-align: top; padding-top: 6px">
                        <asp:Label runat="server" ID="lblCustomerCountry" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CountrySelector ID="drpCountry" runat="server" AddNoneRecord="true" UseCodeNameForSelection="false"
                            AddSelectCountryRecord="false" IsLiveSite="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <%-- Contacts --%>
        <asp:Panel ID="pnlContacts" runat="server">
            <table style="vertical-align: top">
                <tr>
                    <td class="FieldLabel" style="width: 175px;">
                        <cms:LocalizedLabel runat="server" ID="lblCustomerEmail" DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtCustomerEmail" runat="server" CssClass="TextBoxField" MaxLength="200"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label runat="server" ID="lblCustomerPhone" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtCustomerPhone" runat="server" CssClass="TextBoxField" MaxLength="50"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel runat="server" ID="lblCustomerFax" DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtCustomerFax" runat="server" CssClass="TextBoxField" MaxLength="50"
                            EnableViewState="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <%-- User info--%>
        <asp:Panel ID="pnlUserInfo" runat="server">
            <asp:Panel ID="pnlEdit" runat="server">
                <table style="vertical-align: top">
                    <tr>
                        <td class="ContentGroupHeader" colspan="2">
                            <cms:LocalizedLabel runat="server" ID="lblLogin" EnableViewState="false" ResourceString="com.customeredit.login" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel" colspan="2">
                            <asp:CheckBox ID="chkHasLogin" runat="server" CssClass="CheckBoxMovedLeft" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="loginLine1">
                        <td class="FieldLabel" style="width: 175px;">
                            <cms:LocalizedLabel runat="server" ID="lblUserName" EnableViewState="false" ResourceString="general.username"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtUserName" runat="server" CssClass="TextBoxField" MaxLength="100"
                                EnableViewState="false" />
                        </td>
                        <td>
                            <cms:CMSRequiredFieldValidator ID="rqvUserName" runat="server" ControlToValidate="txtUserName"
                                ValidationGroup="Login" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr id="loginLine2">
                        <td class="FieldLabel FieldLabelTop">
                            <asp:Label runat="server" ID="lblPassword1" EnableViewState="false" />
                        </td>
                        <td>
                            <cms:PasswordStrength runat="server" ID="passStrength" AllowEmpty="true"/> 
                        </td>
                        <td>
                        </td>                        
                    </tr>
                    <tr id="loginLine3">
                        <td class="FieldLabel">
                            <asp:Label runat="server" ID="lblPassword2" EnableViewState="false" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtPassword2" runat="server" CssClass="TextBoxField" TextMode="Password"
                                MaxLength="100" EnableViewState="false" />
                        </td>
                        <td>
                            <cms:CMSRequiredFieldValidator ID="rqvPassword2" runat="server" ControlToValidate="txtPassword2"
                                ValidationGroup="Login" EnableViewState="false" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlStatic" runat="server">
                <table style="vertical-align: top">
                    <tr>
                        <td class="ContentGroupHeader" colspan="2">
                            <cms:LocalizedLabel runat="server" ID="lblLogin1" EnableViewState="false" ResourceString="com.customeredit.login" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel" nowrap="nowrap" style="width: 175px;">
                            <cms:LocalizedLabel ID="lblUserName1" runat="server" EnableViewState="false" ResourceString="general.username"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblUserNameStaticValue" EnableViewState="false" />
                        </td>
                        <td>
                            <cms:CMSButton ID="btnEditUser" runat="server" CssClass="ContentButton" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <cms:LocalizedLabel runat="server" ID="lblCustomerEnabled" EnableViewState="false"
                                ResourceString="general.enabled" DisplayColon="true" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCustomerEnabled" runat="server" CssClass="CheckBoxMovedLeft"
                                EnableViewState="false" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="vertical-align: top">
                <asp:PlaceHolder runat="server" ID="plcPreferences">
                    <tr>
                        <td class="ContentGroupHeader" colspan="2">
                            <cms:LocalizedLabel runat="server" ID="lblPreferences" EnableViewState="false" ResourceString="com.customeredit.preferences" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel" style="width: 175px;">
                            <asp:Label runat="server" ID="lblCustomerPreferredCurrency" EnableViewState="false" />
                        </td>
                        <td>
                            <cms:CurrencySelector ID="drpCurrency" runat="server" AddNoneRecord="true" IsLiveSite="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <asp:Label runat="server" ID="lblCustomerPrefferedPaymentOption" EnableViewState="false" />
                        </td>
                        <td>
                            <cms:PaymentSelector ID="drpPayment" runat="server" AddNoneRecord="true" IsLiveSite="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <asp:Label runat="server" ID="lblCustomerPreferredShippingOption" EnableViewState="false" />
                        </td>
                        <td>
                            <cms:ShippingSelector ID="drpShipping" runat="server" AddNoneRecord="true" IsLiveSite="false" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plcDiscounts" runat="server">
                    <tr>
                        <td class="ContentGroupHeader" colspan="2">
                            <cms:LocalizedLabel runat="server" ID="lblDiscounts" EnableViewState="false" ResourceString="com.customeredit.discounts" />
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="plcGlobalDiscount" runat="server">
                        <tr>
                            <td class="FieldLabel" style="width: 175px;">
                                <asp:Label runat="server" ID="lblCustomerGlobalDiscountLevel" EnableViewState="false" />
                            </td>
                            <td>
                                <cms:DiscountLevelSelector ID="drpGlobalDiscountLevel" runat="server" AddNoneRecord="true"
                                    UseCodeNameForSelection="false" IsLiveSite="false" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plcSiteDiscount" runat="server">
                        <tr>
                            <td class="FieldLabel">
                                <asp:Label runat="server" ID="lblCustomerDiscountLevel" EnableViewState="false" />
                            </td>
                            <td>
                                <cms:DiscountLevelSelector ID="drpDiscountLevel" runat="server" AddNoneRecord="false"
                                    UseCodeNameForSelection="false" IsLiveSite="false" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </asp:PlaceHolder>
            </table>
        </asp:Panel>
        <table style="vertical-align: top; margin-top: 10px;">
            <tr>
                <td>
                    <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                        CssClass="SubmitButton" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
