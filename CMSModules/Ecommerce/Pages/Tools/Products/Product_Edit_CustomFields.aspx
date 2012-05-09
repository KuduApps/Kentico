<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Ecommerce_Pages_Tools_Products_Product_Edit_CustomFields"
    Theme="Default" Title="Product edit - Custom fields" CodeFile="Product_Edit_CustomFields.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" ResourceString="General.ChangesSaved" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:DataForm ID="formProductCustomFields" runat="server" IsLiveSite="false" />
</asp:Content>
