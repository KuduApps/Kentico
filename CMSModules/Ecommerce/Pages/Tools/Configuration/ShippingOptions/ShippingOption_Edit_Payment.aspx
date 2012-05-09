<%@ Page Language="C#" AutoEventWireup="true"    Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_Edit_Payment" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="ShippingOption_Edit_Payment.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" EnableViewState="false" />
    <cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" ObjectType="ecommerce.paymentoption"
        SelectionMode="Multiple" ResourcePrefix="paymentselector" />
</asp:Content>
