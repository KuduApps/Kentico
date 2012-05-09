<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_CustomFields" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Order edit - Custom fields" CodeFile="Order_Edit_CustomFields.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <cms:DataForm ID="formOrderCustomFields" runat="server" IsLiveSite="false" />
</asp:Content>

