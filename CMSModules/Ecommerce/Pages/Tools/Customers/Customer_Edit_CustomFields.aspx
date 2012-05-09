<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_CustomFields" Theme="Default" Title="Customer edit - Custom fields" CodeFile="Customer_Edit_CustomFields.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <cms:DataForm ID="formCustomerCustomFields" runat="server" IsLiveSite="false" />
</asp:Content>
