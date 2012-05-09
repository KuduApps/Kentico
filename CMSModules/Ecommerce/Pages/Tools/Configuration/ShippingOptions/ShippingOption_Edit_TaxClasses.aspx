<%@ Page Title="" Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true" CodeFile="ShippingOption_Edit_TaxClasses.aspx.cs" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_Edit_TaxClasses" Theme="Default"%>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" EnableViewState="false" />
    <cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" ObjectType="ecommerce.taxclass"
        SelectionMode="Multiple" ResourcePrefix="taxclassselector" />
</asp:Content>
