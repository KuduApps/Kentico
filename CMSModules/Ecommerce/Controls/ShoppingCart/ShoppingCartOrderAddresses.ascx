<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartOrderAddresses"
    CodeFile="ShoppingCartOrderAddresses.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/CountrySelector.ascx" TagName="CountrySelector"
    TagPrefix="cms" %>
<asp:Label ID="lblBillingTitle" runat="server" CssClass="BlockTitle" EnableViewState="false" />
<div class="BlockContent">
    <asp:Label ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
    <table id="billingAddrTable" style="vertical-align: top">
        <asp:PlaceHolder ID="plhBillAddr" runat="server" Visible="false">
            <tr>
                <%--Billing address--%>
                <td class="FieldLabel">
                    <asp:Label ID="lblBillingAddr" runat="server" CssClass="ContentLabel" EnableViewState="false"
                        AssociatedControlID="drpBillingAddr" />
                </td>
                <td>
                    <asp:DropDownList ID="drpBillingAddr" runat="server" CssClass="DropDownField" AutoPostBack="true"
                        DataTextField="AddressName" DataValueField="AddressID" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <%--Billing address name--%>
            <td class="FieldLabel">
                <asp:Label ID="lblBillingName" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    AssociatedControlID="txtBillingName" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtBillingName" runat="server" CssClass="TextBoxField" MaxLength="200" /><asp:Label
                    ID="lblMark1" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <%--Billing address lines--%>
            <td class="FieldLabel">
                <asp:Label ID="lblBillingAddrLine" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    AssociatedControlID="txtBillingAddr1" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtBillingAddr1" runat="server" CssClass="TextBoxField"
                    MaxLength="100" /><asp:Label ID="lblMark2" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblBillingAddrLine2" runat="server" CssClass="HiddenLabel" EnableViewState="false"
                    AssociatedControlID="txtBillingAddr2" />
            </td>
            <td>
                <label>
                    <cms:ExtendedTextBox ID="txtBillingAddr2" runat="server" CssClass="TextBoxField"
                        MaxLength="100" EnableViewState="false" /></label>
            </td>
        </tr>
        <tr>
            <%--Billing city--%>
            <td class="FieldLabel">
                <asp:Label ID="lblBillingCity" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    AssociatedControlID="txtBillingCity" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtBillingCity" runat="server" CssClass="TextBoxField" MaxLength="100" /><asp:Label
                    ID="lblMark3" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <%--Billing ZIP--%>
            <td class="FieldLabel">
                <asp:Label ID="lblBillingZip" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    AssociatedControlID="txtBillingZip" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtBillingZip" runat="server" CssClass="TextBoxField" MaxLength="20" /><asp:Label
                    ID="lblMark4" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <%--Billing country and state--%>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblBillingCountry" runat="server" CssClass="ContentLabel"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:CountrySelector ID="CountrySelector1" runat="server" UseCodeNameForSelection="false"
                    AddNoneRecord="true" AddSelectCountryRecord="false" />
            </td>
        </tr>
        <tr>
            <%--Billing phone--%>
            <td class="FieldLabel">
                <asp:Label ID="lblBillingPhone" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    AssociatedControlID="txtBillingPhone" />
            </td>
            <td>
                <cms:ExtendedTextBox ID="txtBillingPhone" runat="server" CssClass="TextBoxField"
                    MaxLength="100" EnableViewState="false" />
            </td>
        </tr>
    </table>
</div>
<asp:PlaceHolder runat="server" ID="plcShippingAddress">
    <asp:Label ID="lblShippingTitle" runat="server" CssClass="BlockTitle" EnableViewState="false" />
    <div class="BlockContent">
        <asp:CheckBox ID="chkShippingAddr" runat="server" Checked="false" OnCheckedChanged="chkShippingAddr_CheckedChanged"
            AutoPostBack="true" />
    </div>
    <asp:PlaceHolder ID="plhShipping" runat="server" Visible="false">
        <div class="BlockContent">
            <table id="shippingAddrTable" style="vertical-align: top">
                <asp:PlaceHolder ID="plhShippAddr" runat="server" Visible="false">
                    <tr>
                        <%--Shipping address--%>
                        <td class="FieldLabel">
                            <asp:Label ID="lblShippingAddr" runat="server" CssClass="ContentLabel" EnableViewState="false"
                                AssociatedControlID="drpShippingAddr" />
                        </td>
                        <td>
                            <asp:DropDownList ID="drpShippingAddr" runat="server" CssClass="DropDownField" AutoPostBack="true"
                                DataTextField="AddressName" DataValueField="AddressID" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <%--Shipping address name--%>
                    <td class="FieldLabel">
                        <asp:Label ID="lblShippingName" runat="server" CssClass="ContentLabel" EnableViewState="false"
                            AssociatedControlID="txtShippingName" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtShippingName" runat="server" CssClass="TextBoxField"
                            MaxLength="200" /><asp:Label ID="lblMark5" runat="server" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <%--Shipping address lines--%>
                    <td class="FieldLabel">
                        <asp:Label ID="lblShippingAddrLine" runat="server" CssClass="ContentLabel" EnableViewState="false"
                            AssociatedControlID="txtShippingAddr1" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtShippingAddr1" runat="server" CssClass="TextBoxField"
                            MaxLength="100" /><asp:Label ID="lblMark6" runat="server" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblShippingAddrLine2" runat="server" CssClass="HiddenLabel" EnableViewState="false"
                            AssociatedControlID="txtShippingAddr2" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtShippingAddr2" runat="server" CssClass="TextBoxField"
                            MaxLength="100" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <%--Shipping city--%>
                    <td class="FieldLabel">
                        <asp:Label ID="lblShippingCity" runat="server" CssClass="ContentLabel" EnableViewState="false"
                            AssociatedControlID="txtShippingCity" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtShippingCity" runat="server" CssClass="TextBoxField"
                            MaxLength="100" /><asp:Label ID="lblMark7" runat="server" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <%--Shipping ZIP--%>
                    <td class="FieldLabel">
                        <asp:Label ID="lblShippingZip" runat="server" CssClass="ContentLabel" EnableViewState="false"
                            AssociatedControlID="txtShippingZip" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtShippingZip" runat="server" CssClass="TextBoxField" MaxLength="20" /><asp:Label
                            ID="lblMark8" runat="server" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <%--Shipping country--%>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblShippingCountry" runat="server" CssClass="ContentLabel"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CountrySelector ID="CountrySelector2" runat="server" UseCodeNameForSelection="false"
                            AddNoneRecord="true" AddSelectCountryRecord="false" />
                    </td>
                </tr>
                <tr>
                    <%--Shipping phone--%>
                    <td class="FieldLabel">
                        <asp:Label ID="lblShippingPhone" runat="server" CssClass="ContentLabel" EnableViewState="false"
                            AssociatedControlID="txtShippingPhone" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtShippingPhone" runat="server" CssClass="TextBoxField"
                            MaxLength="100" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:PlaceHolder>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plcCompanyAll" runat="server">
    <asp:Label ID="lblCompanyAddressTitle" runat="server" CssClass="BlockTitle" EnableViewState="false" />
    <div class="BlockContent">
        <asp:CheckBox ID="chkCompanyAddress" runat="server" Checked="false" OnCheckedChanged="chkCompanyAddress_CheckedChanged"
            AutoPostBack="true" />
    </div>
    <asp:PlaceHolder ID="plcCompanyDetail" runat="server" Visible="false">
        <div class="BlockContent">
            <table id="Table1" style="vertical-align: top">
                <asp:PlaceHolder ID="plcCompanyAddress" runat="server" Visible="false">
                    <tr>
                        <%--Company address--%>
                        <td class="FieldLabel">
                            <asp:Label ID="lblCompanyAddress" runat="server" CssClass="ContentLabel" EnableViewState="false"
                                AssociatedControlID="drpCompanyAddress" />
                        </td>
                        <td>
                            <asp:DropDownList ID="drpCompanyAddress" runat="server" CssClass="DropDownField"
                                AutoPostBack="true" DataTextField="AddressName" DataValueField="AddressID" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <%--Company address name--%>
                    <td class="FieldLabel">
                        <asp:Label ID="lblCompanyName" runat="server" CssClass="ContentLabel" EnableViewState="false"
                            AssociatedControlID="txtCompanyName" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtCompanyName" runat="server" CssClass="TextBoxField" MaxLength="200" /><asp:Label
                            ID="lblMark9" runat="server" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <%--Company address lines--%>
                    <td class="FieldLabel">
                        <asp:Label ID="lblCompanyLines" runat="server" CssClass="ContentLabel" EnableViewState="false"
                            AssociatedControlID="txtCompanyLine1" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtCompanyLine1" runat="server" CssClass="TextBoxField"
                            MaxLength="100" /><asp:Label ID="lblMark10" runat="server" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCompanyLines2" runat="server" CssClass="HiddenLabel" EnableViewState="false"
                            AssociatedControlID="txtCompanyLine2" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtCompanyLine2" runat="server" CssClass="TextBoxField"
                            MaxLength="100" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <%--Company city--%>
                    <td class="FieldLabel">
                        <asp:Label ID="lblCompanyCity" runat="server" CssClass="ContentLabel" EnableViewState="false"
                            AssociatedControlID="txtCompanyCity" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtCompanyCity" runat="server" CssClass="TextBoxField" MaxLength="100" /><asp:Label
                            ID="lblMark11" runat="server" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <%--Company ZIP--%>
                    <td class="FieldLabel">
                        <asp:Label ID="lblCompanyZip" runat="server" CssClass="ContentLabel" EnableViewState="false"
                            AssociatedControlID="txtCompanyZip" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtCompanyZip" runat="server" CssClass="TextBoxField" MaxLength="20" /><asp:Label
                            ID="lblMark12" runat="server" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <%--Company country--%>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblCompanyCountry" runat="server" CssClass="ContentLabel"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CountrySelector ID="CountrySelector3" runat="server" UseCodeNameForSelection="false"
                            AddNoneRecord="true" AddSelectCountryRecord="false" />
                    </td>
                </tr>
                <tr>
                    <%--Company phone--%>
                    <td class="FieldLabel">
                        <asp:Label ID="lblCompanyPhone" runat="server" CssClass="ContentLabel" EnableViewState="false"
                            AssociatedControlID="txtCompanyPhone" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtCompanyPhone" runat="server" CssClass="TextBoxField"
                            MaxLength="100" EnableViewState="false" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:PlaceHolder>
</asp:PlaceHolder>
