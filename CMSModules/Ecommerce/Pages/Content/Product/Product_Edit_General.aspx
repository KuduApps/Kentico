<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_General" Theme="Default"
    Title="Product - edit - general" CodeFile="Product_Edit_General.aspx.cs" %>

<%@ Register TagPrefix="cms" TagName="ProductEdit" Src="~/CMSModules/Ecommerce/Controls/UI/ProductEdit.ascx" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:ProductEdit ID="productEditElem" runat="server" />
</asp:Content>
