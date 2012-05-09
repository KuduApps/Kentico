<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Ecommerce_Controls_PaymentGateways_CreditPaymentForm" CodeFile="CreditPaymentForm.ascx.cs" %>
<asp:Label ID="lblTitle" runat="server" CssClass="BlockTitle" EnableViewState="false" />
<asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="margin-top: 3px;">
    <tr>
        <td>
            <asp:Label ID="lblCredit" runat="server" EnableViewState="false" /></td>
        <td>
            <asp:Label ID="lblCreditValue" runat="server" EnableViewState="false" /></td>
    </tr>
</table>
