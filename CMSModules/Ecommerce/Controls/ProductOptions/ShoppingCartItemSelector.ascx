<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_ProductOptions_ShoppingCartItemSelector"
    CodeFile="ShoppingCartItemSelector.ascx.cs" %>
<%@ Register TagPrefix="cms" TagName="DonationProperties" Src="~/CMSModules/Ecommerce/Controls/ProductOptions/DonationProperties.ascx" %>
<cms:CMSUpdatePanel ID="upnlAjax" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlContainer" runat="server" CssClass="CartItemSelectorContainer">
            <asp:Panel ID="pnlSelectors" runat="server" CssClass="CartItemSelector" Visible="false">
                <%-- Donation properties --%>
                <cms:DonationProperties runat="server" ID="donationProperties" Visible="false" StopProcessing="true"
                    ShowDonationUnits="false" />
                <%-- Here come product options --%>
            </asp:Panel>
            <%-- Price --%>
            <asp:Panel ID="pnlPrice" runat="server" CssClass="TotalPriceContainer" EnableViewState="false">
                <asp:Label ID="lblPrice" runat="server" CssClass="TotalPriceLabel" EnableViewState="false" />
                <asp:Label ID="lblPriceValue" runat="server" CssClass="TotalPrice" EnableViewState="false" />
            </asp:Panel>
            <asp:Panel ID="pnlButton" runat="server" CssClass="AddToCartContainer" EnableViewState="false">
                <%-- Add to wishlist --%>
                <asp:ImageButton ID="btnWishlist" runat="server" Visible="false" CssClass="AddToWishlistImageButton" />
                <asp:LinkButton ID="lnkWishlist" runat="server" Visible="false" CssClass="AddToWishlistLink" />
                <%-- Units --%>
                <cms:LocalizedLabel ID="lblUnits" runat="server" Visible="false" ResourceString="ecommerce.shoppingcartcontent.skuunits" />
                <cms:CMSTextBox ID="txtUnits" runat="server" Visible="false" CssClass="AddToCartTextBox"
                    MaxLength="9" />
                <%-- Add to cart controls --%>
                <cms:CMSButton ID="btnAdd" runat="server" Visible="false" CssClass="SubmitButton AddToCartButton" />
                <asp:ImageButton ID="btnAddImage" runat="server" Visible="false" CssClass="AddToCartImageButton"
                    OnClick="btnAddImage_Click" />
                <asp:LinkButton ID="lnkAdd" runat="server" Visible="false" CssClass="AddToCartLink" />
            </asp:Panel>
        </asp:Panel>
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
