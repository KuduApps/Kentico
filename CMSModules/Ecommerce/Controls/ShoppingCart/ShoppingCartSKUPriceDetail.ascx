<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartSKUPriceDetail_Control" CodeFile="ShoppingCartSKUPriceDetail.ascx.cs" %>
<%--Product--%>
<table border="0" cellspacing="0" cellpadding="3" class="UniGridGrid" style="border-collapse:collapse;">
    <tr class="UniGridHead PriceDetailHeader">
        <td colspan="2">
            <asp:Label ID="lblProductName" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <tr class="EvenRow PriceDetailSubtotal">
        <td>
            <asp:Label ID="lblPriceWithoutTax" runat="server" EnableViewState="false" />
        </td>
        <td class="TextRight">
            <asp:Label ID="lblPriceWithoutTaxValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
</table>
<br />

<%--Discounts--%>
<asp:GridView ID="gridDiscounts" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="UniGridGrid PriceDetailSummaryTable"
    CellPadding="3" CellSpacing="0">
    <HeaderStyle CssClass="UniGridHead" />
    <AlternatingRowStyle CssClass="OddRow" />
    <RowStyle CssClass="EvenRow" />
    <Columns>
        <asp:TemplateField>
            <HeaderStyle CssClass="TextLeft" />
            <ItemTemplate>
                <%# GetFormattedName( Eval("DiscountDisplayName")) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="80" />
            <ItemStyle CssClass="TextRight" Width="80" />
            <ItemTemplate>
                <%# GetFormattedValue( Eval("DiscountValue"), Eval("DiscountIsFlat")) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="80" />
            <ItemStyle CssClass="TextRight" Width="80" />
            <ItemTemplate>
                <%# GetFormattedValue(Eval("UnitDiscount"), true)%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="80" />
            <ItemStyle CssClass="TextRight" Width="80" />
            <ItemTemplate>
                <%# Eval("DiscountedUnits")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="80" />
            <ItemStyle CssClass="TextRight" Width="80" />
            <ItemTemplate>
                <%# GetFormattedValue(Eval("SubtotalDiscount"), true)%>
            </ItemTemplate>
        </asp:TemplateField>                
    </Columns>
</asp:GridView>
<%--Total discount--%>
<table class="UniGridGrid PriceDetailSubtotalTable" cellpadding="3" cellspacing="0" style="border-collapse:collapse; border-top: 0px none;">
    <asp:PlaceHolder ID="plcDiscounts" runat="server">
        <tr class="UniGridHead">
            <th colspan="2" class="TextLeft" style="border-top: 1px solid #B5C3D6;">
                <asp:Label ID="lblDiscounts" runat="server" EnableViewState="false" />
            </th>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcTotalDiscount" runat="server">
        <tr class="EvenRow" style="border-top: 0px none;">
            <td style="border-top: 0px none !important;">
                <asp:Label ID="lblTotalDiscount" runat="server" EnableViewState="false" />
            </td>
            <td class="TextRight" style="border-top: 0px none !important;">
                <asp:Label ID="lblTotalDiscountValue" runat="server" EnableViewState="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr class="OddRow PriceDetailSubtotal" style="border-top: 0px none;">
        <td style="border-top: 0px none !important;">
            <asp:Label ID="lblPriceAfterDiscount" runat="server" EnableViewState="false" />
        </td>
        <td class="TextRight" style="border-top: 0px none !important;">
            <asp:Label ID="lblPriceAfterDiscountValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
</table>
<br />
<%--Taxes--%>
<asp:GridView ID="gridTaxes" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="UniGridGrid PriceDetailSummaryTable"
    CellPadding="3" CellSpacing="0">
    <HeaderStyle CssClass="UniGridHead" />
    <AlternatingRowStyle CssClass="OddRow" />
    <RowStyle CssClass="EvenRow" />
    <Columns>
        <asp:TemplateField>
            <HeaderStyle CssClass="TextLeft" />
            <ItemTemplate>
                <%# GetFormattedName( Eval("TaxClassDisplayName")) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="80" />
            <ItemStyle CssClass="TextRight" Width="80" />
            <ItemTemplate>
                <%# GetFormattedValue(Eval("TaxValue"), Eval("TaxIsFlat"))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="80" />
            <ItemStyle CssClass="TextRight" Width="80" />
            <ItemTemplate>
                <%# GetFormattedValue(Eval("UnitTax"), true)%>
            </ItemTemplate>
        </asp:TemplateField>
       <asp:TemplateField>
            <HeaderStyle Width="80" />
            <ItemStyle CssClass="TextRight" Width="80" />
            <ItemTemplate>
                <%# GetFormattedValue(Eval("SubtotalTax"), true)%>
            </ItemTemplate>
        </asp:TemplateField>        
    </Columns>
</asp:GridView>
<%--Total tax---%>
<table border="0" class="UniGridGrid PriceDetailSubtotalTable" cellpadding="3" cellspacing="0" style="border-collapse:collapse; border-top: 0px none;">
    <asp:PlaceHolder ID="plcTaxes" runat="server">
        <tr class="UniGridHead">
            <th colspan="2" class="TextLeft" style="border-top: 1px solid #B5C3D6;">
                <asp:Label ID="lblTaxes" runat="server" EnableViewState="false" />
            </th>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcTotalTax" runat="server">
        <tr class="EvenRow" style="border-top: 0px none;">
            <td style="border-top: 0px none !important;">
                <asp:Label ID="lblTotalTax" runat="server" EnableViewState="false" />
            </td>
            <td class="TextRight" style="border-top: 0px none !important;">
                <asp:Label ID="lblTotalTaxValue" runat="server" EnableViewState="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcPriceWithTax" runat="server">    
    <tr class="OddRow PriceDetailSubtotal" style="border-top: 0px none;">
        <td style="border-top: 0px none !important;">
            <asp:Label ID="lblPriceWithTax" runat="server" EnableViewState="false" />
        </td>
        <td class="TextRight" style="border-top: 0px none !important;">
            <asp:Label ID="lblPriceWithTaxValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
    </asp:PlaceHolder>
</table>
<br />
<%--Totals--%>
<table border="0" width="100%" cellspacing="0" class="UniGridGrid" cellpadding="3" style="border-collapse:collapse;">
    <tr class="UniGridHead">
        <th colspan="2">
            <cms:LocalizedLabel ID="lblTotal" ResourceString="ProductPriceDetail.Total" runat="server"
                EnableViewState="false" />
        </th>
    </tr>
    <asp:PlaceHolder ID="plcUnits" runat="server">        
    <tr class="EvenRow PriceDetailSubtotal">
        <td>
            <asp:Label ID="lblProductUnits" runat="server" EnableViewState="false" />
        </td>
        <td class="TextRight">
            <asp:Label ID="lblProductUnitsValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr class="OddRow PriceDetailHeader">
        <td>
            <asp:Label ID="lblTotalPrice" runat="server" EnableViewState="false" />
        </td>
        <td class="TextRight">
            <asp:Label ID="lblTotalPriceValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
</table>
