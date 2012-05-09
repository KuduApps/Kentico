<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_MyDetails_MyDetails"
    CodeFile="MyDetails.ascx.cs" %>
<%@ Register Src="~/CMSModules/ECommerce/FormControls/CurrencySelector.ascx" TagName="CurrencySelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/CountrySelector.ascx" TagName="CountrySelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ECommerce/FormControls/PaymentSelector.ascx" TagName="PaymentSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ECommerce/FormControls/ShippingSelector.ascx" TagName="ShippingSelector"
    TagPrefix="cms" %>
<asp:Panel ID="pnlMyDetails" runat="server" DefaultButton="btnOK">
    <asp:Label runat="server" ID="lblInfo" EnableViewState="false" Visible="false" />
    <asp:Label runat="server" ID="lblError" EnableViewState="false" Visible="false" ForeColor="Red"
        Style="display: block" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblCustomerFirstName" AssociatedControlID="txtCustomerFirstName"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtCustomerFirstName" runat="server" MaxLength="200" CssClass="TextBoxField"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblCustomerLastName" AssociatedControlID="txtCustomerLastName"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtCustomerLastName" runat="server" MaxLength="200" CssClass="TextBoxField"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblCompanyAccount" runat="server" AssociatedControlID="chkCompanyAccount"
                    EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkCompanyAccount" AutoPostBack="true" OnCheckedChanged="chkCompanyAccount_CheckChanged" />
            </td>
        </tr>
        <asp:Panel runat="server" ID="pnlCompanyInfo" Visible="false" EnableViewState="false">
            <tr>
                <td class="FieldLabel">
                    <asp:Label runat="server" ID="lblCustomerCompany" AssociatedControlID="txtCustomerCompany"
                        EnableViewState="false" />
                </td>
                <td>
                    <cms:ExtendedTextBox ID="txtCustomerCompany" runat="server" MaxLength="200" CssClass="TextBoxField"
                        EnableViewState="false" />
                </td>
            </tr>
            <asp:PlaceHolder ID="plhOrganizationID" runat="server" EnableViewState="false">
                <tr>
                    <td class="FieldLabel">
                        <asp:Label runat="server" ID="lblOrganizationID" AssociatedControlID="txtOraganizationID"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtOraganizationID" runat="server" MaxLength="50" CssClass="TextBoxField"
                            EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plhTaxRegistrationID" runat="server" EnableViewState="false">
                <tr>
                    <td class="FieldLabel">
                        <asp:Label runat="server" ID="lblTaxRegistrationID" AssociatedControlID="txtTaxRegistrationID"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtTaxRegistrationID" runat="server" MaxLength="50" CssClass="TextBoxField"
                            EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
        </asp:Panel>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblCustomerEmail" AssociatedControlID="txtCustomerEmail"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtCustomerEmail" runat="server" MaxLength="200" CssClass="TextBoxField"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblCustomerPhone" AssociatedControlID="txtCustomerPhone"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtCustomerPhone" runat="server" MaxLength="50" CssClass="TextBoxField"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblCustomerFax" AssociatedControlID="txtCustomerFax"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtCustomerFax" runat="server" MaxLength="50" CssClass="TextBoxField"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblCustomerPreferredCurrency" EnableViewState="false" />
            </td>
            <td>
                <cms:CurrencySelector ID="selectCurrency" runat="server" AddNoneRecord="true" IncludeSelected="false" DisplayOnlyWithExchageRate="true" DisplayOnlyEnabled="true" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblCustomerPreferredShippingOption" EnableViewState="false" />
            </td>
            <td>
                <cms:ShippingSelector ID="drpShipping" runat="server" AddNoneRecord="true" IncludeSelected="false"
                    AutoPostBack="true" OnShippingChange="drpShipping_ShippingChange" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblCustomerPrefferedPaymentOption" EnableViewState="false" />
            </td>
            <td>
                <cms:PaymentSelector ID="drpPayment" runat="server" AddNoneRecord="true" IncludeSelected="false" DisplayOnlyEnabled="true" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblCustomerCountry" EnableViewState="false" />
            </td>
            <td>
                <cms:CountrySelector ID="drpCountry" runat="server" AddNoneRecord="true" UseCodeNameForSelection="false"
                    AddSelectCountryRecord="false" IncludeSelected="false" DisplayOnlyEnabled="true" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <br />
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="ContentButton" />
            </td>
        </tr>
    </table>
</asp:Panel>
