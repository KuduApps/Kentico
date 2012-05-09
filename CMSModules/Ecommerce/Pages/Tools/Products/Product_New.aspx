<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Products_Product_New"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Title="Product properties"
    CodeFile="Product_New.aspx.cs" %>

<%@ Register TagPrefix="cms" TagName="ProductEdit" Src="~/CMSModules/Ecommerce/Controls/UI/ProductEdit.ascx" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:ProductEdit ID="productEditElem" runat="server" OnProductSaved="productEditElem_ProductSaved" />
</asp:Content>
