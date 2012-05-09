<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Donation.ascx.cs" Inherits="CMSModules_Ecommerce_Controls_UI_ProductTypes_Donation" %>
<%@ Register TagPrefix="cms" TagName="PriceSelector" Src="~/CMSModules/Ecommerce/Controls/UI/PriceSelector.ascx" %>
<asp:Panel ID="pnlDonation" runat="server">
    <table>
        <%-- Minimum donation amount --%>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ResourceString="com.donation.mindonationamount"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:PriceSelector ID="minDonationAmount" runat="server" ValidatorOnNewLine="true"
                    AllowZero="false" AllowEmpty="true" />
            </td>
        </tr>
        <%-- Maximum donation amount --%>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ResourceString="com.donation.maxdonationamount"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:PriceSelector ID="maxDonationAmount" runat="server" ValidatorOnNewLine="true"
                    AllowZero="false" AllowEmpty="true" />
            </td>
        </tr>
        <%-- Allow private donation --%>
        <tr>
            <td class="FieldLabel" style="width: 150px;">
                <cms:LocalizedLabel runat="server" ResourceString="com.donation.privatedonation"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkPrivateDonation" runat="server" />
            </td>
        </tr>
    </table>
</asp:Panel>
