<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_Options" Theme="Default" CodeFile="Product_Edit_Options.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" %>

<%@ Register Src="~/CMSModules/Ecommerce/Controls/UI/ProductOptions.ascx" TagName="ProductOptions"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:ProductOptions ID="ucOptions" runat="server" />
</asp:Content>
