<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DonationProperties.ascx.cs"
    Inherits="CMSModules_Ecommerce_Controls_ProductOptions_DonationProperties" %>
<%@ Register TagPrefix="cms" TagName="PriceSelector" Src="~/CMSModules/Ecommerce/Controls/UI/PriceSelector.ascx" %>
<table>
    <%-- Donation amount --%>
    <asp:PlaceHolder runat="server" ID="plcAmount">
        <tr>
            <td class="FieldLabel" style="vertical-align: top; padding-top: 4px;">
                <cms:LocalizedLabel runat="server" ResourceString="com.donationproperties.amount"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:PriceSelector runat="server" ID="amountPriceSelector" ValidatorOnNewLine="true"
                    AllowZero="false" FormatValueAsInteger="true" />
                <div>
                    <cms:LocalizedLabel runat="server" ID="lblAmountError" EnableViewState="false" CssClass="ErrorLabel"
                        Visible="false" />
                </div>
            </td>
        </tr>
    </asp:PlaceHolder>
    <%-- Donation units --%>
    <asp:PlaceHolder runat="server" ID="plcUnits">
        <tr>
            <td class="FieldLabel" style="vertical-align: top; padding-top: 4px;">
                <cms:LocalizedLabel runat="server" ResourceString="com.donationproperties.units"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtUnits" Text="1" MaxLength="9" CssClass="TextBoxField" />
                <div>
                    <cms:CMSRangeValidator ID="rvUnits" runat="server" ControlToValidate="txtUnits" MaximumValue="999999999"
                        MinimumValue="1" Type="Integer" EnableViewState="false" Display="Dynamic" />
                </div>
            </td>
        </tr>
    </asp:PlaceHolder>
    <%-- Donation is private --%>
    <asp:PlaceHolder runat="server" ID="plcIsPrivate">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ResourceString="com.donationproperties.isprivate"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkIsPrivate" Checked="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
</table>
