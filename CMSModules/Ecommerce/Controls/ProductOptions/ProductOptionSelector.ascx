<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_ProductOptions_ProductOptionSelector" CodeFile="ProductOptionSelector.ascx.cs" %>
<asp:Panel ID="pnlContainer" runat="server" CssClass="ProductOptionSelectorContainer">
    <asp:Panel ID="plnError" runat="server" Visible="false" CssClass="OptionCategoryErrorContainer">
        <cms:LocalizedLabel ID="lblError" runat="server" CssClass="ErrorLabel OptionCategoryError" EnableViewState="false" />
    </asp:Panel>
    <cms:LocalizedLabel ID="lblCategName" runat="server" CssClass="OptionCategoryName" EnableViewState="false" />
    <asp:Panel ID="pnlSelector" runat="server" CssClass="ProductOptionSelector" />    
    <asp:Label ID="lblCategDescription" runat="server" CssClass="OptionCategoryDescription" EnableViewState="false" />
</asp:Panel>