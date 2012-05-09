<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartPaymentGateway" CodeFile="ShoppingCartPaymentGateway.ascx.cs" %>
<asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel" Visible="false" />
<asp:Label ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel" Visible="false" />
<asp:Label ID="lblTitle" runat="server" EnableViewState="false" CssClass="BlockTitle" />
<div class="BlockContent">
    <table style="margin-top:3px;margin-bottom:10px;" cellpadding="3">
    <tr>
        <td><asp:Label ID="lblOrderId" runat="server" EnableViewState="false" /></td>
        <td><asp:Label ID="lblOrderIdValue" runat="server" EnableViewState="false" /></td>
    </tr>
    <tr>
        <td><asp:Label ID="lblPayment" runat="server" EnableViewState="false" /></td>
        <td><asp:Label ID="lblPaymentValue" runat="server" EnableViewState="false" /></td>
    </tr>
    <tr>
        <td><asp:Label ID="lblTotalPrice" runat="server" EnableViewState="false" /></td>
        <td><asp:Label ID="lblTotalPriceValue" runat="server" EnableViewState="false" /></td>
    </tr>
    </table>    
    <asp:Panel id="PaymentDataContainer" runat="server" CssClass="PaymentGatewayDataContainer" />
</div>