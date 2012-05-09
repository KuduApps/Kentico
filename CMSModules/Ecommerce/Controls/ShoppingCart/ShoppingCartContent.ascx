<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartContent"
    CodeFile="ShoppingCartContent.ascx.cs" %>
<%@ Register Src="~/CMSModules/Ecommerce/FormControls/CurrencySelector.ascx" TagName="CurrencySelector"
    TagPrefix="cms" %>
<asp:Panel ID="pnlCartContent" runat="server" DefaultButton="btnUpdate">
    <asp:Label ID="lblTitle" runat="server" CssClass="BlockTitle" EnableViewState="false" />
    <div class="BlockContent">
        <asp:Label ID="lblInfo" runat="server" CssClass="LabelInfo" Visible="false" EnableViewState="false" />
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <table width="100%">
            <tr>
                <td class="TextLeftt">
                    <asp:Panel ID="pnlNewItem" runat="server" EnableViewState="false">
                        <asp:Image ID="imgNewItem" runat="server" CssClass="NewItemImage" EnableViewState="false" />
                        <asp:LinkButton ID="lnkNewItem" runat="server" CssClass="NewItemLink" EnableViewState="false" />
                    </asp:Panel>
                </td>
                <td class="TextRight">
                    <asp:Panel ID="pnlCurrency" runat="server">
                        <asp:Label ID="lblCurrency" runat="server" EnableViewState="false" AssociatedControlID="selectCurrency" />
                        <cms:CurrencySelector ID="selectCurrency" runat="server" DisplayOnlyWithExchangeRate="true"
                            DoFullPostback="true" RenderInline="true" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gridData" runat="server" AutoGenerateColumns="false" CellPadding="3"
                        AllowSorting="true" CssClass="UniGridGrid CartContentTable" GridLines="horizontal"
                        Width="100%">
                        <HeaderStyle HorizontalAlign="Left" CssClass="UniGridHead" />
                        <RowStyle CssClass="EvenRow" />
                        <AlternatingRowStyle CssClass="OddRow" />
                        <Columns>
                            <asp:BoundField DataField="CartItemId" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblGuid" runat="server" Text='<%# Eval("CartItemGuid")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CartItemParentGuid" />
                            <asp:BoundField DataField="SKUID" />
                            <asp:TemplateField>
                                <HeaderStyle Width="50" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRemove" runat="server" Checked="false" Visible='<%# !IsProductOption(Eval("IsProductOption")) && !IsBundleItem(Eval("IsBundleItem")) %>'
                                        EnableViewState="false" />
                                    <cms:LocalizedLabel ID="lblRemove" runat="server" AssociatedControlID="chkRemove"
                                        EnableViewState="false" Display="false" ResourceString="general.remove" Visible='<%# !IsProductOption(Eval("IsProductOption")) && !IsBundleItem(Eval("IsBundleItem")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <%# GetSKUName(Eval("SKUID"), Eval("SKUSiteID"), Eval("SKUName"), Eval("IsProductOption"), Eval("IsBundleItem"), Eval("CartItemIsPrivate"), Eval("CartItemText"), Eval("SKUProductType"))%>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle Width="60" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <cms:CMSTextBox ID="txtUnits" runat="server" Text='<%# Eval("Units")%>' CssClass="UnitsTextBox"
                                        MaxLength="9" Visible='<%# !IsProductOption(Eval("IsProductOption"))  && !IsBundleItem(Eval("IsBundleItem")) %>'
                                        EnableViewState="false" />
                                    <cms:LocalizedLabel ID="lblUnits" runat="server" AssociatedControlID="txtUnits" Display="false"
                                        EnableViewState="false" ResourceString="general.units" Visible='<%# !IsProductOption(Eval("IsProductOption")) && !IsBundleItem(Eval("IsBundleItem")) %>' />
                                    <%# GetChildCartItemUnits(Eval("Units"), Eval("IsProductOption"), Eval("IsBundleItem")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" Width="80" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSKUPrice" runat="server" Text='<%# GetFormattedValue(Eval("UnitPrice")) %>'
                                        EnableViewState="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Right" Width="80" />
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitDiscount" runat="server" Text='<%# GetFormattedValue(Eval("UnitTotalDiscount")) %>'
                                        EnableViewState="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" Width="80" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTax" runat="server" Text='<%# GetFormattedValue(Eval("TotalTax")) %>'
                                        EnableViewState="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Wrap="false">
                                <ItemStyle CssClass="TextRight" />
                                <HeaderStyle HorizontalAlign="Right" Width="100" />
                                <ItemTemplate>
                                    <span class="LeftAlign">
                                        <asp:Label ID="lblPriceDetail" runat="server" Text='<%# GetPriceDetailLink(Eval("CartItemGuid")) %>'
                                            CssClass="ProductPriceDetailLink" EnableViewState="false" />
                                        <asp:Label ID="lblEditItem" runat="server" Text='<%# GetEditOrderItemLink(Eval("CartItemGuid")) %>'
                                            CssClass="EditOrderItemLink" EnableViewState="false" />
                                    </span>
                                    <asp:Label ID="lblSubtotal" runat="server" Text='<%# GetFormattedValue(Eval("TotalPrice")) %>'
                                        EnableViewState="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="TextLeftt">
                </td>
                <td class="TextRight">
                    <asp:PlaceHolder ID="plcCoupon" runat="server" EnableViewState="false">
                        <asp:Label ID="lblCoupon" AssociatedControlID="txtCoupon" runat="server" EnableViewState="false" />&nbsp;
                        <cms:CMSTextBox ID="txtCoupon" runat="server" MaxLength="200" EnableViewState="false" />
                    </asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="TextLeftt" colspan="2">
                    <asp:Panel ID="pnlPrice" runat="server" EnableViewState="false">
                        <table width="100%">
                            <asp:PlaceHolder ID="plcShippingPrice" runat="server">
                                <tr class="TotalShipping">
                                    <td style="width: 100%">
                                        &nbsp;
                                    </td>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblShippingPrice" runat="server" EnableViewState="false" />
                                    </td>
                                    <td class="TextRight" style="white-space: nowrap;">
                                        <asp:Label ID="lblShippingPriceValue" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <tr class="TotalPrice">
                                <td style="width: 100%">
                                    &nbsp;
                                </td>
                                <td style="white-space: nowrap;">
                                    <strong>
                                        <asp:Label ID="lblTotalPrice" runat="server" EnableViewState="false" /></strong>
                                </td>
                                <td class="TextRight" style="white-space: nowrap;">
                                    <strong>
                                        <asp:Label ID="lblTotalPriceValue" runat="server" EnableViewState="false" /></strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <cms:CMSButton ID="btnEmpty" runat="server" OnClick="btnEmpty_Click1" CssClass="ContentButton"
                                        EnableViewState="false" />
                                </td>
                                <td colspan="2" class="TextRight">
                                    <cms:CMSButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click1" CssClass="ContentButton"
                                        EnableViewState="false" />
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblTotalTax" runat="server" EnableViewState="false" /><br />
                        <asp:Label ID="lblTaxSubtotal" runat="server" EnableViewState="false" /><br />
                        <asp:Label ID="lblRoundedDifference" runat="server" EnableViewState="false" /><br />
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <asp:CheckBox ID="chkSendEmail" runat="server" Visible="false" EnableViewState="false" />
    <asp:Literal ID="ltlAlert" runat="server" EnableViewState="false" />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="true" />
</asp:Panel>

<script type="text/javascript">
    //<![CDATA[
    // Opens the "Add product to shopping cart" in modal dialog
    function OpenAddProductDialog(cartSessionKey) {
        var cart = "";
        if ((cartSessionKey != null) & (cartSessionKey != "")) {
            cart = "?cart=" + cartSessionKey;
        }
        modalDialog('Order_Edit_AddItems.aspx' + cart, 'addProduct', 700, 575);
    }
    //]]>
</script>

<asp:HiddenField runat="server" ID="hdnPrice" />
<asp:HiddenField runat="server" ID="hdnIsPrivate" />
