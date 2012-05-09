<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_TaxClasses_TaxClass_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="TaxClass_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:LocalizedLabel ID="lblMissingRate" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        ResourceString="com.NeedExchangeRateFromGlobal" Visible="false" />
    <asp:Label ID="lblError" runat="server" Visible="false" EnableViewState="false" CssClass="ErrorLabel" />
    <asp:Label ID="lblGlobalInfo" runat="server" Visible="false" EnableViewState="false"
        Font-Bold="true" CssClass="InfoLabel" />
    <cms:UniGrid runat="server" ID="UniGrid" GridName="TaxClass_List.xml" OrderBy="TaxClassDisplayName"
        IsLiveSite="false" Columns="TaxClassID,TaxClassDisplayName" />
</asp:Content>
