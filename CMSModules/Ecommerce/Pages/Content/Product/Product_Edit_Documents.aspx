<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_Documents"    Theme="Default" Title="Product edit - documents" CodeFile="Product_Edit_Documents.aspx.cs"  MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" %>

<%@ Register Src="~/CMSModules/Ecommerce/Controls/UI/ProductDocuments.ascx" TagName="ProductDocuments"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:ProductDocuments ID="productDocuments" runat="server" />
</asp:Content>
