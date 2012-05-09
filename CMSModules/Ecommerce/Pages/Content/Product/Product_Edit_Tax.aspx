<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_Tax" Theme="Default" CodeFile="Product_Edit_Tax.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" %>

<%@ Register Src="~/CMSModules/Ecommerce/Controls/UI/ProductTaxes.ascx" TagName="Product_Edit_Tax"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:Product_Edit_Tax ID="taxForm" runat="server" />
</asp:Content>
