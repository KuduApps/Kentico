<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_Address_List" Theme="Default" CodeFile="Customer_Edit_Address_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label ID="lblError" runat="server" Visible="false" EnableViewState="false" CssClass="ErrorLabel" />
    <cms:UniGrid runat="server" ID="UniGrid" GridName="Customer_Edit_Address_List.xml"
        OrderBy="AddressName" IsLiveSite="false" Columns="AddressID,AddressName,AddressIsBilling,AddressIsShipping,AddressIsCompany,AddressEnabled" />
</asp:Content>
