<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartCustomerSelection" CodeFile="ShoppingCartCustomerSelection.ascx.cs" %>
<%@ Register Src="~/CMSModules/Ecommerce/FormControls/CustomerSelector.ascx" TagName="CustomerSelector"
    TagPrefix="cms" %>
<asp:Label ID="lblTitle" runat="server" CssClass="BlockTitle" EnableViewState="false" />
<div class="BlockContent">
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="False"
        Visible="False" />
    <cms:CustomerSelector runat="server" ID="customerSelector" DisplayOnlyEnabled="true" />
</div>
