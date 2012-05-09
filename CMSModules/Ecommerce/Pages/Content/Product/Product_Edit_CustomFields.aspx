<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_CustomFields" Theme="Default" CodeFile="Product_Edit_CustomFields.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:DataForm ID="formProductCustomFields" runat="server" ClassName="Ecommerce.SKU"
        IsLiveSite="false" />
</asp:Content>
