<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Ecommerce_Pages_Tools_Products_Product_Edit_VolumeDiscount_List"
    Theme="Default" Title="Product edit - volume discount" CodeFile="Product_Edit_VolumeDiscount_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:UniGrid runat="server" ID="UniGrid" GridName="Product_Edit_VolumeDiscount_List.xml"
        IsLiveSite="false" Columns="VolumeDiscountID,VolumeDiscountValue,VolumeDiscountMinCount,VolumeDiscountIsFlatValue" />
</asp:Content>
