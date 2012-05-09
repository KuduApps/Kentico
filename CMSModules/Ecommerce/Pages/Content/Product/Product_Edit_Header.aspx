<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/TabsHeader.master" Inherits="CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_Header" Theme="Default" CodeFile="Product_Edit_Header.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcSiteSelector" ID="content" runat="server">
    <cms:LocalizedCheckBox CssClass="CheckBoxProd" ID="chkMarkDocAsProd" runat="server" OnCheckedChanged="chkMarkDocAsProd_CheckedChanged"
        AutoPostBack="true" Checked="true" ResourceString="product_selection.docisasprod" />
</asp:Content>
