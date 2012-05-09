<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartPaymentShipping"
    CodeFile="ShoppingCartPaymentShipping.ascx.cs" %>
<%@ Register Src="~/CMSModules/ECommerce/FormControls/PaymentSelector.ascx" TagName="PaymentSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ECommerce/FormControls/ShippingSelector.ascx" TagName="ShippingSelector"
    TagPrefix="cms" %>
<div class="BlockContent">
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblTitle" runat="server" CssClass="BlockTitle" EnableViewState="false" />
    <table cellspacing="0" cellpadding="10" border="0">
        <%-- Shipping --%>
        <asp:PlaceHolder ID="plcShipping" runat="server" Visible="false">
            <tr>
                <td colspan="2">
                    <cms:LocalizedLabel ID="lblFreeShippingInfo" runat="server" ResourceString="ecommerce.order.freeshipping"
                        Visible="false" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel" style="width: 100px">
                    <asp:Label ID="lblShipping" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                </td>
                <td>
                    <cms:ShippingSelector ID="selectShipping" runat="server" AddNoneRecord="false" IncludeSelected="false"
                        AutoPostBack="true" IsLiveSite="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <%-- Payment --%>
        <asp:PlaceHolder ID="plcPayment" runat="server">
            <tr>
                <td class="FieldLabel" style="width: 100px">
                    <asp:Label ID="lblPayment" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                </td>
                <td>
                    <cms:PaymentSelector ID="selectPayment" runat="server" AddNoneRecord="false" IncludeSelected="false"
                        AutoPostBack="true" DisplayOnlyEnabled="true" IsLiveSite="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
    <%-- <asp:Panel id="PaymentGatewayCustomData" runat="server" CssClass="PaymentGatewayDataContainer" /> --%>
</div>
